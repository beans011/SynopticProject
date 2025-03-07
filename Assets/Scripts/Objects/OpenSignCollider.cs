using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSignCollider : MonoBehaviour
{
    private BoxCollider boxCollider;
    private const string playerTag = "Player";
    
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == playerTag)
        {
            EventManager.OnSetActiveOpenSignUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == playerTag)
        {
            EventManager.OnSetActiveOpenSignUI();
        }
    }
}
