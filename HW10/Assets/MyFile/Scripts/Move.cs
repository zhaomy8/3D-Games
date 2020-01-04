using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject camera;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 20, this.transform.position.z - 10);
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("forward");
            this.GetComponent<Rigidbody>().velocity = this.transform.forward * 10;
            //action.moveForward();
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("back");
            //action.moveBackWard();
            this.GetComponent<Rigidbody>().velocity = this.transform.forward * -10;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Ture");
            //action.shoot();
        }

        //获取水平轴上的增量，目的在于控制玩家坦克的转向
        float offsetX = Input.GetAxis("Horizontal");
        float x = this.transform.localEulerAngles.x;
        float y = this.transform.localEulerAngles.y + offsetX * 2;
        this.transform.localEulerAngles = new Vector3(x, y, 0);
        //action.turn(offsetX);
    }

}
