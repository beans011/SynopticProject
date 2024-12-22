using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Player Movement Keybinds")]
    [SerializeField] private KeyCode playerForwardKey;
    [SerializeField] private KeyCode playerBackwardKey;
    [SerializeField] private KeyCode playerLeftKey;
    [SerializeField] private KeyCode playerRightKey;

    [Header("Object Interact Keybinds")]
    [SerializeField] private KeyCode playerObjectInteractKey;
    [SerializeField] private KeyCode playerObjectDropKey;
    [SerializeField] private KeyCode playerObjectThrowKey;

    [Header("Menu Keybinds")]
    [SerializeField] private KeyCode playerPauseButtonKey;
    [SerializeField] private KeyCode playerMenuButtonKey;

    //Input Events
    //movement events
    public static event Action playerMoveForward;
    public static event Action playerMoveBackward;
    public static event Action playerMoveLeft;
    public static event Action playerMoveRight;

    //object interact events
    public static event Action playerObjectInteract;
    public static event Action playerObjectDrop;
    public static event Action playerObjectThrow;

    //menu events
    public static event Action playerPause;
    public static event Action playerMenu;

    //some player events IDK
    public static void OnPlayerMoveForward() { playerMoveForward?.Invoke(); }
    public static void OnPlayerMoveBackward() { playerMoveBackward?.Invoke(); }
    public static void OnPlayerMoveLeft() { playerMoveLeft?.Invoke(); }
    public static void OnPlayerMoveRight() { playerMoveRight?.Invoke(); }
    public static void OnPlayerObjectInteract() { playerObjectInteract?.Invoke(); }
    public static void OnPlayerObjectDrop() { playerObjectDrop?.Invoke(); }
    public static void OnPlayerThrow() { playerObjectThrow?.Invoke(); }
    public static void OnPlayerPause() { playerPause?.Invoke(); }
    public static void OnPlayerMenu() { playerMenu?.Invoke(); }
}
