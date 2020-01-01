using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;
public class FirstSceneActionManager : SSActionManager
{
    public float boatSpeed = 5.0F;
    public float roleSpeed = 5.0F;

    private SSMoveToAction move_boat_action;
    private SequenceAction move_role_action;

    protected new void Start()
    {
    }
    public void moveBoat(GameObject boat, Vector3 target, float speed)
    {

        move_boat_action = SSMoveToAction.GetSSAction(target, speed);
        this.RunAction(boat, move_boat_action, this);
    }

    public void moveRole(GameObject role, Vector3 middle_pos, Vector3 end_pos, float speed)
    {
        SSAction action1 = SSMoveToAction.GetSSAction(middle_pos, speed);
        SSAction action2 = SSMoveToAction.GetSSAction(end_pos, speed);
        move_role_action = SequenceAction.GetSSAcition(1, 0, new List<SSAction> { action1, action2 });
        this.RunAction(role, move_role_action, this);
    }


}