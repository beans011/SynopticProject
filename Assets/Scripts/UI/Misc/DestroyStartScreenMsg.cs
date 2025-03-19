using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStartScreenMsg : MonoBehaviour
{
    void Start()
    {
        EventManager.SomeRandomEvent += Eliminado;
    }

    private void Eliminado()
    {
        Destroy(gameObject);
    }
}
