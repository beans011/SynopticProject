using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI startMoneyText;
    [SerializeField] private TextMeshProUGUI endMoneyText;
    [SerializeField] private TextMeshProUGUI profitText;

    private void Start()
    {
        EventManager.UpdateEndScreen += UpdateUI;
        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        gameObject.SetActive(true);

        dayText.text = "Day: " + GameManager.instance.GetDay();
        startMoneyText.text = "Starting Money: £" + GameManager.instance.GetStartMoney();
        endMoneyText.text = "Ending Money: £" + GameManager.instance.GetEndMoney();
        profitText.text = "Profit: £" + GameManager.instance.GetProfit();
    }

    public void CloseUI()
    {
        EventManager.OnCloseCheatConsole();
        gameObject.SetActive(false);        
    }
}
