using UnityEngine;
using System.Collections;
using System;

// Helper script will prevent raycasts from hitting, since IsRaycastTarget isn't an option in Unity 5.0.1
public class PreventUiRaycasts : MonoBehaviour, ICanvasRaycastFilter
{
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return false;
    }
}