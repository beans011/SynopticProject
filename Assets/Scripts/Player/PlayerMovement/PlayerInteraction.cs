using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Object Interact Keybinds")]
    [SerializeField] public KeyCode playerObjectInteractKey;
    [SerializeField] public KeyCode playerObjectInteractSecondaryKey;
    [SerializeField] public KeyCode playerObjectDropKey;
    [SerializeField] public KeyCode playerObjectThrowKey;

    [Header("Object Interaction")]
    [SerializeField] public Transform holdPos;
    [SerializeField] public Transform shelfHeldPos;
    [SerializeField] public float interactRange;
    [HideInInspector] public GameObject holdingObject { get; private set;}

    [Header("Camera")]
    [SerializeField] public Transform cameraTransform;

    private IInteractionState currentState;
    private bool inMenu = false;

    private void Start()
    {
        EventManager.PlacedShelf += DestroyCurrentHeldBox;
        EventManager.OpenTabMenu += SetInMenuTrue;
        EventManager.CloseTabMenu += SetInMenuFalse;
        EventManager.OpenCheatConsole += SetInMenuTrue;
        EventManager.CloseCheatConsole += SetInMenuFalse;
        
        currentState = new NothingHeldState();
    }

    private void Update()
    {
        if (!inMenu)
        {
            currentState.HandleInput(this);
        }
    }

    public void SetState(IInteractionState newState)
    {
        Debug.Log($"Switched to state: {newState.GetType().Name}");
        currentState = newState;
    }

    /*
    private void NothingHeldState()
    {
        if (Input.GetKeyDown(playerObjectInteractKey))
        {
            RaycastHit hit;

            if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, interactRange))
            {
                if (holdingObject == null && hit.transform.gameObject.tag == "canPickUp") //picking up box
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

        if (Input.GetKeyDown(playerObjectDropKey) && holdingObject != null)
        {
            DropObject();
        }

        if (Input.GetKeyDown(playerObjectThrowKey) && holdingObject != null)
        {
            ThrowObject();
        }
    }

    private void HoldingShelfBoxState()
    {
        CardboardBoxShelf shelfBox = holdingObject.GetComponent<CardboardBoxShelf>();

        switch (boxOpen)
        {
            case 0:
                if (shelfBox.IsPreviewShelfMade() == true)
                {
                    shelfBox.DestroyPreviewShelf();
                }

                if (Input.GetKeyDown(playerObjectInteractKey) && holdingObject != null) //open tha box
                {
                    boxOpen = 1;
                    holdingObject.SetActive(false);
                }

                if (Input.GetKeyDown(playerObjectDropKey) && holdingObject != null)
                {
                    DropObject();
                }

                if (Input.GetKeyDown(playerObjectThrowKey) && holdingObject != null)
                {
                    ThrowObject();
                }
                break; 

            case 1:

                if (shelfBox != null && shelfBox.IsPreviewShelfMade() == false)
                {
                    shelfBox.SpawnPreviewShelf(shelfBox.GetRotation());
                }

                shelfBox.HandlePreviewPosition(cameraTransform, interactRange, shelfHeldPos.transform.position);

                if (Input.GetKeyDown(playerObjectInteractKey))
                {
                    shelfBox.PlaceShelf();
                    boxOpen = 0;
                    stateNo = 0;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    shelfBox.RotateShelf(-45f);
                }

                if (Input.GetKeyDown(playerObjectThrowKey))
                {
                    shelfBox.RotateShelf(45f);
                }

                if (Input.GetKeyDown(KeyCode.Mouse1)) //close tha box
                {
                    boxOpen = 0;
                    holdingObject.SetActive(true);
                }

                break;
        }               
    }
    */
    public void PickUpObject(GameObject objectToPickUp)
    {
        holdingObject = objectToPickUp; //assigns the picked up object to holding object so it can be easily interacted with 
        holdingObject.transform.parent = holdPos; //sets parent
        holdingObject.transform.localPosition = new Vector3(0, 0, 0); //sets object to position of heldObjPos
        holdingObject.GetComponent<Rigidbody>().isKinematic = true;

        EventManager.OnPickUpObject(); //call to do some other stuff like ui shit
    }

    public void DropObject()
    {
        holdingObject.GetComponent<Rigidbody>().isKinematic = false;
        holdingObject.transform.parent = null;
        EventManager.OnDropObject();
        holdingObject = null;
        SetState(new NothingHeldState());
    }

    public void ThrowObject()
    {
        holdingObject.GetComponent<Rigidbody>().isKinematic = false;
        holdingObject.transform.parent = null;
        EventManager.OnDropObject();
        holdingObject.GetComponent<Rigidbody>().AddForce(transform.forward * 500f); //throw for the shits and giggles
        holdingObject = null;
        SetState(new NothingHeldState());
    }   

    public void DestroyCurrentHeldBox()
    {
        Destroy(holdingObject);
    }

    public void SetInMenuFalse()
    {
        inMenu = false;
    }

    public void SetInMenuTrue() 
    {
        inMenu = true; 
    }
}
