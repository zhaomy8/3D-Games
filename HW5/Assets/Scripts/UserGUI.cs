using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;

public class UserGUI : MonoBehaviour
{
    private UserAction action;
    public int round = 0;
    public int score = 0;
    public int targetThisRound = 0;
    //public Font blue_font;

    // Use this for initialization
    void Start()
    {
        action = Director.GetInstance().currentSceneController as UserAction;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1");
            Vector3 pos = Input.mousePosition;
            action.hit(pos);
        }
    }

    // Update is called once per frame
    void OnGUI()
    {
        //GUI.skin.label.font = blue_font;
        GUI.Label(new Rect(Screen.width / 2 - 50, 20, 180, 50), "     Hit UFO     ");
        GUI.Label(new Rect(Screen.width / 2 - 120, 50, 100, 50), "Round: " + round.ToString());
        GUI.Label(new Rect(Screen.width / 2 - 30, 50, 180, 50), "score: " + score.ToString());
        GUI.Label(new Rect(Screen.width / 2 + 60, 50, 180, 50), "goal: " + targetThisRound.ToString());
        
        if (round == -1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 30, 80, 100, 50), "Game over!");
        }
        else if (round != -1)
        {
            //GUI.Label(new Rect(Screen.width / 2 - 120, 50, 100, 50), "Round: " + round.ToString());
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 4 * 3 , 70, 30), "Restart"))
        {
            action.restart();
        }
    }

}
