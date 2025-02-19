using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyProductInfoController : MonoBehaviour
{
    [SerializeField] private Item item;
    private BuyProductInfoView view;

    private void Start()
    {
        view = gameObject.GetComponent<BuyProductInfoView>();

        SetupItem();
    }

    private void SetupItem()
    {
        if (item != null)
        {
            view.UpdateProductUI(item);
            view.UpdateUnlockUI(item);
            view.ToggleUnlockPanel(IsItemUnlocked());
        }
        else
        {
            Debug.LogError("u stoopid no reference here " + gameObject.name);
        }
    }

    public void BuyProductLicense()
    {
        if (ShopStats.GetMoney() >= item.liscenseCost && ShopStats.GetShopLevel() >= item.levelRequirement)
        {
            ShopStats.RemoveMoney(item.liscenseCost);
            view.ToggleUnlockPanel(true);
        }
        else
        {
            Debug.Log("NOOOOOO U CANT DO THAT U NO MONEY OR NOT ENOUGH LEVEL GUESS");
            UIController.instance.DisplayErrorMessage("Not enough money to buy");
        }
    }

    public void BuyItem()
    {
        if (ShopStats.GetMoney() >= item.deliveryCost)
        {
            ProductFactory.instance.SpawnBoxOfItems(item.productID);
            ShopStats.RemoveMoney(item.deliveryCost);
        }

        else 
        {
            Debug.Log("u broke");
            UIController.instance.DisplayErrorMessage("Not enough money to buy");
        }
    }

    private bool IsItemUnlocked()
    {
        return ShopStats.GetShopLevel() >= item.levelRequirement;
    }
}
