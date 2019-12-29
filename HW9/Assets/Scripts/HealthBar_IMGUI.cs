using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar_IMGUI : MonoBehaviour {
    public float currentHealth; //当前血量值
    public float changedHealth; //增/减后的血量值

    private Rect HealthBar;     //血条区域
    private Rect AddButtonArea; //加血按钮区域  
    private Rect SubButtonArea; //减血按钮区域

    void Start()
    {
        //设置血条和按钮显示区域的位置和大小
        HealthBar = new Rect(120, 100, 200, 20);
        SubButtonArea = new Rect(350, 100, 40, 20);
        AddButtonArea = new Rect(410, 100, 40, 20);

        currentHealth = changedHealth = 0.0f;//初始化
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

        // 用水平滚动条的宽度作为血条的显示值
        GUI.HorizontalScrollbar(HealthBar, 0.0f, currentHealth, 0.0f, 1.0f);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
