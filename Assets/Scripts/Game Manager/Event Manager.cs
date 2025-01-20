using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #region UIEvents
    public static event Action GamePause;

    public static event Action GameResume;

    public static void OnGamePause()
    {
        GamePause?.Invoke();
    }

    public static void OnGameResume()
    {
        GameResume?.Invoke();
    }
    #endregion

    #region ObjectInteractionEvents
    public static event Action PickUpObject;
    public static event Action DropObject;

    public static void OnPickUpObject()
    {
        PickUpObject?.Invoke();
    }
    public static void OnDropObject()
    {
        DropObject?.Invoke();
    }
    #endregion
}
