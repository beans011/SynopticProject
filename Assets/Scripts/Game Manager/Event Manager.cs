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

    public static Action SetShopOpen;
    public static void OnSetShopOpen()
    {
        SetShopOpen?.Invoke();
    }

    #region UIEvents
    public static event Action GamePause;
    public static event Action GameResume;
    public static event Action OpenTabMenu;
    public static event Action CloseTabMenu;
    public static event Action OpenCheatConsole;
    public static event Action CloseCheatConsole;
    public static event Action UpdateTimeDisplay;
    public static event Action UpdatePriceDisplays;
    public static event Action NothingHeldStateUI;
    public static event Action HoldingItemBoxStateUI;
    public static event Action HoldingShelfBoxOpenStateUI;
    public static event Action HoldingShelfBoxClosedStateUI;
    public static event Action DiableKeyBindUI;
    public static event Action SetActiveBinBoxUI;
    public static event Action SetActiveOpenSignUI;
    public static event Action UpdateMoneyDisplays;
    public static event Action UpdateXPDisplays;
    public static event Action UpdateShopNameDisplay;
    public static event Action SomeRandomEvent;

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
    public static void OnOpenCheatConsole()
    {
        OpenCheatConsole?.Invoke();
    }
    public static void OnCloseCheatConsole() 
    { 
        CloseCheatConsole?.Invoke();
    }
    public static void OnUpdateTimeDisplay()
    { 
        UpdateTimeDisplay?.Invoke();
    }
    public static void OnUpdatePriceDisplays()
    {
        UpdatePriceDisplays?.Invoke();
    }
    public static void OnNothingHeldStateUI()
    {
        NothingHeldStateUI?.Invoke();
    }
    public static void OnHoldingItemBoxStateUI()
    {
        HoldingItemBoxStateUI?.Invoke();
    }
    public static void OnHoldingShelfBoxOpenStateUI()
    {
        HoldingShelfBoxOpenStateUI?.Invoke();
    }
    public static void OnHoldingShelfBoxClosedStateUI()
    {
        HoldingShelfBoxClosedStateUI?.Invoke();
    }
    public static void OnDisableKeyBindUI()
    {
        DiableKeyBindUI?.Invoke();
    }
    public static void OnSetActiveBinBoxUI()
    {
        SetActiveBinBoxUI?.Invoke();
    }
    public static void OnSetActiveOpenSignUI()
    {
        SetActiveOpenSignUI?.Invoke();
    }
    public static void OnUpdateMoneyDisplays()
    {
        UpdateMoneyDisplays?.Invoke();
    }
    public static void OnUpdateXPDisplays()
    {
        UpdateXPDisplays?.Invoke();
    }
    public static void OnUpdateShopNameDisplay()
    {
        UpdateShopNameDisplay?.Invoke();
    }
    public static void OnSomeRandomEvent()
    {
        SomeRandomEvent?.Invoke();
    }
    #endregion

    #region ObjectInteractionEvents
    public static event Action PickUpObject;
    public static event Action DropObject;
    public static event Action CardboardBoxInteract;
    public static event Action CardboardBoxInteractSecondary;
    public static event Action PlacedShelf;

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
    public static void OnPlacedShelf()
    {
        PlacedShelf?.Invoke();
    }
    #endregion
}
