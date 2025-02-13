using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ProductFactory : MonoBehaviour
{
    public static ProductFactory instance;

    [SerializeField] private Item[] items; //list of all items to easily put them in in the inspector
    [SerializeField] private ShopShelf[] shelves; //list of of all shelves to put into dictionary - why cant unity like dictionaries
    [SerializeField] private GameObject cardboardBox; //set the prefab cardboard box here
    [SerializeField] private GameObject cardboardBoxShelf;
    [SerializeField] private Transform deliveryLocation; //setup the spawn location for the box eg where the delivery driver puts

    private Dictionary<int, ShopShelf> shelvesDict = new Dictionary<int, ShopShelf>();

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
        InitaliseShelvesDictionary();
    }

    public void InitaliseItemDictionary()
    {
        for (int i = 0; i < items.Length; i++)
        {
            ShopStats.AddShopItem(items[i]);
        }
    }

    private void InitaliseShelvesDictionary()
    {
        for (int i = 0; i < shelves.Length; i++)
        {
            shelvesDict.Add(shelves[i].shelfID, shelves[i]);
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

        GameObject newBox = Instantiate(cardboardBox, deliveryLocation) as GameObject;
        CardboardBox cardboardBoxScript = newBox.GetComponent<CardboardBox>();
        cardboardBoxScript.SetUpBox(item);
    }

    public void SpawnBoxOfShelf(int itemID)
    {
        if (!shelvesDict.ContainsKey(itemID))
        {
            Debug.LogError($"NO CANNY FIND {itemID}");
            return;
        }

        ShopShelf shopShelf = shelvesDict[itemID];

        Debug.Log(shopShelf.shelfID + " " + shopShelf.shelfName);

        GameObject newBox = Instantiate(cardboardBoxShelf, deliveryLocation) as GameObject;
        CardboardBoxShelf cardboardBoxScript = newBox.GetComponent<CardboardBoxShelf>();
        cardboardBoxScript.SetupBox(shopShelf);
    }
}

