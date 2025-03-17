using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopNameSign : MonoBehaviour
{
    [SerializeField] private TextMeshPro shopSignText;

    private void Start()
    {
        shopSignText = GetComponent<TextMeshPro>();
        EventManager.UpdateShopNameDisplay += UpdateShopSignText;
    }

    private void UpdateShopSignText()
    {
        shopSignText.SetText(ShopStats.GetShopName());
    }
}
