using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/ViewSO")]
public class CustomViewSO : ScriptableObject
{
    public CustomThemeSO theme;
    public RectOffset padding;
    public float spacing;
}
