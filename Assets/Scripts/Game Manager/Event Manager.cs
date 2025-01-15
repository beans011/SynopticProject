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
}
