using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingShelfBoxState : IInteractionState
{
    private int boxOpen = 0;
    private const string binTag = "bin";

    public void HandleInput(PlayerInteraction playerInteraction)
    {
        var shelfBox = playerInteraction.holdingObject.GetComponent<CardboardBoxShelf>();

        if (boxOpen == 0) //box closed
        {
            if (shelfBox.IsPreviewShelfMade())
            {
                shelfBox.DestroyPreviewShelf();
            }

            if (Input.GetKeyDown(playerInteraction.playerObjectInteractKey))
            {
                boxOpen = 1;
                playerInteraction.holdingObject.SetActive(false);
                EventManager.OnHoldingShelfBoxOpenStateUI();
            }           

            if (Input.GetKeyDown(playerInteraction.playerObjectDropKey))
            {
                playerInteraction.DropObject();
            }

            if (Input.GetKeyDown(playerInteraction.playerObjectThrowKey))
            {
                playerInteraction.ThrowObject();
            }

            //binning boxes bit
            if (Input.GetKeyDown(KeyCode.Mouse1) && playerInteraction.GetNearBoxBin() == true)
            {
                playerInteraction.DestroyCurrentHeldBox();
                playerInteraction.SetState(new NothingHeldState());
            }
        }

        else //box open
        {
            if (shelfBox.IsPreviewShelfMade() == false)
            {
                shelfBox.SpawnPreviewShelf(shelfBox.GetRotation());
            }

            shelfBox.HandlePreviewPosition(playerInteraction.cameraTransform, playerInteraction.interactRange, playerInteraction.shelfHeldPos.position);

            if (Input.GetKeyDown(KeyCode.Mouse0) && shelfBox.GetCanPlaceShelf() == true)
            {
                shelfBox.PlaceShelf();
                boxOpen = 0;
                playerInteraction.SetState(new NothingHeldState());
            }

            if (Input.GetKeyDown(playerInteraction.playerObjectInteractKey))
            {
                shelfBox.RotateShelf(-45f);
            }

            if (Input.GetKeyDown(playerInteraction.playerObjectInteractSecondaryKey))
            {
                shelfBox.RotateShelf(45f);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                boxOpen = 0;
                playerInteraction.holdingObject.SetActive(true);
                EventManager.OnHoldingShelfBoxClosedStateUI();
            }
        }
    }
}
