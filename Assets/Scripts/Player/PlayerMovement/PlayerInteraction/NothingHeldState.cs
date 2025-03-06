using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NothingHeldState : IInteractionState
{
    int layer = LayerMask.GetMask("shelfSign");
    private const string openSignTag = "openSign";

    public void HandleInput(PlayerInteraction playerInteraction)
    {
        if (Input.GetKeyDown(playerInteraction.playerObjectInteractKey))
        {
            RaycastHit hit;

            //interact with boxes
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

                //interact with shop sign
                if (playerInteraction.holdingObject == null && hit.transform.CompareTag(openSignTag))
                {
                    var signScript = hit.transform.GetComponent<OpenCloseSign>();
                    signScript.SignState();
                }
            }
        }

        if (Input.GetKeyDown(playerInteraction.playerObjectInteractSecondaryKey))
        {
            RaycastHit hit;

            if (Physics.Raycast(playerInteraction.cameraTransform.transform.position, playerInteraction.cameraTransform.transform.forward, out hit, playerInteraction.interactRange, layer))
            {
                Debug.Log(hit.transform.tag);

                if (hit.transform.gameObject.tag == "shelfSign")
                {
                    hit.transform.gameObject.GetComponent<Shelf>().GiveInfoToUIController();
                }
            }
        }
    }
}
