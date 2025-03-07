using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOpenSignMsg : MonoBehaviour
{
    private bool isActive = false;

    void Start()
    {
        EventManager.SetActiveOpenSignUI += SetTextActive;
        gameObject.SetActive(isActive);
    }

    public void SetTextActive()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
    }
}
