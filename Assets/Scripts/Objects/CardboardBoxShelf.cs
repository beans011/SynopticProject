using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardBoxShelf : MonoBehaviour
{
    [SerializeField] private ShopShelf shelfInBox;
    [SerializeField] private GameObject shelfBoxImage01;
    [SerializeField] private GameObject shelfBoxImage02;
    [SerializeField] private Material defaultMaterial;


    public void SetupBox(ShopShelf shelfAdded)
    {
        shelfInBox = shelfAdded;
        SetupBox();
    }

    private void SetupBox()
    {
        if (shelfInBox == null)
        {
            shelfBoxImage01.GetComponent<MeshRenderer>().material = defaultMaterial;
            shelfBoxImage02.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        else
        {
            shelfBoxImage01.GetComponent<MeshRenderer>().material = shelfInBox.shelfImage;
            shelfBoxImage02.GetComponent<MeshRenderer>().material = shelfInBox.shelfImage;
        }       
    }
}
