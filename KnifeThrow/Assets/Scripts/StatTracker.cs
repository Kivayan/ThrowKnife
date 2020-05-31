using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    public int knifesTotal;
    public int knifesRemaining;
    public static bool gameOngoing = true;
    public static bool levelWon; 

    public Target curTarget;
    public int curLevelNumber = 0;
    public delegate void CustomEvent();
    public static event CustomEvent onLevelPass;
    public static event CustomEvent onLevelFail;


    [SerializeField] Transform spawnPoint;
    [SerializeField] List<GameObject> targetList;

    void Start()
    {
        curTarget = GetComponentInChildren<Target>();
        LoadGameFromStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(knifesTotal == knifesRemaining)
        {
            KnifeSpawner.allowThrow = true;
        }
    }

    public void SubstractKnife()
    {
    
        knifesRemaining -= 1;

        if (knifesRemaining == 0 && onLevelPass != null && gameOngoing)
        {
            KnifeSpawner.DisableSpawn();
            onLevelPass();
            levelWon = true;
        }
    
    }



    private void OnEnable()
    {
        // I know it's dirty but I have to sort out order. Target knives arent loaded in proper order

        onLevelFail += SetGameOngoingInactive;
        onLevelPass += SetGameOngoingInactive;
        KnifeSpawner.onKnifeThrow += SubstractKnife;
        UIController.onMessageDismiss += ContinueAfterLevel;
    }
    private void OnDisable()
    {

        KnifeSpawner.onKnifeThrow -= SubstractKnife;
        onLevelFail -= SetGameOngoingInactive;
        onLevelPass -= SetGameOngoingInactive;
        UIController.onMessageDismiss -= ContinueAfterLevel;
    }

    public static void ThrowFailed()
    {
        onLevelFail();
        levelWon = false;
    }

    private void GetKnifesCount()
    {
        knifesTotal = curTarget.knifes;
        knifesRemaining = curTarget.knifes;
    }

    void SetGameOngoingActive()
    {
        gameOngoing = true;
        Debug.Log("Game Ongoing");
    }

    void SetGameOngoingInactive() 
    {
        gameOngoing = false;
        Debug.Log("Game Ongoing _NOT");
    }

    void LoadNextLevel()
    {
        RemoveTarget();
        SpawnNewTarget(false);
        GetKnifesCount();
        SetGameOngoingActive();
        //KnifeSpawner.allowThrow = true;
    }

    void LoadGameFromStart()
    {
        if(curTarget!= null)
        {
            RemoveTarget();
        }

        SpawnNewTarget(true);
        GetKnifesCount();
        SetGameOngoingActive();
        
    }

     void RemoveTarget()
    {
        Destroy(curTarget.gameObject);
        curTarget = null;
    }

    void SpawnNewTarget(bool freshStart)
    {
        if(freshStart)
        {
            GameObject newTarget = Instantiate(targetList[0], spawnPoint.position, spawnPoint.rotation);
            newTarget.transform.SetParent(GetComponent<Transform>());
            curTarget = newTarget.GetComponent<Target>();
        }
        else
        {
            curLevelNumber += 1;
            GameObject newTarget = Instantiate(targetList[curLevelNumber], spawnPoint.position, spawnPoint.rotation);
            newTarget.transform.SetParent(GetComponent<Transform>());
            curTarget = newTarget.GetComponent<Target>();
        }
    }

    void ContinueAfterLevel()
    {
        if(levelWon)
        {
            LoadNextLevel();
        }
        else
        {
            LoadGameFromStart();
        }
    }
}
