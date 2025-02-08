using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDisplayModel : MonoBehaviour
{
    [SerializeField] private GameObject spaceObject;
    [SerializeField] private float spaceCost;
    [SerializeField] private int levelRequirement;
    private bool isBought = false;

    public GameObject GetSpaceObject() { return spaceObject; }
    public float GetSpaceCost() { return spaceCost; }
    public int GetLevelRequirement() {  return levelRequirement; }
    public bool GetIsBought() { return isBought; }
    public void SetIsBought(bool well) {  isBought = well; }

    public void ObjectObliteration()
    {
        isBought = true;
        Destroy(spaceObject);
    }
}
