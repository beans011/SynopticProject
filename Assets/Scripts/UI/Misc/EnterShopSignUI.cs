using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnterShopSignUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField shopNameInput;  

    public void SetShopName()
    {
        string newName = shopNameInput.text;

        ShopStats.SetShopName(newName);
        gameObject.SetActive(false);
        EventManager.OnCloseTabMenu();
        EventManager.OnSomeRandomEvent();
    }
}
