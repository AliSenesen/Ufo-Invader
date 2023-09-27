using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;
    GUIStyle style;
    Rect rect;

    void Start()
    {
        Application.targetFrameRate = -1;
        int fontSize = Mathf.RoundToInt(Screen.width / 40f);
        style = new GUIStyle();
        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = fontSize;
        style.normal.textColor = Color.white;
        rect = new Rect(0, 0, Screen.width, Screen.height * 2 / 100);
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
