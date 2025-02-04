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
    public float deliveryCost;
    public float liscenseCost;
    public int maxStackSize;
    public int levelRequirement;

    [Header("Product Model")] //make a prefab of the model and then set it as this
    public GameObject productModel;
    public Material productMaterial; //take a screen shot of the model in game so it can be placed on cardboard boxes
    public Sprite productImage; //image for ui use - take the photo on whatever the background is gonna be
}
