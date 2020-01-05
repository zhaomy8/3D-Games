using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;

 public class FirstActionManager : SSActionManager
    {

        public DiskFlyAction fly;                            //飞碟飞行的动作
        public FirstController scene_controller;             //当前场景的场景控制器

        protected void Start()
        {
            scene_controller = (FirstController)Director.GetInstance().currentSceneController;
            scene_controller.actionManager = this;
        }

        //飞碟飞行
        public void diskFly(GameObject disk, float angle, float power)
        {
            fly = DiskFlyAction.GetSSAction(angle, power); 
            this.RunAction(disk, fly, this);
        }
    }