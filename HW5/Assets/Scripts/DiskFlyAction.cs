using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;

public class DiskFlyAction : SSAction
{
    private Vector3 start_vector;                              //初速度向量
    private Vector3 gravity_vector = Vector3.zero;             //加速度的向量，初始时为0
    private float time;                                        //已经过去的时间
    private Vector3 current_angle = Vector3.zero;              //当前时间的欧拉角

    private DiskFlyAction() { }
    public static DiskFlyAction GetSSAction(float angle, float power) //Vector3 direction, float angle, float power)
    {
        //初始化物体将要运动的初速度向量
        DiskFlyAction action = CreateInstance<DiskFlyAction>();
        action.start_vector = Quaternion.Euler(new Vector3(0, 0, angle)) * Vector3.right * power;
        return action;
    }

    public override void Update()
    {
        time += Time.fixedDeltaTime;

        gravity_vector.y = 0;  //gravity * time;   

        transform.position += (start_vector + gravity_vector) * Time.fixedDeltaTime;
        current_angle.z = Mathf.Atan((start_vector.y + gravity_vector.y) / start_vector.x) * Mathf.Rad2Deg;
        transform.eulerAngles = current_angle;

        //动作做完
        if (this.transform.position.y < -10 || this.transform.position.y > 10)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }

    public override void Start() { }
}
