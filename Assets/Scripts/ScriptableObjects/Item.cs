using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Products", menuName = "Shop/New Product")]
public class Item : ScriptableObject
{
    [Header("Product ID")]
    public int productID;
    public bool itemUnlocked = false;
    public bool isItemIllegal;
    public bool isIDRequired;

    [Header("Product Text")]
    public string productName;
    public string productType;
    public string productDescription;

    [Header("Product Numbers")]
    public float marketPrice;
    public float playerSetPrice;
    public float liscenseCost;

    [Header("Product Model")] //make a prefab of the model and then set it as this
    public GameObject productModel;
}
