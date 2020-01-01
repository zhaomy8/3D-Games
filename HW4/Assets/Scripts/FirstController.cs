using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class FirstController : MonoBehaviour, SceneController, UserAction
{

    readonly Vector3 water_pos = new Vector3(0, 0.5F, 0);

    UserGUI userGUI;

    public CoastController fromCoast;
    public CoastController toCoast;
    public BoatController boat;
    private MyCharacterController[] characters;
    private GameObject camera;

    private FirstSceneActionManager myActionManager;//添加动作管理器
    private Judgment judgment; //添加裁判
    void Awake()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        characters = new MyCharacterController[6];

        myActionManager = gameObject.AddComponent<FirstSceneActionManager>() as FirstSceneActionManager;   //动作管理器部件获取
        judgment = gameObject.AddComponent<Judgment>() as Judgment;   //裁判获取
        camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(0,3,-15);
        loadResources();
    }

    public void loadResources()
    {
        GameObject water = Instantiate(Resources.Load("Perfabs/Water", typeof(GameObject)), water_pos, Quaternion.identity, null) as GameObject;
        water.name = "water";

        fromCoast = new CoastController("from");
        toCoast = new CoastController("to");
        boat = new BoatController();

        for (int i = 0; i < 3; i++)
        {
            MyCharacterController cha = new MyCharacterController("priest");
            cha.setName("priest" + i);
            cha.setPosition(fromCoast.getEmptyPosition());
            cha.getOnCoast(fromCoast);
            fromCoast.getOnCoast(cha);

            characters[i] = cha;
        }

        for (int i = 0; i < 3; i++)
        {
            MyCharacterController cha = new MyCharacterController("devil");
            cha.setName("devil" + i);
            cha.setPosition(fromCoast.getEmptyPosition());
            cha.getOnCoast(fromCoast);
            fromCoast.getOnCoast(cha);

            characters[i + 3] = cha;
        }
    }

    public void moveBoat()
    {
        if (boat.isEmpty())
            return;
        //boat.Move();
        myActionManager.moveBoat(boat.getGameobj(), boat.getDestination(), myActionManager.boatSpeed);
        userGUI.status = judgment.check_game_over(boat, fromCoast, toCoast);
    }

    public void characterIsClicked(MyCharacterController characterCtrl)
    {
        if (characterCtrl.isOnBoat())
        {
            CoastController whichCoast;
            if (boat.get_to_or_from() == -1)
            { // to->-1; from->1
                whichCoast = toCoast;
            }
            else
            {
                whichCoast = fromCoast;
            }

            boat.GetOffBoat(characterCtrl.getName());

            //characterCtrl.moveToPosition(whichCoast.getEmptyPosition());
            
            Vector3 end_pos = whichCoast.getEmptyPosition();                                      //修改部分
            Vector3 middle_pos = new Vector3(characterCtrl.getGameObj().transform.position.x, end_pos.y, end_pos.z);  //修改部分
            myActionManager.moveRole(characterCtrl.getGameObj(), middle_pos, end_pos, myActionManager.roleSpeed);  //修改部分
            whichCoast.getOnCoast(characterCtrl);
            characterCtrl.getOnCoast(whichCoast);
        }
        else
        {                                   // character on coast
            CoastController whichCoast = characterCtrl.getCoastController();

            if (boat.getEmptyIndex() == -1)
            {       // boat is full
                return;
            }

            if (whichCoast.get_to_or_from() != boat.get_to_or_from())   // boat is not on the side of character
                return;

            Vector3 end_pos = boat.getEmptyPosition();                                            //修改部分
            Vector3 middle_pos = new Vector3(end_pos.x, characterCtrl.getGameObj().transform.position.y, end_pos.z); //修改部分
            myActionManager.moveRole(characterCtrl.getGameObj(), middle_pos, end_pos, myActionManager.boatSpeed);  //修改部分

            whichCoast.getOffCoast(characterCtrl.getName());
            //characterCtrl.moveToPosition(boat.getEmptyPosition());
            characterCtrl.getOnBoat(boat);
            boat.GetOnBoat(characterCtrl);
        }
        userGUI.status = judgment.check_game_over(boat, fromCoast, toCoast);
    }

    public void restart()
    {
        userGUI.status = 0;
        boat.reset();
        fromCoast.reset();
        toCoast.reset();
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].reset();
        }
    }
}
