using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tank
{
    public delegate void DestroyPlayer(); // game over
    public static event DestroyPlayer destroyEvent;
    void Start()
    {
        setHP(1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (getHP() <= 0)
        {
            this.gameObject.SetActive(false);
            destroyEvent();
        }
    }

    //w，s控制前进后退
    public void moveForward()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 10;
    }
    public void moveBackWard()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * -10;
    }

    //a，d控制原地左右旋转的方向
    public void turn(float offsetX)
    {
        //在x轴上添加增量，改变玩家坦克的欧拉角，从而根据主轴旋转
        float x = gameObject.transform.localEulerAngles.x;
        float y = gameObject.transform.localEulerAngles.y + offsetX * 5;
        gameObject.transform.localEulerAngles = new Vector3(x, y, 0);
    }
}