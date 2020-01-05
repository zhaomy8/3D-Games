using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;

public class SSAction : ScriptableObject
{
    public bool enable = true;                      //是否正在进行此动作
    public bool destroy = false;                    //是否需要被销毁
    public GameObject gameobject;                   //动作对象
    public Transform transform;                     //动作对象的transform
    public ISSActionCallback callback;              //动作完成后的消息通知者

    protected SSAction() { }
    //子类可以使用下面这两个函数
    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}

public class SequenceAction : SSAction, ISSActionCallback
{

    public List<SSAction> sequence;    
    public int repeat = -1;           
    public int start = 0;             

    public static SequenceAction GetSSAcition(int repeat, int start, List<SSAction> sequence)
    {
        SequenceAction action = ScriptableObject.CreateInstance<SequenceAction>();
        action.repeat = repeat;
        action.sequence = sequence;
        action.start = start;
        return action;
    }

    public override void Update()
    {
        if (sequence.Count == 0) return;
        if (start < sequence.Count)
        {
            sequence[start].Update();
        }
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
        source.destroy = false;
        this.start++;
        if (this.start >= sequence.Count)
        {
            this.start = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0)
            {
                this.destroy = true;
                this.callback.SSActionEvent(this);
            }
        }
    }

    public override void Start()
    {
        foreach (SSAction action in sequence)
        {
            action.gameobject = this.gameobject;
            action.transform = this.transform;
            action.callback = this;
            action.Start();
        }
    }

    void OnDestroy()
    {
    }


}

public class SSActionManager : MonoBehaviour, ISSActionCallback
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();    //将执行的动作的字典集合
    private List<SSAction> waitingAdd = new List<SSAction>();                       //等待去执行的动作列表
    private List<int> waitingDelete = new List<int>();                              //等待删除的动作的key                

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
}

public enum SSActionEventType : int
{
    Started,
    Completed
}

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Completed,
        int intParamameter = 0,
        string strParameter = null,
        Object objectParameter = null);

}