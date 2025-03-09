using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Shelf : MonoBehaviour
{
    [Header("Scriptable Object Related Stuff")]
    [SerializeField] private Item itemOnShelf; //whats in the box
    public GameObject productImage;
    public GameObject priceText;
    [SerializeField] private Material defaultMaterial;

    [Header("Stuff to get objects on the Shelf")]
    [SerializeField] private Vector3 shelfSize;
    private BoxCollider boxCollider;
    [SerializeField] private Vector3 shelfPadding = new Vector3(0.01f, 0.01f, 0.01f); //idk what to set this yet just test u know
    private List<GameObject> spawnedItems = new List<GameObject>(); //keep track of kack put into box for easiness
    private bool isShelfFull = false;

    private bool npcCanInteract = true;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        EventManager.UpdatePriceDisplays += UpdateInfo;
        UpdateInfo();
    }

    public void AddItemsToShelf(int count, Item item)
    {
        if (isShelfFull) 
        {
            Debug.LogWarning("NO ROOM BOZO");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            if (itemOnShelf == null) //checks if there is an item on the shelf
            {
                itemOnShelf = item;
            }

            if (item != itemOnShelf) //checks if the item being added is the same
            {
                Debug.LogWarning("NOT THE SAME ITEM");
                UIController.instance.DisplayErrorMessage("Not the same item on the shelf");
                break;
            }

            Vector3 position = CalculateNextPosition(spawnedItems.Count);

            if (position == Vector3.zero)
            {
                Debug.LogWarning("NO ROOM BOZO");
                UIController.instance.DisplayErrorMessage("No room on shelf");
                isShelfFull = true;
                break;
            }

            GameObject newItem = Instantiate(itemOnShelf.productModel, position, itemOnShelf.productModel.transform.rotation, transform);
            spawnedItems.Add(newItem);

            Debug.Log(itemOnShelf.productName);
            StockManager.instance.AddToStockInfo(itemOnShelf); //adds thing to the stock info system for the npc clowns so tehy can see if it exists cause ai needs so much data casue tehyare silly
        }

        UpdateInfo();
    }

    public void RemoveItemsFromShelf(int count)
    {
        if (npcCanInteract)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject itemToRemove = spawnedItems[spawnedItems.Count - 1];
                spawnedItems.RemoveAt(spawnedItems.Count - 1);

                StockManager.instance.RemoveOneFromStock(itemOnShelf); //removes one item from the stock info dictionary that the npcs will use to see items exist in world
                                                                       //shelf shit is just visual stuff cause its painful to do it with them directly grabbing on shelf
                Destroy(itemToRemove);
                isShelfFull = false;

                if (spawnedItems.Count == 0) //check if no item left in the shelf
                {
                    Debug.LogWarning("ALL ITEMS GONE");

                    itemOnShelf = null;
                    UpdateInfo();

                    break;
                }
            }

            UpdateInfo();
        }       
    }

    private void UpdateInfo()
    {
        if (itemOnShelf != null)
        {
            productImage.GetComponent<MeshRenderer>().material = itemOnShelf.productMaterial;
            priceText.GetComponent<TextMeshPro>().SetText("£:" + itemOnShelf.playerSetPrice.ToString());
        }
        else
        {
            productImage.GetComponent<MeshRenderer>().material = defaultMaterial;
            priceText.GetComponent<TextMeshPro>().SetText("£: 0");
        }
    }

    public Item GetItem()
    {
        return itemOnShelf;
    }

    public bool GetShelfFullStatus()
    {
        return isShelfFull;
    }

    public bool IsThereItemOnShelf()
    {
        if (spawnedItems.Count > 0) 
        {
            return true;
        }

        return false;
    }

    public void SetNPCCanInteract(bool newAns)
    {
        npcCanInteract = newAns;
    }
    public bool GetNPCCanInteract() { return npcCanInteract; }

    private Vector3 CalculateNextPosition(int index) //IT FINALLY WORKS thank god
    {
        Vector3 itemSize = itemOnShelf.productModel.GetComponent<MeshRenderer>().bounds.size;
        Vector3 cellSize = itemSize + shelfPadding;

        int cols = Mathf.FloorToInt(boxCollider.bounds.size.x / cellSize.x);
        int rows = Mathf.FloorToInt(boxCollider.bounds.size.z / cellSize.z);

        int col = index % cols;
        int row = index / cols;

        if (row >= rows) //NO SPACES LEFt
        {
            return Vector3.zero;
        }

        Vector3 shelfMin = boxCollider.bounds.min;
        Vector3 shelfMax = boxCollider.bounds.max;

        Vector3 localStartOffset = new Vector3(
            shelfMin.x + cellSize.x / 2,
            shelfMin.y + itemSize.y / 2,
            shelfMin.z + cellSize.z / 2
        );

        Vector3 localPosition = new Vector3(
            localStartOffset.x + col * cellSize.x,
            localStartOffset.y,                    
            localStartOffset.z + row * cellSize.z 
        );

        return localPosition;
    }

    public void GiveInfoToUIController()
    {
        GameObject parentShelf = gameObject.transform.root.gameObject;

        if (itemOnShelf != null) 
        {
            UIController.instance.ConfigureShelfControlUI(itemOnShelf, parentShelf);
        }
        else 
        {
            UIController.instance.ConfigureShelfControlUI(parentShelf);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position, shelfSize);
    //}
}
