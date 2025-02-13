using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Products", menuName = "Shop/New Shelf")]
public class ShopShelf : ScriptableObject
{
    public int shelfID;
    public string shelfName;
    public string shelfDescription;
    public int levelRequirement;
    public GameObject shelfObj;
    public Material shelfImage;
    public string axisRotation;
}
