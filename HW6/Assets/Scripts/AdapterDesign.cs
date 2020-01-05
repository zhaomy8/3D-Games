using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;

public interface IActionManager
{
    void diskFly(GameObject disk, float angle, float power);
}

public class SSActionManager : MonoBehaviour, ISSActionCallback, IActionManager
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();    
    private List<SSAction> waitingAdd = new List<SSAction>();                       
    private List<int> waitingDelete = new List<int>();                                            

    public DiskFlyAction fly;                            //添加部分
    public FirstController scene_controller;             //添加部分

    protected void Start()
    {
        scene_controller = (FirstController)Director.GetInstance().currentSceneController;
        scene_controller.actionManager = this;

    }

    protected void Update()
    {
        foreach (SSAction ac in waitingAdd)
        {
            actions[ac.GetInstanceID()] = ac;
        }
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                ac.Update();
            }
        }

        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }

    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
    }

    public void diskFly(GameObject disk, float angle, float power)//添加部分
    {
        fly = DiskFlyAction.GetSSAction(angle, power);
        this.RunAction(disk, fly, this);
    }
}

public class PhysicActionManager : MonoBehaviour, IActionManager
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();    
    private List<SSAction> waitingAdd = new List<SSAction>();                       
    private List<int> waitingDelete = new List<int>();                                 

    public DiskFlyAction fly;                            
    public FirstController scene_controller;             

    protected void Start()
    {
        scene_controller = (FirstController)Director.GetInstance().currentSceneController;
        scene_controller.actionManager = this;

    }

    protected void FixedUpdate()
    {
        foreach (SSAction ac in waitingAdd)
        {
            actions[ac.GetInstanceID()] = ac;
        }
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                ac.Update();
            }
        }

        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }

    public void diskFly(GameObject disk, float angle, float power)
    {
        disk.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        disk.GetComponent<Rigidbody>().useGravity = false;
        disk.GetComponent<Rigidbody>().drag = (30 - power) / 20;   //round越大，power越大，阻力越小，速度越快
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
    } 
}