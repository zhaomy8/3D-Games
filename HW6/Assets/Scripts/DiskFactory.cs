using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  myGame;

public class DiskFactory : MonoBehaviour
{
    public GameObject diskPrefab;

    private List<GameObject> usingDisks = new List<GameObject>();   //正在使用
    private List<GameObject> freeDisks = new List<GameObject>();    //使用过已被释放的，可以重复使用

    int nameIndex;

    private void Awake()
    {
        //diskPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/disk"), Vector3.zero, Quaternion.identity);
        //Instantiate<Transform>(brick, new Vector3(x, y, 0), Quaterni on.identity
        diskPrefab.name = "prefab";
        diskPrefab.SetActive(false);
        nameIndex = 0;
    }

    public GameObject getDisk(int round)
    {
        GameObject newDisk = null;
        if (freeDisks.Count > 0)
        {
            newDisk = freeDisks[0].gameObject;
            freeDisks.Remove(freeDisks[0]);
        }
        else
        {
            newDisk = GameObject.Instantiate<GameObject>(diskPrefab, Vector3.zero, Quaternion.identity);
            newDisk.AddComponent<Disk>();
            newDisk.AddComponent<Rigidbody>();
            newDisk.GetComponent<Rigidbody>().useGravity = false;
            newDisk.name = nameIndex.ToString();
            nameIndex++;
        }
        return newDisk;
    }

    public void freeDisk(GameObject usedDisk)
    {
        if (usedDisk != null)
        {
            usedDisk.SetActive(false);
            usingDisks.Remove(usedDisk);
            freeDisks.Add(usedDisk);
        }
    }
    
}
