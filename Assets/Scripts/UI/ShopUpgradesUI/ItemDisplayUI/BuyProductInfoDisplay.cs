using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyProductInfoDisplay : MonoBehaviour
{
    [Header("Product Buying Stuff")]
    [SerializeField] private Item item;
    [SerializeField] private GameObject itemImageDisplay;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    [Header("Unlock Product Stuff")]
    [SerializeReference] private GameObject itemUnlockThing;
    [SerializeField] private TextMeshProUGUI levelRequiredText;
    [SerializeField] private TextMeshProUGUI licenseCostText;

    void Start()
    {
        ConfigureItemDisplay();
    }

    private void ConfigureItemDisplay()
    {
        if (item != null) 
        {
            //Product Buying Stuff
            itemImageDisplay.GetComponent<UnityEngine.UI.Image>().sprite = item.productImage;
            itemNameText.text = item.productName;
            itemDescriptionText.text = item.productDescription;
            itemPriceText.text = "£" + item.deliveryCost.ToString();

            //Unlock Product Stuff
            levelRequiredText.text = "Level Required: " + item.levelRequirement.ToString();
            licenseCostText.text = "Cost: £" + item.liscenseCost.ToString();
        }

        else
        {
            Debug.LogError("ERROR: NO ITEM IN ITEM DISPLAY " + gameObject.name);
        }
    }

    public void BuyProductLicense()
    {
        if (ShopStats.GetMoney() >= item.liscenseCost && ShopStats.GetShopLevel() >= item.levelRequirement)
        {
            ShopStats.RemoveMoney(item.liscenseCost);
            itemUnlockThing.SetActive(false);
        }

        else
        {
            //do thing here like play a sound or a little popup saying cant buy item
            Debug.Log("NOOOOOOO U CANT BUY IT: LICENSE IS TOO MUCH");
            UIController.instance.DisplayErrorMessage("Not enough money to buy");
        }
    }

    public void BuyItem()
    {
        if (ShopStats.GetMoney() >= item.deliveryCost)
        {
            ProductFactory.instance.SpawnBoxOfItems(item.productID);
        }
    }
}
