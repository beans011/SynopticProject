using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float movementSpeed;
    private CharacterController characterController;
    [SerializeField] private float mass;
    private Vector3 velocity;
    [SerializeField] private float jumpSpeed;
    private bool canMove = true;
    private bool isMenuOpen = false;

    [Header("Movement Keybinds")]
    [SerializeField] private KeyCode playerJump;

    [Header("Menu Keybinds")]
    [SerializeField] private KeyCode playerPauseButtonKey;
    [SerializeField] private KeyCode playerMenuButtonKey;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        GameManager.SetPlayerCursorLocked(true); //call to lock player cursor to to center

        EventManager.GamePause += SetCanMoveFalse;

        EventManager.GameResume += SetCanMoveTrue;

        EventManager.OpenTabMenu += SetMenuOpenTrue;
        EventManager.OpenTabMenu += SetCanMoveFalse;

        EventManager.CloseTabMenu += SetCanMoveTrue;
        EventManager.CloseTabMenu += SetMenuOpenFalse;

        EventManager.OpenCheatConsole += SetCanMoveFalse;

        EventManager.CloseCheatConsole += SetCanMoveTrue;
    }

    void Update()
    {
        PlayerPaused();
        UpdateGravity();
        PlayerMove();        
    }

    private void PlayerMove()
    {
        if (canMove == true)
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

        if (Input.GetKeyDown(playerMenuButtonKey) && Time.timeScale == 1.0f)
        {
            if (isMenuOpen == true)
            {
                EventManager.OnCloseTabMenu();
            }

            else if (isMenuOpen == false)
            {
                EventManager.OnOpenTabMenu();
            }
        }
    }

    private void SetCanMoveTrue()
    {
        canMove = true;
    }

    private void SetCanMoveFalse()
    {
        canMove = false;
    }

    private void SetMenuOpenTrue() { isMenuOpen = true; }

    private void SetMenuOpenFalse() { isMenuOpen = false; }

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
}
