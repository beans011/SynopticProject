using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Object Interact Keybinds")]
    [SerializeField] private KeyCode playerObjectInteractKey;
    [SerializeField] private KeyCode playerObjectInteractSecondaryKey;
    [SerializeField] private KeyCode playerObjectDropKey;
    [SerializeField] private KeyCode playerObjectThrowKey;

    [Header("Object Interaction")]
    [SerializeField] private Transform holdPos;
    [SerializeField] private float interactRange;
    private GameObject holdingObject;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    void Update()
    {
        PlayerInteractionStuff();
    }

    private void PlayerInteractionStuff()
    {
        if (Input.GetKeyDown(playerObjectInteractKey))
        {
            RaycastHit hit;

            if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, interactRange))
            {
                if (holdingObject == null) //picking up box
                {
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }

                if (holdingObject != null) //putting item onto shelf
                {
                    if (hit.transform.gameObject.tag == "shelfSign")
                    {
                        //check if item can be added to the shelf
                        if (hit.transform.gameObject.GetComponent<Shelf>().GetItem() == null || holdingObject.GetComponent<CardboardBox>().GetItem() == hit.transform.gameObject.GetComponent<Shelf>().GetItem())
                        {
                            hit.transform.gameObject.GetComponent<Shelf>().AddItemsToShelf(1, holdingObject.GetComponent<CardboardBox>().GetItem()); //validation in here to change to shelf status

                            if (hit.transform.gameObject.GetComponent<Shelf>().GetShelfFullStatus() == false)
                            {
                                holdingObject.GetComponent<CardboardBox>().SubtractFromBox();
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(playerObjectInteractSecondaryKey)) //if statement to remove items from shelf and add it to the box if they can
        {
            RaycastHit hit;

            if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, interactRange))
            {
                if (holdingObject != null)
                {
                    if (hit.transform.gameObject.tag == "shelfSign")
                    {
                        //check if item can be added to box
                        if (holdingObject.GetComponent<CardboardBox>().GetItem() == null || holdingObject.GetComponent<CardboardBox>().GetItem() == hit.transform.gameObject.GetComponent<Shelf>().GetItem())
                        {
                            holdingObject.GetComponent<CardboardBox>().AddToBox(hit.transform.gameObject.GetComponent<Shelf>().GetItem());
                            hit.transform.gameObject.GetComponent<Shelf>().RemoveItemsFromShelf(1);
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(playerObjectDropKey) && holdingObject != null)
        {
            DropObject();
        }

        if (Input.GetKeyDown(playerObjectThrowKey) && holdingObject != null)
        {
            ThrowObject();
        }
    }

    private void PickUpObject(GameObject objectToPickUp)
    {
        holdingObject = objectToPickUp; //assigns the picked up object to holding object so it can be easily interacted with 
        holdingObject.transform.parent = holdPos; //sets parent
        holdingObject.transform.localPosition = new Vector3(0, 0, 0); //sets object to position of heldObjPos
        holdingObject.GetComponent<Rigidbody>().isKinematic = true;

        EventManager.OnPickUpObject(); //call to do some other stuff like ui shit
    }

    private void DropObject()
    {
        holdingObject.GetComponent<Rigidbody>().isKinematic = false;
        holdingObject.transform.parent = null;
        EventManager.OnDropObject();
        holdingObject = null;
    }

    private void ThrowObject()
    {
        holdingObject.GetComponent<Rigidbody>().isKinematic = false;
        holdingObject.transform.parent = null;
        EventManager.OnDropObject();
        holdingObject.GetComponent<Rigidbody>().AddForce(transform.forward * 500f); //throw for the shits and giggles
        holdingObject = null;
    }
}
