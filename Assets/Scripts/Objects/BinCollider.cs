using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider binCollider;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == playerTag)
        {
            EventManager.OnSetActiveBinBoxUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == playerTag)
        {
            EventManager.OnSetActiveBinBoxUI();
        }
    }
}
