using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStats
{
    //this file is for data to do with the shop eg money, rent, shop upgrades etc

    private static string shopName = "shop";
    private static float money = 0, billsAmount = 0;
    private static float moralityMeter = 0;
    private static int shopXP = 0, shopLevel = 1; 

    //VERY IMPORTANT!!!!!!
    //THE PRODUCT ID MUST MATCH ONE OF THESE IN THIS DICTIONARY JUST FOR EASE OF SAVING THIS FILE LATER ON
    private static Dictionary<int, Item> itemUnlocks = new Dictionary<int, Item>();

    public static string GetShopName() {  return shopName; }
    public static void SetShopName(string newShopName) { shopName = newShopName; }

    public static float GetMoney() { return money; }
    public static void AddMoney(float amount) 
    { 
        money = money + amount;
        Debug.Log(money);
    }
    public static void RemoveMoney(float amount) 
    {  
        money -= amount;
        Debug.Log(money);
    }

    public static float GetBillsAmount() { return billsAmount; }
    public static void SetBillsAmount(float billCost) { billsAmount = billsAmount + billCost; }

    public static float GetMoralityMeter() { return moralityMeter; }
    public static void SetMoralityMeter(float moralityCost) { moralityMeter = moralityMeter + moralityCost; }

    public static int GetShopXP() { return shopXP; }
    public static void SetShopXP(int xpGained) { shopXP = shopXP + xpGained; }

    public static int GetShopLevel() {  return shopLevel; }
    public static void SetShopLevel(int level) 
    {  
        shopLevel = shopLevel + level;

        EventManager.OnShopLevelUp();
    }


    //dictionary helper methods
    public static void AddShopItem(Item item)
    {
        if (!itemUnlocks.ContainsKey(item.productID))
        {
            itemUnlocks.Add(item.productID, item);
            itemUnlocks[item.productID].playerSetPrice = itemUnlocks[item.productID].marketPrice;
            Debug.Log($"Added: {item.productID} with name {item.productName}");
        }

        else
        {
            Debug.Log($"Item {item.productID} already exists");
        }
    }

    public static bool ShopItemExists(int itemID)
    {
        if (itemUnlocks.ContainsKey(itemID)) 
        { 
            return true; 
        }

        else
        {
            return false;
        }
    }

    public static bool? GetShopItemStatus(int itemID)
    {
        if (itemUnlocks.ContainsKey(itemID))
        {
            return itemUnlocks[itemID].itemUnlocked;
        }

        Debug.Log($"Item {itemID} not found");

        return null;
    }

    public static void UpdateShopItemStatus(int itemID, bool newStatus)
    {
        if (itemUnlocks.ContainsKey(itemID))
        {
            itemUnlocks[itemID].itemUnlocked = newStatus;
            Debug.Log($"Updated: {itemID} to status {newStatus}");
        }

        else
        {
            Debug.Log($"Item {itemID} not found");
        }
    }

    public static void PrintAllShopItems()
    {
        Debug.Log("All Items:");

        foreach (var item in itemUnlocks)
        {
            Debug.Log($"- {item.Key}: {item.Value}");
        }
    }

    public static Item GetItemData(int itemID)
    {
        if (itemUnlocks.ContainsKey(itemID))
        {
            return itemUnlocks[itemID];
        }

        else 
        {
            Debug.Log($"Item {itemID} not found");
            return null; 
        }
    }

    public static float GetItemMarketPrice(int itemID)
    {
        if (itemUnlocks.ContainsKey(itemID))
        {
            return itemUnlocks[itemID].marketPrice;
        }

        else
        {
            Debug.Log($"Item {itemID} not found");
            return 0;
        }
    }

    public static void SetItemMarketPrice(int itemID, float newMarketPrice)
    {
        if (itemUnlocks.ContainsKey(itemID))
        {
            itemUnlocks[itemID].marketPrice = newMarketPrice;
            Debug.Log("Set new Item Price: " +  itemID + " = " + itemUnlocks[itemID].marketPrice);
            EventManager.OnUpdatePriceDisplays();
        }

        else
        {
            Debug.Log($"Item {itemID} not found");
        }
    }

    public static float GetItemPlayerSetPrice(int itemID)
    {
        if (itemUnlocks.ContainsKey(itemID))
        {
            return itemUnlocks[itemID].playerSetPrice;
        }

        else
        {
            Debug.Log($"Item {itemID} not found");
            return 0;
        }
    }

    public static void SetItemPlayerSetPrice(int itemID, float newPlayerSetPrice)
    {
        if (itemUnlocks.ContainsKey(itemID))
        {
            itemUnlocks[itemID].marketPrice = newPlayerSetPrice;
            Debug.Log("Set new Player Set Price: " + itemID + " = " + itemUnlocks[itemID].playerSetPrice);
        }

        else
        {
            Debug.Log($"Item {itemID} not found");
        }
    }
}
