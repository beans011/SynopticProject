using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyProductInfoView : MonoBehaviour
{
    [Header("Product Buying Stuff")]
    [SerializeField] private Image itemImageDisplay;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    [Header("Unlock Product Stuff")]
    [SerializeField] private GameObject itemUnlockThing;
    [SerializeField] private TextMeshProUGUI levelRequiredText;
    [SerializeField] private TextMeshProUGUI licenseCostText;

    public void UpdateProductUI(Item item)
    {
        itemImageDisplay.sprite = item.productImage;
        itemNameText.text = item.productName;
        itemDescriptionText.text = item.productDescription;
        itemPriceText.text = "£" + item.deliveryCost.ToString("F2");
    }

    public void UpdateUnlockUI(Item item) 
    { 
        levelRequiredText.text = "Level Required: " +  item.levelRequirement.ToString();
        licenseCostText.text = "Cost: £ " + item.liscenseCost.ToString("F2"); 
    }

    public void ToggleUnlockPanel(bool isUnlocked)
    {
        itemUnlockThing.SetActive(!isUnlocked);
    }
}
