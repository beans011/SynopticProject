using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardboardBox : MonoBehaviour
{
    [SerializeField] private Item itemInBox;
    [SerializeField] private int currentStackSize;
    [SerializeField] private int maxStackSize;

    [Header("Display Related Kack")]
    [SerializeField] private GameObject text01;
    [SerializeField] private GameObject text02;
    [SerializeField] private GameObject productImage01;
    [SerializeField] private GameObject productImage02;
    [SerializeField] private Material defaultMaterial;

    public void SetUpBox(Item addedItem)
    {
        itemInBox = addedItem;
        currentStackSize = itemInBox.maxStackSize;
        maxStackSize = itemInBox.maxStackSize;
        UpdateTextAndImage();
    }

    //ADD CODE IN HERE TO REPLACE ITEM IN BOX WHEN SHELVES ARE DONE

    public void AddToBox(Item item)
    {
        if (itemInBox != null) 
        {
            currentStackSize = currentStackSize + 1;

            if (currentStackSize > maxStackSize)
            {
                //Debug.LogWarning("CANT GO ABOVE THE MAX STACK");
                UIController.instance.DisplayErrorMessage("Box is full");
                currentStackSize = maxStackSize;
            }
        }
        
        else if (itemInBox == null) 
        {
            //code here to add the new item type to this box when shelves are done
            itemInBox = item;

            currentStackSize = currentStackSize + 1;
            maxStackSize = itemInBox.maxStackSize;
        }

        UpdateTextAndImage();
    }

    public void SubtractFromBox()
    {
        currentStackSize = currentStackSize - 1;

        if (currentStackSize < 0) 
        {           
            itemInBox = null; //box has no item assoicaated with it now so new can be added
            currentStackSize = 0;
        }

        UpdateTextAndImage();
    }

    private void UpdateTextAndImage()
    {
        text01.GetComponent<TextMeshPro>().SetText(currentStackSize.ToString());
        text02.GetComponent<TextMeshPro>().SetText(currentStackSize.ToString());

        if (itemInBox != null) 
        {
            productImage01.GetComponent<MeshRenderer>().material = itemInBox.productMaterial;
            productImage02.GetComponent<MeshRenderer>().material = itemInBox.productMaterial;
        }
        else
        {
            productImage01.GetComponent<MeshRenderer>().material = defaultMaterial;
            productImage02.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }

    public Item GetItem()
    {
        return itemInBox;
    }
}
