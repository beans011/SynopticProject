using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTime : MonoBehaviour
{
    private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = gameObject.GetComponent<TextMeshProUGUI>();
        EventManager.UpdateTimeDisplay += UpdateText;
    }

    public void UpdateText()
    {
        timeText.SetText(GameManager.instance.GetTimeString());
    }
}
