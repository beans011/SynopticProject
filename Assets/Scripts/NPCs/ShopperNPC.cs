using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Progress;

public class NpcInventoryItem
{
    public NpcInventoryItem(int itemID, Item item, int amount)
    {
        ItemID = itemID;
        StructItem = item;
        Amount = amount;
    }

    public int ItemID { get; }
    public Item StructItem { get; }
    public int Amount { get; set; }
}

public class ShopperNPC : MonoBehaviour
{
    private NavMeshAgent agent;
    private int counter = 0;
    private Transform targetPoint;

    private int npcState = 0;

    private float budget;
    private float currentBudget;
    public Dictionary<int, NpcInventoryItem> npcInv = new Dictionary<int, NpcInventoryItem>();
    private bool canBuyMore;

    private void Start()
    {
        budget = Random.Range(3.0f, 7.0f) * (ShopStats.GetShopLevel()); //shop level is currently 0. and X * 0 is still fucking 0 so the npcs are breoke beyond belief
        currentBudget = budget;
        Debug.Log(budget);

        gameObject.GetComponentInChildren<Renderer>().material = NPCSpawner.instance.GetMaterial();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (counter == 30)
        {
            switch (npcState)
            {
                case 0:
                    WalkToShop();
                    break;

                case 1:
                    ShoppingYAY();
                    break;

                case 2:
                    GotToTillsToPay();
                    break;

                case 3:
                    LeaveShop();
                    break;
            }
            
            counter = 0;
        }
        else
        {
            counter += 1;
        }
    }

    private void WalkToShop()
    {
        agent.SetDestination(NPCSpawner.instance.GetShopEntrance());

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            npcState = 1;
        }
    }

    private void ShoppingYAY()
    {
        canBuyMore = false;
        int maxAttempts = 10; //while loop might be painful and cause blackwhole so limiter here to help stop that
        int attempts = 0;

        while (canBuyMore && attempts < maxAttempts)
        {
            canBuyMore = false;

            foreach (var stockItem in StockManager.instance.stockInfo.Values)
            {
                if (stockItem.StockAmount > 0 && currentBudget >= stockItem.StructItem.playerSetPrice)
                {
                    CanNPCBuyThatItem(stockItem.StructItem);
                    StockManager.instance.RemoveOneFromStock(stockItem.StructItem);
                    canBuyMore = true;
                    break;
                }
            }
            attempts++;
        }

        npcState = 2;
    }

    private void GotToTillsToPay()
    {
        agent.SetDestination(NPCSpawner.instance.GetTillLocation());

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            ShopStats.AddMoney(Mathf.Round(budget - currentBudget));
            Debug.Log(ShopStats.GetMoney());
            npcState = 3;
        }
    }

    private void LeaveShop()
    {
        agent.SetDestination(NPCSpawner.instance.GetASpawnPoint().position);
        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Destroy(gameObject);
        }
    }

    #region dictionary stuff
    public void AddToNPCInv(Item item)
    {
        if (npcInv.ContainsKey(item.productID))
        {
            npcInv[item.productID].Amount = npcInv[item.productID].Amount + 1;
            Debug.Log("added 1 to " + npcInv[item.productID] + " " + npcInv[item.productID].StructItem.productName);
        }
        else
        {
            NpcInventoryItem tempStockProduct = new NpcInventoryItem(item.productID, item, 1);

            npcInv.Add(item.productID, tempStockProduct);
            Debug.Log("added: " + item.productID + " and " + tempStockProduct.StructItem.productName);
        }
    }

    public void RemoveOneFromNPCInv(Item item)
    {
        if (!npcInv.ContainsKey(item.productID))
        {
            Debug.LogWarning("Item to be removed from npcInv not found");
            return;
        }
        else
        {
            npcInv[item.productID].Amount = npcInv[item.productID].Amount - 1;
            Debug.Log("removed 1 from " + npcInv[item.productID] + " " + npcInv[item.productID].StructItem.productName);

            if (npcInv[item.productID].Amount <= 0)
            {
                DeleteItemFromDictionary(item);
            }
        }
    }

    public void DeleteItemFromDictionary(Item item)
    {
        Debug.Log("No items of name: " + item.productName + " in npcInv");
        npcInv.Remove(item.productID);
    }

    public void CanNPCBuyThatItem(Item item)
    {
        if (currentBudget >= item.playerSetPrice) 
        { 
            currentBudget = currentBudget - item.playerSetPrice;
            AddToNPCInv(item);
        }
        else
        {
            Debug.Log("Not enough funds for: " + item.name);
        }
    }    
    #endregion
}
