using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UGUI : MonoBehaviour
{
    public Slider HealthBar;     //血条

    public float currentHealth; //当前血量值
    public float changedHealth; //增/减后的血量值

    private Rect AddButtonArea; //加血按钮区域  
    private Rect SubButtonArea; //减血按钮区域

    void Start()
    {
        SubButtonArea = new Rect(350, 100, 40, 20);
        AddButtonArea = new Rect(410, 100, 40, 20);
        currentHealth = changedHealth = 0.0f;
    }

    void OnGUI()
    {
        if (GUI.Button(AddButtonArea, "加血"))     //设置加血GUI按钮和点击事件
        {
            changedHealth = changedHealth + 0.1f > 1.0f ? 1.0f : changedHealth + 0.1f;
        }
        if (GUI.Button(SubButtonArea, "减血"))   //设置减血GUI按钮和点击事件
        {
            changedHealth = changedHealth - 0.1f < 0.0f ? 0.0f : changedHealth - 0.1f;
        }

        //插值计算health值，以实现血条值平滑变化
        currentHealth = Mathf.Lerp(currentHealth, changedHealth, 0.05f);

        HealthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Camera.main.transform.position);
    }
}
