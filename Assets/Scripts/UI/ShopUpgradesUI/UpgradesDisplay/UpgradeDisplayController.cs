using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDisplayController : MonoBehaviour
{
    private UpgradeDisplayModel model;
    private UpgradeDisplayView view;

    private void Start()
    {
        view = gameObject.GetComponent<UpgradeDisplayView>();
        model = gameObject.GetComponent<UpgradeDisplayModel>();

        EventManager.ShopLevelUp += CheckUnlockThing;

        SetupView();
    }

    private void SetupView()
    {
        if (model != null && view != null) 
        { 
            view.UpdateBuyUI(model.GetSpaceCost());
            view.UpdateUnlockUI(model.GetLevelRequirement());
            view.ToggleUnlockThing(IsItemUnlocked());
        }
        else
        {
            Debug.Log("the model and view aint there");
        }
    }

    public void BuySpace()
    {
        if (model.GetIsBought() == false && ShopStats.GetMoney() >= model.GetSpaceCost()) 
        { 
            ShopStats.RemoveMoney(model.GetSpaceCost());
            model.SetIsBought(true);
            model.ObjectObliteration();
        }
        else
        {
            UIController.instance.DisplayErrorMessage("Not enough money to buy");
        }
    }

    public void CheckUnlockThing()
    {
        view.ToggleUnlockThing(IsItemUnlocked());
    }

    private bool IsItemUnlocked()
    {
        return ShopStats.GetShopLevel() >= model.GetLevelRequirement();
    }
}
