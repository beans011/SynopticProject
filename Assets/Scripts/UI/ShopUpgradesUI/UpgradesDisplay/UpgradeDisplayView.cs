using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeDisplayView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI spaceCostText;
    [SerializeField] private TextMeshProUGUI levelRequirementText;

    [SerializeField] private GameObject itemUnlockThing;

    public void UpdateBuyUI(float cost)
    {
        spaceCostText.text = "Cost: £ " + cost.ToString("F2");
    }

    public void UpdateUnlockUI(int level)
    {
        levelRequirementText.text = "Level Required: " + level.ToString();
    }

    public void ToggleUnlockThing(bool isUnlocked)
    {
        itemUnlockThing.SetActive(!isUnlocked);
    }
}
