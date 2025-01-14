using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameObject("GameManager").AddComponent<GameManager>();
            instance.Initialise();
        }

        return instance;
    }

    private void Initialise()
    {
        //stuff for on start up here
    }

    private static bool playerCursorLocked = true; //set player cursor to be hidden or usable

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
    
}
