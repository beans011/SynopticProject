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

    public static event Action ShopLevelUp;
    
    //CALL THIS ONE ON SHOP LEVEL UP
    public static void OnShopLevelUp()
    {
        ShopLevelUp?.Invoke();
    }

    #region UIEvents
    public static event Action GamePause;

    public static event Action GameResume;

    public static event Action OpenTabMenu;

    public static event Action CloseTabMenu;

    public static event Action UpdateTimeDisplay;

    public static void OnGamePause()
    {
        GamePause?.Invoke();
    }

    public static void OnGameResume()
    {
        GameResume?.Invoke();
    }

    public static void OnOpenTabMenu()
    {
        OpenTabMenu?.Invoke();
    }

    public static void OnCloseTabMenu() 
    {
        CloseTabMenu?.Invoke();
    }

    public static void OnUpdateTimeDisplay()
    { 
        UpdateTimeDisplay?.Invoke();
    }
    #endregion

    #region ObjectInteractionEvents
    public static event Action PickUpObject;
    public static event Action DropObject;
    public static event Action CardboardBoxInteract;
    public static event Action CardboardBoxInteractSecondary;

    public static void OnPickUpObject()
    {
        PickUpObject?.Invoke();
    }
    public static void OnDropObject()
    {
        DropObject?.Invoke();
    }
    public static void OnCardboardBoxInteract()
    {
        CardboardBoxInteract?.Invoke();
    }

    public static void OnCardboardBoxInteractSecondary()
    {
        CardboardBoxInteractSecondary?.Invoke();
    }
    #endregion
}
