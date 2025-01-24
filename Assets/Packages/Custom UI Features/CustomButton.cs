using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class CustomButton : CustomUIComponent
{
    public CustomThemeSO theme;
    public Style style;
    public UnityEvent onClick;

    public Button button;
    public TextMeshProUGUI buttonText;

    public override void Setup()
    {
        button = GetComponentInChildren<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Configure()
    {
        ColorBlock cb = button.colors;
        cb.normalColor = theme.GetBackgroundColour(style);
        button.colors = cb;

        buttonText.color = theme.GetTextColour(style);
    }

    public void OnClick()
    {
        onClick.Invoke();
    }
}
