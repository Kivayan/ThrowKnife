using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatTracker : MonoBehaviour
{
    public int knifesTotal;
    public int knifesRemaining;
    public static bool gameOngoing = false;
    public static bool levelWon;

    public Target curTarget;
    public int curLevelNumber = 0;

    public static event Action<LevelResult> onLevelEnd = details => { };

    [SerializeField] UIController UIController;

    [SerializeField] Transform spawnPoint;
    [SerializeField] List<GameObject> targetList;
    [SerializeField] List<GameObject> bossList;
    [SerializeField] int bossInterval;

    void Start()

    {
        curTarget = GetComponentInChildren<Target>();
        ;

        targetList = CreateLevelOrder(bossInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (knifesTotal == knifesRemaining)
        {
            KnifeSpawner.allowThrow = true;
        }
    }

    public void SubstractKnife()
    {
        if (gameOngoing)
        {
            knifesRemaining -= 1;
        }

        if (knifesRemaining == 0 && gameOngoing)
        {
            EndLevel(new LevelResult { lvlResult = LevelResult.Result.win });
        }

    }

    private void OnEnable()
    {
        // I know it's dirty but I have to sort out order. Target knives arent loaded in proper order

        KnifeSpawner.onKnifeThrow += SubstractKnife;
        UIController.onMessageDismiss += ContinueAfterLevel;
    }


    private void OnDisable()
    {
        KnifeSpawner.onKnifeThrow -= SubstractKnife;
        UIController.onMessageDismiss -= ContinueAfterLevel;
    }

    private static void EndLevel(LevelResult result)
    {
        KnifeSpawner.DisableSpawn();
        gameOngoing = false;
        KnifeSpawner.DisableSpawn();
        KnifeSpawner.DisableTrow();
        onLevelEnd.Invoke(result);

        if (result.lvlResult == LevelResult.Result.win)
        {
            levelWon = true;
        }
        else
        {
            levelWon = false;
        }

    }

    public static void ThrowFailed()
    {
        if (gameOngoing)
        {
            EndLevel(new LevelResult { lvlResult = LevelResult.Result.loose });
        }
    }

    private void GetKnifesCount()
    {
        knifesTotal = curTarget.knifes;
        knifesRemaining = curTarget.knifes;
    }

    void LoadNextLevel()
    {
        RemoveTarget();
        SpawnNewTarget(false);
        GetKnifesCount();


        gameOngoing = true;
        KnifeSpawner.EnableSpawn();
        //KnifeSpawner.allowThrow = true;
    }

    void LoadGameFromStart()
    {
        if (curTarget != null)
        {
            RemoveTarget();
        }
;
        SpawnNewTarget(true);
        GetKnifesCount();
        KnifeSpawner.EnableSpawn();
        gameOngoing = true;
    }

    void RemoveTarget()
    {
        Destroy(curTarget.gameObject);
        curTarget = null;
    }

    void SpawnNewTarget(bool freshStart)
    {
        if (freshStart)
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
        if (levelWon)
        {
            LoadNextLevel();
        }
        else
        {
            LoadGameFromStart();
        }
    }

    List<GameObject> CreateLevelOrder(int bossInterval)
    {
        List<GameObject> finalLevelList = new List<GameObject>();   
        int posCounter = 0;
        int lastTargetTaken = 0;
        int lastBossTaken = 0 ;
        int levelListLenght = targetList.Count + (targetList.Count / bossInterval) - 1 ;

        while (posCounter <= levelListLenght)
        {
            if(posCounter != 0 && (posCounter - bossInterval + 1 )  % bossInterval == 0)
            {
                if(lastBossTaken >= bossList.Count)
                {
                    lastBossTaken = 0;
                }
                finalLevelList.Add(bossList[lastBossTaken]);
                lastBossTaken ++;
                posCounter++;
            }
            else 
            {
                finalLevelList.Add(targetList[lastTargetTaken]);
                lastTargetTaken ++;
                posCounter ++;
            }
        }
        return finalLevelList;
    }


    public void StartGame()
    {
        UIController.ToogleUI(true);
        LoadGameFromStart();
        gameOngoing = true;

    }

}

public class LevelResult
{
    public enum Result { win, loose }
    public Result lvlResult;
}
