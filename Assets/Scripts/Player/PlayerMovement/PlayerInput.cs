using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //based of this yt video https://www.youtube.com/watch?v=HHzQMYxtmU4

    [Header("Movement Keybinds")]
    [SerializeField] private KeyCode playerJump;

    [Header("Object Interact Keybinds")]
    [SerializeField] private KeyCode playerObjectInteractKey;
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
}
