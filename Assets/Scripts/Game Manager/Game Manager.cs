using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }          
    }

    private void Start()
    {
        EventManager.GamePause += PauseGame;
        EventManager.GameResume += ResumeGame;
    }

    public static bool GetPlayerCursorLocked()
    {
        if (Cursor.lockState == CursorLockMode.Locked) 
        {
            return true;
        }

        return false;
    }

    public static void SetPlayerCursorLocked(bool setBool) 
    {  
        if (setBool == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        else if (setBool == false) 
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    public static void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }
}
