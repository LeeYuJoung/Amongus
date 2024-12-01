using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EControlType
{
    Mouse,
    KeyboaedMouse
}

public class PlayerSettings
{
    public static EControlType controlType;
    public static string nickname;
}