using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingHeldState : IInteractionState
{
    public void HandleInput(PlayerInteraction playerInteraction)
    {
        if (Input.GetKeyDown(playerInteraction.playerObjectInteractKey))
        {
            RaycastHit hit;

            if (Physics.Raycast(playerInteraction.cameraTransform.position, playerInteraction.cameraTransform.forward, out hit, playerInteraction.interactRange))
            {
                if (playerInteraction.holdingObject == null && hit.transform.CompareTag("canPickUp"))
                {
                    playerInteraction.PickUpObject(hit.transform.gameObject);

                    if (playerInteraction.holdingObject.TryGetComponent(out CardboardBox _))
                    {
                        playerInteraction.SetState(new HoldingItemBoxState());
                    }
                    else if (playerInteraction.holdingObject.TryGetComponent(out CardboardBoxShelf _))
                    {
                        playerInteraction.SetState(new HoldingShelfBoxState());
                    }
                }
            }
        }
    }
}
