using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductFactory : MonoBehaviour
{
    public static ProductFactory instance;

    [SerializeField] private Item[] items; //list of all items to easily put them in in the inspector
    [SerializeField] private GameObject cardboardBox; //set the prefab cardboard box here
    [SerializeField] private Transform spawnLocation; //setup the spawn location for the box eg where the delivery driver puts

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        InitaliseItemDictionary();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) //test code 
        {
            SpawnBoxOfItems(1);
        }
    }

    public void InitaliseItemDictionary()
    {
        for (int i = 0; i < items.Length; i++)
        {
            ShopStats.AddShopItem(items[i]);
        }
    }

    public void SpawnBoxOfItems(int itemID)
    {
        if (!ShopStats.ShopItemExists(itemID))
        {
            Debug.LogError($"NO CANNY FIND {itemID}");
            return;
        }

        Item item = ShopStats.GetItemData(itemID);

        //Debug.Log(ShopStats.ShopItemExists(1)); //test code
        //Debug.Log(item.productID + " " + item.productName); //TESTING CODE

        //SPAWN CODE FOR BOXES HERE
        GameObject newBox = Instantiate(cardboardBox, spawnLocation) as GameObject;
        CardboardBox cardboardBoxScript = newBox.GetComponent<CardboardBox>();
        cardboardBoxScript.SetUpBox(item);
    }
}
