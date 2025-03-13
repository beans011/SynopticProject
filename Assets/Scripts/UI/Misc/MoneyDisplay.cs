using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    private TextMeshProUGUI moneyText;

    void Start()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
        EventManager.UpdateMoneyDisplays += UpdateMoney;
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        float money = ShopStats.GetMoney();       
        moneyText.SetText("£: " + Math.Round(money, 2));
    }
}
