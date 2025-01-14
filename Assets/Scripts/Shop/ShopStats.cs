using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStats
{
    //this file is for data to do with the shop eg money, rent, shop upgrades etc

    private static string shopName;
    private static float money;
    private static Dictionary<string, bool> shopUnlocks = new Dictionary<string, bool>() 
    {
        //items go in here and put default bool of false
        {"Fiend Energy Drink", false},

    };

    public static string GetShopName() {  return shopName; }
    public static void SetShopName(string newShopName) { shopName = newShopName; }

    public static float GetMoney() { return money; }
    public static void SetMoney(float cost) { money = money - cost; }

    public static void AddShopItem(string itemName, bool status)
    {
        if (!shopUnlocks.ContainsKey(itemName))
        {
            shopUnlocks[itemName] = status;
            Console.WriteLine($"Added: {itemName} with status {status}");
        }

        else
        {
            Console.WriteLine($"Item {itemName} already exists.");
        }
    }

    public static void RemoveShopItem(string itemName)
    {
        if (shopUnlocks.Remove(itemName))
        {
            Console.WriteLine($"Removed: {itemName}");
        }

        else
        {
            Console.WriteLine($"Item {itemName} not found.");
        }
    }

    public static bool ShopItemExists(string itemName)
    {
        return shopUnlocks.ContainsKey(itemName);
    }

    public static bool? GetShopItemStatus(string itemName)
    {
        if (shopUnlocks.TryGetValue(itemName, out bool status))
        {
            return status;
        }

        Console.WriteLine($"Item {itemName} not found.");

        return null;
    }

    public static void UpdateShopItemStatus(string itemName, bool newStatus)
    {
        if (shopUnlocks.ContainsKey(itemName))
        {
            shopUnlocks[itemName] = newStatus;
            Console.WriteLine($"Updated: {itemName} to status {newStatus}");
        }

        else
        {
            Console.WriteLine($"Item {itemName} not found.");
        }
    }

    public static void PrintAllShopItems()
    {
        Console.WriteLine("All Items:");

        foreach (var item in shopUnlocks)
        {
            Console.WriteLine($"- {item.Key}: {item.Value}");
        }
    }
}
