using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardboardBoxShelf : MonoBehaviour
{
    [SerializeField] private ShopShelf shelfInBox;
    [SerializeField] private GameObject shelfBoxImage01;
    [SerializeField] private GameObject shelfBoxImage02;
    [SerializeField] private Material defaultMaterial;

    private GameObject previewShelf;
    private bool canPlaceShelf = false;
    private Quaternion shelfRotation;

    public void SetupBox(ShopShelf shelfAdded)
    {
        shelfInBox = shelfAdded;
        UpdateBox();
    }

    private void UpdateBox()
    {
        if (shelfInBox == null)
        {
            shelfBoxImage01.GetComponent<MeshRenderer>().material = defaultMaterial;
            shelfBoxImage02.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        else
        {
            shelfBoxImage01.GetComponent<MeshRenderer>().material = shelfInBox.shelfImage;
            shelfBoxImage02.GetComponent<MeshRenderer>().material = shelfInBox.shelfImage;

            shelfRotation = shelfInBox.shelfObj.transform.rotation;
        }       
    }

    public void SpawnPreviewShelf(Quaternion rotation) //makes preview shelf from the shelfInBoxObj
    {
        if (previewShelf != null)
        {
            Destroy(previewShelf);
        }

        if (shelfInBox != null)
        {            
            previewShelf = Instantiate(shelfInBox.shelfObj);
            //previewShelf.GetComponent<Collider>().enabled = false;
            previewShelf.transform.rotation = shelfInBox.shelfObj.transform.rotation;
            SetPreviewMaterial(previewShelf, true);
        }
    }

    public void DestroyPreviewShelf()
    {
        Destroy(previewShelf);
    }

    public void HandlePreviewPosition(Transform cameraTransform, float interactRange, Vector3 objectHoldPos)
    {
        if (previewShelf == null)
        {
            return;
        }
        
        MeshRenderer meshCollider = previewShelf.GetComponent<MeshRenderer>();      
        Vector3 holdPos = new Vector3(objectHoldPos.x,(meshCollider.bounds.size.y / 2) + 0.3f, objectHoldPos.z); //0.3f is the floor height

        previewShelf.transform.position = holdPos;

        if (previewShelf.GetComponent<ShelfDetection>().CheckTags() == true)
        {   
            canPlaceShelf = true;
            SetPreviewMaterial(previewShelf, canPlaceShelf); //change colour if shelf can go there
        }
        else 
        {
            canPlaceShelf = false;
            SetPreviewMaterial(previewShelf, canPlaceShelf);
        }

        previewShelf.SetActive(true);                        
    }

    public void PlaceShelf()
    {
        if (previewShelf != null && canPlaceShelf)
        {
            GameObject newShelf = Instantiate(shelfInBox.shelfObj, previewShelf.transform.position, previewShelf.transform.rotation);
            ShelfController shelfController = newShelf.GetComponent<ShelfController>();
            shelfController.SetColliderActive();

            Destroy(previewShelf);
            EventManager.OnPlacedShelf();
        }
        else 
        {
            UIController.instance.DisplayErrorMessage("Cant place shelf");
        }
    }

    public void RotateShelf(float angle)
    {
        Vector3 currentRotation = previewShelf.transform.rotation.eulerAngles;

        switch (shelfInBox.axisRotation.ToLower())
        {
            case "x":
                shelfRotation *= Quaternion.Euler(angle, 0, 0);
                break;

            case "y":
                shelfRotation *= Quaternion.Euler(0, angle, 0);
                break;

            case "z":
                shelfRotation *= Quaternion.Euler(0, 0, angle);
                break;

            default:
                Debug.LogError("not a good axis in the scriptable object: " + shelfInBox.name);
                break;
        }

        previewShelf.transform.rotation = shelfRotation;
    }

    private void SetPreviewMaterial(GameObject obj, bool isValid)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            Color colour;

            if (isValid == true)
            {
                colour = new Color(0, 1, 0, 0.5f);
            }
            else
            {
                colour = new Color(1, 0, 0, 0.5f);
            }

            rend.material.color = colour;
        }
    }

    public bool IsPreviewShelfMade()
    {
        if (previewShelf == null)
        {
            return false;
        }

        else
        {
            return true;
        }
    }

    public Quaternion GetRotation()
    {
        return shelfRotation;
    }

    public bool GetCanPlaceShelf()
    {
        return canPlaceShelf;
    }
}
