using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Products", menuName = "Shop/New Product")]
public class ShopItemCreation : ScriptableObject
{
    [Header("Product Text")]
    public string productName;
    public string productType;
    public string productDescription;

    [Header("Product Numbers")]
    public float retailPrice;
    public float playerSetPrice;

    [Header("Product Model")] //make a prefab of the model and then set it as this
    public GameObject productModel;

    public void SetRetailPrice(float newRetailPrice)
    {
        retailPrice = newRetailPrice;
    }    
}
