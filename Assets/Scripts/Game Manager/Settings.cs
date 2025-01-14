using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    //this file is for options like volume and other kack

    private static float mouseSensitivity = 5.0f;

    public static float GetMouseSensitivity() { return mouseSensitivity; }
    public static void SetMouseSensitivity(float value) {  mouseSensitivity = value; }
}
