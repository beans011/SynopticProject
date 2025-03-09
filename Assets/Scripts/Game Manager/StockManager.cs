using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockProduct
{
    public StockProduct(int itemID, Item item, int stockAmount)
    {
        ItemID = itemID;
        StructItem = item;
        StockAmount = stockAmount;
    }

    public int ItemID { get; }
    public Item StructItem { get; }
    public int StockAmount { get; set; }
}

public class StockManager : MonoBehaviour
{
    public static StockManager instance;

    public Dictionary<int, StockProduct> stockInfo = new Dictionary<int, StockProduct>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #region stuff to help the dictionary
    public void AddToStockInfo(Item item)
    {
        if (stockInfo.ContainsKey(item.productID)) 
        {
            stockInfo[item.productID].StockAmount = stockInfo[item.productID].StockAmount + 1;
            Debug.Log("added 1 to " + stockInfo[item.productID] + " " + stockInfo[item.productID].StructItem.productName);
        }
        else
        {
            StockProduct tempStockProduct = new StockProduct(item.productID, item, 1);

            stockInfo.Add(item.productID, tempStockProduct);
            Debug.Log("added: " +  item.productID + " and " + tempStockProduct.StructItem.productName);
        }       
    }

    public void RemoveOneFromStock(Item item)
    {
        if (!stockInfo.ContainsKey(item.productID))
        {
            Debug.LogWarning("Item to be removed from StockInfo not found");
            return;
        }
        else
        {
            stockInfo[item.productID].StockAmount = stockInfo[item.productID].StockAmount - 1;
            Debug.Log("removed 1 from " + stockInfo[item.productID] + " " + stockInfo[item.productID].StructItem.productName);

            if (stockInfo[item.productID].StockAmount <= 0)
            {
                DeleteItemFromDictionary(item);
            }
        }
    }

    public void DeleteItemFromDictionary(Item item)
    {
        Debug.Log("No items of name: " + item.productName + " in shop");
        stockInfo.Remove(item.productID);
    }

    public float GetPlayerSetPrice(Item item)
    {
        return stockInfo[item.productID].StructItem.playerSetPrice;
    }

    public int GetStockAmount(Item item)
    {
        return stockInfo[item.productID].StockAmount;
    }
    #endregion
}
