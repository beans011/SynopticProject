using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    private Vector2 playerLook;
    private bool lockCameraMovement;

    void Start()
    {
        EventManager.GamePause += LockCameraMovement;

        EventManager.GameResume += UnlockCameraMovement;

        EventManager.OpenTabMenu += LockCameraMovement;

        EventManager.CloseTabMenu += UnlockCameraMovement;
    }

    void Update()
    {
        CameraLook();
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
}
