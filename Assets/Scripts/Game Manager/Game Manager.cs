using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Timer stuff
    [SerializeField] private int startHour;
    [SerializeField] private int startMinute;
    [SerializeField] private int dayDuration; //IDK day is 10 min for now

    private float elapsedTime = 0f;
    private bool isCountingTime = false;
    private string timeDisplayText; 

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
        EventManager.GamePause += UnlockPlayerCursor;

        EventManager.GameResume += LockPlayerCursor;
        EventManager.GameResume += ResumeGame;

        EventManager.OpenTabMenu += UnlockPlayerCursor;

        EventManager.CloseTabMenu += LockPlayerCursor;

        EventManager.OpenCheatConsole += UnlockPlayerCursor;

        EventManager.CloseCheatConsole += LockPlayerCursor;

        ConfigureTime();
    }

    private void Update()
    {       
        if (isCountingTime == true) 
        {
            TimerCounting();
        }        
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

    public void UnlockPlayerCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void LockPlayerCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    private void TimerCounting()
    {
        elapsedTime += Time.deltaTime;

        float dayProgress = Mathf.Clamp01(elapsedTime / dayDuration);
        int totalInGameMinutes = Mathf.RoundToInt(dayProgress * (10 * 60));

        int currentHour = startHour + (totalInGameMinutes / 60);
        int currentMinute = startMinute + (totalInGameMinutes % 60);

        if (elapsedTime >= dayDuration)
        {
            isCountingTime = false;
            currentHour = startHour + 10;
            currentMinute = 0;
        }

        timeDisplayText = $"{currentHour:D2}:{currentMinute:D2}";

        EventManager.OnUpdateTimeDisplay();
    }

    private void ConfigureTime()
    {
        timeDisplayText = $"{startHour:D2}:{startMinute:D2}";

        EventManager.OnUpdateTimeDisplay();
    }

    public string GetTimeString()
    {
        return timeDisplayText;
    }

    public void SetIsTimerCounting(bool eh)
    {
        isCountingTime = eh;
    }
}
