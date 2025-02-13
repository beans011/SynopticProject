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

    private int stateNo; //0 - nothingHeld, 1 - holdingItemBox, 2 - holdingShelfBox}

    private void Start()
    {
        stateNo = 0;
    }

    private void Update()
    {       
        switch(stateNo)
        {
            case 0:
                NothingHeldState();               
                break;

            case 1:
                HoldingItemBoxState();
                break; 
            
            case 2:
                HoldingShelfBoxState();
                break;
        }

        if (Input.GetKeyDown(playerObjectDropKey) && holdingObject != null)
        {
            DropObject();
        }

        if (Input.GetKeyDown(playerObjectThrowKey) && holdingObject != null)
        {
            ThrowObject();
        }
        Debug.Log(stateNo);
    }

    private void NothingHeldState()
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
                        
                        if (holdingObject.TryGetComponent<CardboardBox>(out CardboardBox cardboardBox))
                        {
                            stateNo = 1;
                        }
                        else if (holdingObject.TryGetComponent<CardboardBoxShelf>(out CardboardBoxShelf cardboardBoxShelf))
                        {
                            stateNo = 2;
                        }
                    }
                }
            }
        }
    }

    private void HoldingItemBoxState()
    {
        if (Input.GetKeyDown(playerObjectInteractKey))
        {
            RaycastHit hit;

            if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, interactRange))
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

        if (Input.GetKeyDown(playerObjectInteractSecondaryKey)) //if statement to remove items from shelf and add it to the box if they can
        {
            RaycastHit hit;

            if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, interactRange))
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

    private void HoldingShelfBoxState()
    {
        if (Input.GetKeyDown(playerObjectInteractKey))
        {
            
        }

        if (Input.GetKeyDown(playerObjectInteractSecondaryKey)) //if statement to remove items from shelf and add it to the box if they can
        {
            
        }
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
        stateNo = 0;
    }

    private void ThrowObject()
    {
        holdingObject.GetComponent<Rigidbody>().isKinematic = false;
        holdingObject.transform.parent = null;
        EventManager.OnDropObject();
        holdingObject.GetComponent<Rigidbody>().AddForce(transform.forward * 500f); //throw for the shits and giggles
        holdingObject = null;
        stateNo = 0;
    }
}
