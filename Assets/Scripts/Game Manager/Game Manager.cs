using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Timer Stuff")]
    [SerializeField] private int startHour;
    [SerializeField] private int startMinute;
    [SerializeField] private int dayDuration; //IDK day is 10 min for now
    private int orgStartHour;
    private int orgStartMinute;
    private int lastSpawnMinute = -1;

    private float elapsedTime = 0f;
    private bool isShopOpen = false;
    private bool isGameRunning = false;
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
        orgStartHour = startHour;
        orgStartMinute = startMinute;

        StartGameDay();

        EventManager.GamePause += PauseGame;
        EventManager.GamePause += UnlockPlayerCursor;

        EventManager.GameResume += LockPlayerCursor;
        EventManager.GameResume += ResumeGame;

        EventManager.OpenTabMenu += UnlockPlayerCursor;

        EventManager.CloseTabMenu += LockPlayerCursor;

        EventManager.OpenCheatConsole += UnlockPlayerCursor;

        EventManager.CloseCheatConsole += LockPlayerCursor;

        EventManager.SetShopOpen += SetIsShopOpen;

        ConfigureTime();
    }

    private void Update()
    {       
        if (isGameRunning)
        {
            if (isShopOpen == true)
            {
                TimerCounting();
            }
        }             
    }

    public void StartGameDay()
    {
        startHour = orgStartHour;
        startMinute = orgStartMinute;
        ConfigureTime();

        isGameRunning = true;
    }

    public void EndGameDay() 
    { 
        isGameRunning = false;
    }

    //VERY IMPORTANT: use stuff for big important stuff that rely on the shop being open and day to day runnings
    public bool GetIsShopOpen()
    {
        return isShopOpen;
    }

    public void SetIsShopOpen()
    {
        isShopOpen = !isShopOpen;
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

    #region timer stuff
    private void TimerCounting()
    {
        elapsedTime += Time.deltaTime;

        float dayProgress = Mathf.Clamp01(elapsedTime / dayDuration);
        int totalInGameMinutes = Mathf.RoundToInt(dayProgress * (10 * 60));

        int currentHour = startHour + (totalInGameMinutes / 60);
        int currentMinute = startMinute + (totalInGameMinutes % 60);

        if ((currentMinute == 0 || currentMinute == 30) && currentMinute != lastSpawnMinute) //spawning npc shenanigans
        {
            SpawnWalkerNPC();
            SpawnShopperNPC();
            lastSpawnMinute = currentMinute;
        }

        if (elapsedTime >= dayDuration)
        {
            EndGameDay();
            isShopOpen = false;
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
    #endregion

    #region NPC stuff
    private void SpawnWalkerNPC()
    {
        NPCSpawner.instance.SpawnWalkingNPC();      
    }

    private void SpawnShopperNPC()
    {
        NPCSpawner.instance.SpawnShopperNPC();
    }
    #endregion
}
