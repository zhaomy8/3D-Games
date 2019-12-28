using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuforia;

public class ButtonEvent : MonoBehaviour, IVirtualButtonEventHandler
{
    public VirtualButtonBehaviour[] vbs;
    public GameObject sphere;
    public GameObject button;
    public Color[] colors;
    public int index;

    void Start()
    {
        vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i++)
        {
            vbs[i].RegisterEventHandler(this);
        }
        index = 0;
        colors = new Color[5];
        colors[0] = Color.white;
        colors[1] = Color.red;
        colors[2] = Color.green;
        colors[3] = Color.blue;
        colors[4] = Color.yellow;

        sphere = GameObject.Find("ImageTarget/Sphere");
        button = GameObject.Find("ImageTarget/VirtualButton/Plane");
    }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (index == 5)
        {
            index = 0;
            //下移
            sphere.transform.Translate(Vector3.down * Time.deltaTime * 1.5F);
        }
        else
        {
            sphere.transform.Translate(Vector3.up * Time.deltaTime * 0.3F);
        }
        //点击按钮，变换Sphere的颜色，并将其向上移动
        sphere.GetComponent<Renderer>().material.color = colors[index];
        index++;
        Debug.Log("按钮按下");
    }
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("按钮释放");
    }
}
