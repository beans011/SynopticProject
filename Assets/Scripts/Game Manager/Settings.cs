using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    //this file is for options like volume and other kack

    private static float mouseSensitivity = 5.0f, volumePercent = 1.0f;

    public static float GetMouseSensitivity() { return mouseSensitivity; }
    public static void SetMouseSensitivity(float value) {  mouseSensitivity = value; }

    public static float GetVolumePercent() {  return volumePercent; }
    public static void SetVolumePercent(float newVolumePercent) { volumePercent = newVolumePercent; }
}
