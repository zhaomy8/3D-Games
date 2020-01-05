using myGame;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, UserAction
{

    public int round;
    public int trial;
    public float interval;
    public int score;
    private UserGUI userGUI;
    private Ruler ruler;
    public FirstActionManager actionManager;
    public GameObject cam;
    private Queue<GameObject> disksQueue = new Queue<GameObject>();
    private bool isRestart;
    void Awake()
    {
        Director director = Director.GetInstance();
        director.currentSceneController = this;
        director.currentSceneController.loadResources();

        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        //this.gameObject.AddComponent<DiskFactory>();
        actionManager = gameObject.AddComponent<FirstActionManager>() as FirstActionManager;

        ruler = gameObject.AddComponent<Ruler>() as Ruler;
        cam.transform.position = new Vector3(0,-2,-15);
    }
    public void loadResources()
    {
        getDisksForNextRound();
    }
    public void getDisksForNextRound()
    {
        DiskFactory diskFactory = Singleton<DiskFactory>.Instance;
        int numDisk = 10;
        for (int i = 0; i < numDisk; i++)
        {
            GameObject disk = diskFactory.getDisk(round);
            disksQueue.Enqueue(disk);
        }
    }

    // Use this for initialization
    void Start()
    {
        round = 1;
        interval = 0;
        trial = 0;
        loadResources();
        userGUI.targetThisRound = ruler.getTargetThisRound(round);
    }

    // Update is called once per frame
    void Update()
    {
        if (ruler.enterNextRound(round))
        {
            round++;
            trial = 0;
            getDisksForNextRound();
            userGUI.score = this.score = 0;
            userGUI.targetThisRound = ruler.getTargetThisRound(round);
        }
        else if (!ruler.enterNextRound(round) && trial == 11)
        {
            round = -1;
        }

        if (this.round >= 1)
        {
            if (interval > ruler.setInterval(round))
            {
                if (trial < 10)
                {
                    throwDisk();
                    interval = 0;
                    trial++;
                }
                else if (trial == 10)
                {
                    trial++;
                }
            }
            else
            {
                interval += Time.deltaTime;
            }
        }

        userGUI.round = this.round;
    }

    public void throwDisk()
    {
        if (disksQueue.Count != 0)
        {
            GameObject disk = disksQueue.Dequeue();
            setDiskProperty(disk, round);
            disk.SetActive(true);
            actionManager.diskFly(disk, disk.GetComponent<Disk>().angle, disk.GetComponent<Disk>().power);
        }
    }

    public void hit(Vector3 pos)
    {
        Camera ca;
        if (cam != null) ca = cam.GetComponent<Camera>();
        else ca = Camera.main;
        Ray ray = ca.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.gameObject.GetComponent<Disk>() != null)
            {
                //Debug.Log("hit");
                hit.collider.gameObject.SetActive(false);
                userGUI.score += 1;
                ruler.updateScore(round - 1);
            }
        }
    }


    public void setDiskProperty(GameObject disk, int round)
    {
        disk.transform.position = this.setRandomInitPos();
        disk.GetComponent<Renderer>().material.color = setRandomColor();
        disk.GetComponent<Disk>().angle = setRandomAngle();
        disk.transform.localScale = ruler.setScale(round);
        disk.GetComponent<Disk>().power = ruler.setPower(round);
    }

    public Vector3 setRandomInitPos()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-1f, 4f);
        float z = Random.Range(-3f, 3f);
        return new Vector3(x, y, z);
    }

    public Vector4 setRandomColor()
    {
        int r = Random.Range(0f, 1f) > 0.5 ? 255 : 0;
        int g = Random.Range(0f, 1f) > 0.5 ? 255 : 0;
        int b = Random.Range(0f, 1f) > 0.5 ? 255 : 0;
        return new Vector4(r, g, b, 1);
    }

    public float setRandomAngle()
    {
        return Random.Range(-360f, 360f);
    }

    public void restart()
    {
        Debug.Log("restart");
        round = 1;
        userGUI.round = 1;
        interval = 0;
        trial = 0;
        ruler.init();
        userGUI.score = ruler.score[round];
        getDisksForNextRound();
        userGUI.targetThisRound = ruler.getTargetThisRound(round);
    }


}