using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBoxBinMsg : MonoBehaviour
{
    private bool isActive = false;

    void Start()
    {
        EventManager.SetActiveBinBoxUI += SetTextActive;
        gameObject.SetActive(isActive);
    }

    public void SetTextActive()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
    }
}
