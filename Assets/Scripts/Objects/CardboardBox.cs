using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardBox : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int currentStackSize;
    [SerializeField] private int maxStackSize;

    public void SetUpBox(Item addedItem)
    {
        item = addedItem;
        currentStackSize = item.maxStackSize;
        maxStackSize = item.maxStackSize;
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
            UpdateTextAndImage();
            item = null; //box has no item assoicaated with it now so new can be added
            currentStackSize = 0;
        }
    }

    private void UpdateTextAndImage()
    {
        //set text to 0
        //set image to null
        //set the Item item to null
    }
}
