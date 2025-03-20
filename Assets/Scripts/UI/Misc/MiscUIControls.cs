using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscUIControls : MonoBehaviour
{
    private void Start()
    {
        EventManager.OpenCheatConsole += SetNotActive;
        EventManager.CloseCheatConsole += SetActive;

        EventManager.OpenTabMenu += SetNotActive;
        EventManager.CloseTabMenu += SetActive;
    }

    private void SetActive()
    {
        gameObject.SetActive(true);
    }

    private void SetNotActive()
    {
        gameObject.SetActive(false);
    }
}
