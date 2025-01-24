using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/ThemeSO")]
public class CustomThemeSO : ScriptableObject
{
    [Header("Primary")]
    public Color primary_bg;
    public Color primary_text;

    [Header("Secondary")]
    public Color secondary_bg;
    public Color secondary_text;

    [Header("Tertiary")]
    public Color tertiary_bg;
    public Color tertiary_text;

    [Header("Other")]
    public Color disable;

    public Color GetBackgroundColour(Style style)
    {
        if (style == Style.Primary)
        {
            return primary_bg;
        }
        else if (style == Style.Secondary) 
        { 
            return secondary_bg;
        }
        else if (style == Style.Tertiary) 
        {
            return tertiary_bg;
        }

        return disable;
    }

    public Color GetTextColour(Style style) 
    {
        if (style == Style.Primary)
        {
            return primary_text;
        }
        else if (style == Style.Secondary)
        {
            return secondary_text;
        }
        else if (style == Style.Tertiary)
        {
            return tertiary_text;
        }

        return disable;
    }
}
