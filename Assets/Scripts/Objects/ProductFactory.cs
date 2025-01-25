using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductFactory : MonoBehaviour
{
    public static ProductFactory instance;

    [SerializeField] private Item[] items; //list of all items to easily put them in in the inspector

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
            SpawnBoxOfItems(0);
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
        Item item = ShopStats.GetItemData(itemID);

        Debug.Log(ShopStats.ShopItemExists(0));
        Debug.Log(item.productID + " " + item.productName); //TESTING CODE

        //SPAWN CODE FOR BOXES HERE
        //ONCE WRITTEN EXPLAIN IT IN THE SYSTEM EXPLANATION WORD FILE
    }
}
