using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "CustomUI/TextSO")]
public class CustomTextSO : ScriptableObject
{
    public CustomThemeSO theme;

    public TMP_FontAsset font;
    public float size;
}
