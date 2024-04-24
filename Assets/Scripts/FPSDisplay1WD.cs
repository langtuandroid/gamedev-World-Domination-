using UnityEngine;

public class FPSDisplay1WD : MonoBehaviour
{
    private float deltaTimeE = 0.0f;

    private void Update()
    {
        deltaTimeE += (Time.unscaledDeltaTime - deltaTimeE) * 0.1f;
    }

    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTimeE * 1000.0f;
        float fps = 1.0f / deltaTimeE;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}