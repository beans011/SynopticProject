using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClipBoard_controller : MonoBehaviour
{
    private ClipBoard_View view;
    [SerializeField] private Item itemOnShelf = null;
    [SerializeField] private GameObject shelfSelected = null;

    [SerializeField] private TMP_InputField playerSetPrice_textInput;

    void Start()
    {
        view = gameObject.GetComponent<ClipBoard_View>();
    }

    public void SetUpClipboardUI(Item item, GameObject shelf)
    {        
        EventManager.OnOpenCheatConsole();

        itemOnShelf = item;
        shelfSelected = shelf;

        shelfSelected.GetComponent<ShelfController>().OpenShelfControlUI();
        Invoke("UpdateClipboardUI", 0.01f); //this is only shitty fix cause it was updating on the same tick so it would activate cause the shelf is stupid
    }

    private void UpdateClipboardUI()
    {
        if (itemOnShelf != null)
        {
            Debug.Log(itemOnShelf.productName);

            string text = itemOnShelf.playerSetPrice.ToString();
            view.SetAvgPriceText(text);

            text = itemOnShelf.marketPrice.ToString();
            view.SetMarketPriceText(text);

            float profit = itemOnShelf.playerSetPrice - itemOnShelf.marketPrice;
            view.SetProfitText(profit.ToString());

            view.SetPricePlaceHolderText((float)Math.Round(itemOnShelf.playerSetPrice, 2));

            view.SetItemImage(itemOnShelf.productImage);
        }

        else
        {
            view.SetAvgPriceText(0.0f.ToString());
            view.SetMarketPriceText(0.0f.ToString());

            view.SetProfitText(0.0f.ToString());

            view.SetPricePlaceHolderText(0.0f);

            view.SetItemImage(null);
        }
    }

    public void FlatPackButton()
    {
        if (shelfSelected != null) 
        {
            shelfSelected.GetComponent<ShelfController>().FlatpackShelf();

            shelfSelected.GetComponent<ShelfController>().CloseShelfControlUI();
            shelfSelected = null;
            itemOnShelf = null;

            gameObject.SetActive(false);

            EventManager.OnCloseCheatConsole();
        }
    }

    public void CloseClipBoardUI()
    {
        //logic in here to get the input from the text field and to make it the playerSetPrice
        string newPrice = playerSetPrice_textInput.text;
        float priceSet = 0.0f;
        bool canConvert = float.TryParse(newPrice, out priceSet);

        if (canConvert == true)
        {
            itemOnShelf.playerSetPrice = (float)Math.Round(priceSet, 2);

            shelfSelected.GetComponent<ShelfController>().CloseShelfControlUI();
            shelfSelected = null;
            itemOnShelf = null;

            gameObject.SetActive(false);

            EventManager.OnCloseCheatConsole();
        }
        else
        {
            //logic for error message stuff here
            Debug.LogError("cant set the price bossman");

            shelfSelected.GetComponent<ShelfController>().CloseShelfControlUI();
            shelfSelected = null;
            itemOnShelf = null;

            gameObject.SetActive(false);

            EventManager.OnCloseCheatConsole();
        }
    }
}
