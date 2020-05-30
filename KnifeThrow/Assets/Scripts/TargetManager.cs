using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{

    [SerializeField] GameObject curTarget;
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<GameObject> targetList;


    public delegate void CustomEvent();
    public static event CustomEvent onTargetLoad;

    void Start()
    {
        curTarget = GetComponentInChildren<Target>().gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            LoadNewTarget();
        }
    }

    void RemoveTarget()
    {
        Destroy(curTarget.gameObject);
        curTarget = null;
    }

    void SpawnNewTarget()
    {
        GameObject newTarget = Instantiate(targetList[1], spawnPoint.position, spawnPoint.rotation);
        curTarget = newTarget;

    }

    void LoadNewTarget()
    {

        RemoveTarget();
        SpawnNewTarget();
        onTargetLoad();
    }
    

    

}
