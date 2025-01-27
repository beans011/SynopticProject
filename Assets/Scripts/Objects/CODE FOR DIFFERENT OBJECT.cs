using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CODEFORADIFFERENTOBJECT : MonoBehaviour
{
    [Header("Scriptable Object Related Stuff")]
    [SerializeField] private Item itemInBox; //whats in the box
    [SerializeField] private int currentAmountOfItems;
    [SerializeField] private int maxAmountOfItems;

    [Header("Stuff to get objects in the box")]
    private BoxCollider boxCollider;
    private Vector3 boxSize;
    [SerializeField] private Vector3 boxPadding = new Vector3(0.2f, 0.2f, 0.2f);
    private List<GameObject> spawnedItems = new List<GameObject>(); //keep track of kack put into box for easiness

    private bool itemSetupDone = false;

    private void Start()
    {
        boxSize = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        //boxSize = gameObject.GetComponent<BoxCollider>().size;
        AddItemToBox(maxAmountOfItems);
    }

    private void Update()
    {
        if (itemSetupDone == false)
        {
            //AddItemToBox(maxAmountOfItems);
            itemSetupDone = true;
        }       
    }

    public void SetUpBox(Item item)
    {
        itemInBox = item;
        maxAmountOfItems = item.maxStackSize;     
        Debug.Log(item.productModel + " is in the box!!!");
    }

    #region REUSE THIS CODE FOR THE SHELVES CAUSE IT WORKS WELL FOR DYNAMICALLY HAVING A GOOD NUMBER OF SHIT ON SHELVES BASED ON SIZE
    public void AddItemToBox(int count)
    {
        for (int i = 0; i < count; i++) 
        { 
            if(spawnedItems.Count >= maxAmountOfItems)
            {
                Debug.Log("NO MORE ROOM TO ADD");
                break;
            }

            Vector3 position = CalcNextPos(spawnedItems.Count);

            if (position == Vector3.zero)
            {
                return;
            }

            //maybe change this with an object pooling system if lag rocks the boat
            GameObject copy = Instantiate(itemInBox.productModel, position, itemInBox.productModel.transform.rotation, transform);
            //copy.transform.localScale = itemInBox.productModel.transform.localScale;
            spawnedItems.Add(copy);

            currentAmountOfItems = spawnedItems.Count;
        }
    }

    public void RemoveItemFromBox(int count) 
    { 
        for (int i = 0;i < count;i++)
        {
            if (spawnedItems.Count == 0)
            {
                Debug.LogWarning("NOTHINGS IN THE BOX CANT REMOVE WHATS NOT THERE");
                break;
            }

            GameObject obj = spawnedItems[spawnedItems.Count - 1];
            spawnedItems.RemoveAt(spawnedItems.Count - 1);
            Destroy(obj);

            currentAmountOfItems = spawnedItems.Count;
        }
    }

    private Vector3 CalcNextPos(int index)
    {
        Vector3 itemSize = itemInBox.productModel.GetComponent<MeshRenderer>().localBounds.size;
        Debug.Log(itemSize); //test code
        Vector3 cellSize = itemSize + boxPadding;

        int cols = Mathf.FloorToInt(boxSize.x / cellSize.x);
        int rows = Mathf.FloorToInt(boxSize.y / cellSize.y);
        int layers = Mathf.FloorToInt(boxSize.z / cellSize.z);

        if (index >= cols * rows * layers)
        {
            Debug.LogWarning("NO ROOM LEFT");
            return Vector3.zero;
        }

        // Reverse order: Fill from the bottom layer first
        int col = index % cols;                          // Column within a row
        int row = (index / cols) % rows;                 // Row within a layer
        int layer = (index / (cols * rows));             // Layer from bottom to top

        Vector3 startOffset = -boxSize / 2 + cellSize / 2;
        return transform.position + new Vector3(
            startOffset.x + col * cellSize.x,
            startOffset.y + row * cellSize.y,
            startOffset.z + layer * cellSize.z
        );
    }
    #endregion
}
