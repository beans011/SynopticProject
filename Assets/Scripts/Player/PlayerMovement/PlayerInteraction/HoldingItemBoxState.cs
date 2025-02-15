using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingItemBoxState : IInteractionState
{
    int layer = LayerMask.GetMask("shelfSign");

    public void HandleInput(PlayerInteraction playerInteraction)
    {
        if (Input.GetKeyDown(playerInteraction.playerObjectInteractKey))
        {
            RaycastHit hit;     

            if (Physics.Raycast(playerInteraction.cameraTransform.transform.position, playerInteraction.cameraTransform.transform.forward, out hit, playerInteraction.interactRange, layer))
            {
                Debug.Log(hit.transform.tag);

                if (hit.transform.gameObject.tag == "shelfSign")
                {
                    //check if item can be added to the shelf
                    if (hit.transform.gameObject.GetComponent<Shelf>().GetItem() == null || playerInteraction.holdingObject.GetComponent<CardboardBox>().GetItem() == hit.transform.gameObject.GetComponent<Shelf>().GetItem())
                    {
                        hit.transform.gameObject.GetComponent<Shelf>().AddItemsToShelf(1, playerInteraction.holdingObject.GetComponent<CardboardBox>().GetItem()); //validation in here to change to shelf status

                        if (hit.transform.gameObject.GetComponent<Shelf>().GetShelfFullStatus() == false)
                        {
                            playerInteraction.holdingObject.GetComponent<CardboardBox>().SubtractFromBox();
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(playerInteraction.playerObjectInteractSecondaryKey)) //if statement to remove items from shelf and add it to the box if they can
        {
            RaycastHit hit;

            if (Physics.Raycast(playerInteraction.cameraTransform.transform.position, playerInteraction.cameraTransform.transform.forward, out hit, playerInteraction.interactRange, layer))
            {
                Debug.Log(hit.transform.tag);

                if (hit.transform.gameObject.tag == "shelfSign")
                {
                    //check if item can be added to box
                    if (playerInteraction.holdingObject.GetComponent<CardboardBox>().GetItem() == null || playerInteraction.holdingObject.GetComponent<CardboardBox>().GetItem() == hit.transform.gameObject.GetComponent<Shelf>().GetItem())
                    {
                        playerInteraction.holdingObject.GetComponent<CardboardBox>().AddToBox(hit.transform.gameObject.GetComponent<Shelf>().GetItem());
                        hit.transform.gameObject.GetComponent<Shelf>().RemoveItemsFromShelf(1);
                    }
                }
            }
        }

        if (Input.GetKeyDown(playerInteraction.playerObjectDropKey) && playerInteraction.holdingObject != null)
        {
            playerInteraction.DropObject();           
        }

        if (Input.GetKeyDown(playerInteraction.playerObjectThrowKey) && playerInteraction.holdingObject != null)
        {
            playerInteraction.ThrowObject();
        }
    }
}
