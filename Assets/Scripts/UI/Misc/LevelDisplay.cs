using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    private TextMeshProUGUI levelText;

    void Start()
    {
        levelText = GetComponent<TextMeshProUGUI>();
        EventManager.ShopLevelUp += UpdateLevelDisplay;
        UpdateLevelDisplay();
    }

    private void UpdateLevelDisplay()
    {
        levelText.SetText(ShopStats.GetShopLevel().ToString());
    }
}
