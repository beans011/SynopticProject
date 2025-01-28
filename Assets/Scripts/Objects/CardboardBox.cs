using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardboardBox : MonoBehaviour
{
    [SerializeField] private Item item;
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
        item = addedItem;
        currentStackSize = item.maxStackSize;
        maxStackSize = item.maxStackSize;
        UpdateTextAndImage();
    }

    //ADD CODE IN HERE TO REPLACE ITEM IN BOX WHEN SHELVES ARE DONE

    public void AttachEvents()
    {
        Debug.Log("events attached");
        EventManager.CardboardBoxInteract += SubtractFromBox;
        EventManager.CardboardBoxInteractSecondary += AddToBox;
    }

    public void DetachEvents()
    {
        Debug.Log("events detached");
        EventManager.CardboardBoxInteract -= SubtractFromBox;
        EventManager.CardboardBoxInteractSecondary -= AddToBox;
    }

    public void AddToBox()
    {
        if (item != null) 
        {
            currentStackSize = currentStackSize + 1;

            if (currentStackSize > maxStackSize)
            {
                Debug.LogWarning("CANT GO ABOVE THE MAX STACK");
                currentStackSize = maxStackSize;
            }
        }
        
        else if (item == null) 
        {
            //code here to add the new item type to this box when shelves are done

            currentStackSize = currentStackSize + 1;
        }

        UpdateTextAndImage();
    }

    public void SubtractFromBox()
    {
        currentStackSize = currentStackSize - 1;

        if (currentStackSize < 0) 
        {           
            item = null; //box has no item assoicaated with it now so new can be added
            currentStackSize = 0;
        }

        UpdateTextAndImage();
    }

    private void UpdateTextAndImage()
    {
        text01.GetComponent<TextMeshPro>().SetText(currentStackSize.ToString());
        text02.GetComponent<TextMeshPro>().SetText(currentStackSize.ToString());

        if (item != null) 
        {
            productImage01.GetComponent<MeshRenderer>().material = item.productImage;
            productImage02.GetComponent<MeshRenderer>().material = item.productImage;
        }
        else
        {
            productImage01.GetComponent<MeshRenderer>().material = defaultMaterial;
            productImage02.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }
}
