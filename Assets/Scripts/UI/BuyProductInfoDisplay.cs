using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyProductInfoDisplay : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private GameObject itemImageDisplay;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    // Start is called before the first frame update
    void Start()
    {
        ConfigureItemDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConfigureItemDisplay()
    {
        if (item != null) 
        {
            itemImageDisplay.GetComponent<UnityEngine.UI.Image>().sprite = item.productImage;
            itemNameText.text = item.productName;
            itemDescriptionText.text = item.productDescription;
            itemPriceText.text = "£" + item.deliveryCost.ToString();
        }

        else
        {
            Debug.LogError("ERROR: NO ITEM IN ITEM DISPLAY " + gameObject.name);
        }
    }
}
