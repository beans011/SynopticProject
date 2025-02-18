using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindDisplay_Control : MonoBehaviour
{
    [SerializeField] private GameObject nothingHeldKB;
    [SerializeField] private GameObject holdingItemBoxKB;
    [SerializeField] private GameObject holdingShelfBoxClosedKB;
    [SerializeField] private GameObject holdingShelfBoxOpenKB;

    private void Start()
    {
        EventManager.NothingHeldStateUI += NothingHeldState;
        EventManager.HoldingItemBoxStateUI += HoldingItemBoxState;
        EventManager.HoldingShelfBoxClosedStateUI += HoldingShelfBoxClosedState;
        EventManager.HoldingShelfBoxOpenStateUI += HoldingShelfBoxOpenState;
    }

    public void NothingHeldState()
    {
        holdingItemBoxKB.SetActive(false);
        holdingShelfBoxClosedKB.SetActive(false);
        holdingShelfBoxOpenKB.SetActive(false);
        nothingHeldKB.SetActive(true);
    }

    public void HoldingItemBoxState()
    {        
        holdingShelfBoxClosedKB.SetActive(false);
        holdingShelfBoxOpenKB.SetActive(false);
        nothingHeldKB.SetActive(false);
        holdingItemBoxKB.SetActive(true);
    }

    public void HoldingShelfBoxClosedState()
    {        
        holdingShelfBoxOpenKB.SetActive(false);
        nothingHeldKB.SetActive(false);
        holdingItemBoxKB.SetActive(false);
        holdingShelfBoxClosedKB.SetActive(true);
    }

    public void HoldingShelfBoxOpenState()
    {        
        nothingHeldKB.SetActive(false);
        holdingItemBoxKB.SetActive(false);
        holdingShelfBoxClosedKB.SetActive(false);
        holdingShelfBoxOpenKB.SetActive(true);
    }

    public void DisableKeybindUI()
    {
        nothingHeldKB.SetActive(false);
        holdingItemBoxKB.SetActive(false);
        holdingShelfBoxClosedKB.SetActive(false);
        holdingShelfBoxOpenKB.SetActive(false);
    }
}
