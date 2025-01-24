using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomText : CustomUIComponent
{
    public CustomTextSO textData;
    public Style style;

    private TextMeshProUGUI textMeshProUGUI;

    public override void Setup()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Configure()
    {
        textMeshProUGUI.color = textData.theme.GetTextColour(style);
        textMeshProUGUI.font = textData.font;
        textMeshProUGUI.fontSize = textData.size;
    }
}
