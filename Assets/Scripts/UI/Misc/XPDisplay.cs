using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XPDisplay : MonoBehaviour
{
    private TextMeshProUGUI xpText;

    void Start()
    {
        xpText = GetComponent<TextMeshProUGUI>();
        EventManager.UpdateXPDisplays += UpdateXPDisplay;
        UpdateXPDisplay();
    }

    private void UpdateXPDisplay()
    {
        xpText.SetText(ShopStats.GetShopXP().ToString() + "/" + ShopStats.GetShopXPToLevelUp().ToString());
    }
}
