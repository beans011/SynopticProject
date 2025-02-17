using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClipBoard_View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI avgPrice_text;
    [SerializeField] private TextMeshProUGUI marketPrice_text;
    [SerializeField] private TextMeshProUGUI profit_text;
    [SerializeField] private TextMeshProUGUI pricePlaceholder_text;
    [SerializeField] private Image itemDisplayImage;

    public void SetAvgPriceText(string text)
    {
        avgPrice_text.text = "£: " +  text;
    }

    public void SetMarketPriceText(string text)
    {
        marketPrice_text.text = "£: " + text;
    }

    public void SetProfitText(string text)
    {
        profit_text.text = "£: " + text;
    }

    public void SetPricePlaceHolderText(float price) 
    { 
        pricePlaceholder_text.text = price.ToString();
    }

    public void SetItemImage(Sprite itemSprite)
    {
        itemDisplayImage.sprite = itemSprite;
    }
}

