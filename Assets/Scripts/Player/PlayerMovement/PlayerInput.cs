using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //based of this yt video https://www.youtube.com/watch?v=HHzQMYxtmU4

    [Header("Movement Keybinds")]
    [SerializeField] private KeyCode playerJump;

    [Header("Object Interact Keybinds")]
    [SerializeField] private KeyCode playerObjectInteractKey;
    [SerializeField] private KeyCode playerObjectInteractSecondaryKey;
    [SerializeField] private KeyCode playerObjectDropKey;
    [SerializeField] private KeyCode playerObjectThrowKey;

    [Header("Menu Keybinds")]
    [SerializeField] private KeyCode playerPauseButtonKey;
    [SerializeField] private KeyCode playerMenuButtonKey;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    private Vector2 playerLook;
    private bool lockCameraMovement;

    [Header("Player")]
    [SerializeField] private float movementSpeed;
    private CharacterController characterController;
    [SerializeField] private float mass;
    private Vector3 velocity;
    [SerializeField] private float jumpSpeed;

    [Header("Object Interaction")]
    [SerializeField] private Transform holdPos;
    [SerializeField] private float interactRange;
    private GameObject holdingObject;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        GameManager.SetPlayerCursorLocked(true); //call to lock player cursor to to center
        EventManager.GamePause += LockCameraMovement;
        EventManager.GameResume += UnlockCameraMovement;
    }

    private void Update()
    {
        PlayerPaused(); //event to pause game
        UpdateGravity(); //manual gravity cause why not
        CameraLook(); //player look around
        PlayerMove(); //player move around
        PlayerPickUpBoxes(); //playe can pick up and drop boxes
    }

    private void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;

        if (characterController.isGrounded)
        {
            velocity.y = -1f;
        }
        else
        {
            velocity.y += gravity.y;
        }
    }

    private void CameraLook()
    {
        if (lockCameraMovement == false)
        {
            playerLook.x += Input.GetAxis("Mouse X") * Settings.GetMouseSensitivity();
            playerLook.y += Input.GetAxis("Mouse Y") * Settings.GetMouseSensitivity();

            playerLook.y = Mathf.Clamp(playerLook.y, -80.0f, 80.0f);

            cameraTransform.localRotation = Quaternion.Euler(-playerLook.y, 0, 0);
            transform.localRotation = Quaternion.Euler(0, playerLook.x, 0);
        }        
    }

    private void LockCameraMovement()
    {
        lockCameraMovement = true;
    }

    private void UnlockCameraMovement()
    {
        lockCameraMovement = false;
    }

    private void PlayerMove()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);

        if (Input.GetKeyDown(playerJump) && characterController.isGrounded)  
        {
            velocity.y = jumpSpeed;
        }

        characterController.Move((input * movementSpeed + velocity) * Time.deltaTime);
    }

    private void PlayerPaused()
    {
        if (Input.GetKeyDown(playerPauseButtonKey))
        {
            if (Time.timeScale == 0.0f)
            {
                EventManager.OnGameResume();
            }

            else if (Time.timeScale == 1.0f) 
            { 
                EventManager.OnGamePause();
            }
        }
    }

    private void PlayerPickUpBoxes()
    {
        if (Input.GetKeyDown(playerObjectInteractKey))
        {
            if (holdingObject == null) 
            {             
                RaycastHit hit;
               
                if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, interactRange))
                {               
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        PickUpObject(hit.transform.gameObject);                       
                    }
                }
            }

            if (holdingObject != null) //add here that need to be in correct are to interact
            {
                EventManager.OnCardboardBoxInteract();
                Debug.Log("U ARE INTERACTING WITH THE OBJECT");
            }         
        }
        
        if (Input.GetKeyDown(playerObjectInteractSecondaryKey) && holdingObject != null) //add in here that neeed to be in correct are to interact
        {
            EventManager.OnCardboardBoxInteractSecondary();
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

        if (holdingObject.GetComponent<CardboardBox>() != null) //check if held object has components for event attaching  
        {
            holdingObject.GetComponent<CardboardBox>().AttachEvents();
        }
        //add more of these when more interaction objects get added

        EventManager.OnPickUpObject(); //call to do some other stuff like ui shit
    }

    private void DropObject()
    {
        DropObjectEventDetachment();
        holdingObject.GetComponent<Rigidbody>().isKinematic = false;
        holdingObject.transform.parent = null;
        EventManager.OnDropObject();
        holdingObject = null;
    }

    private void ThrowObject()
    {
        DropObjectEventDetachment();
        holdingObject.GetComponent<Rigidbody>().isKinematic = false;
        holdingObject.transform.parent = null;       
        EventManager.OnDropObject();
        holdingObject.GetComponent<Rigidbody>().AddForce(transform.forward * 500f); //throw for the shits and giggles
        holdingObject = null;
    }

    private void DropObjectEventDetachment()
    {
        if (holdingObject.GetComponent<CardboardBox>() != null) //check if held object has components for event attaching  
        {
            holdingObject.GetComponent<CardboardBox>().DetachEvents();
        }
    }
}
