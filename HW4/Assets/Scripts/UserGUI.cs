using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class UserGUI : MonoBehaviour
{
    private UserAction action;
    public int status = 0;
    GUIStyle style;
    GUIStyle buttonStyle;

    void Start()
    {
        action = Director.getInstance().currentSceneController as UserAction;

        style = new GUIStyle();
        style.fontSize = 30;
        style.alignment = TextAnchor.MiddleCenter;

        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 25;
    }
    void OnGUI()
    {
        if (status == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 60, 100, 50), "Failture!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 40), "Restart", buttonStyle))
            {
                status = 0;
                action.restart();
            }
        }
        else if (status == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 60, 100, 50), "Success!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 40), "Restart", buttonStyle))
            {
                status = 0;
                action.restart();
            }
        }
    }
}