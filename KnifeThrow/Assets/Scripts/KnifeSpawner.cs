using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] GameObject KnifeModel;
    [SerializeField] Transform spawnpoint;
    [SerializeField] KnifeControl curKnife;
    [SerializeField] float spawnDelay = 0.1f;
    [SerializeField] static bool allowSpawn = true;
    [SerializeField] public static bool allowThrow = true;

    public static event Action onKnifeThrow;


    void Start()
    {
        curKnife = GetComponentInChildren<KnifeControl>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void spawnNewKnife()
    {
        if(allowSpawn)
        {
            GameObject newKnife = Instantiate(KnifeModel,spawnpoint.position, spawnpoint.rotation);
            curKnife = newKnife.GetComponent<KnifeControl>();
            curKnife.transform.SetParent(transform);
        }
    }

    IEnumerator delayKnifeSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        spawnNewKnife();
    }

    void Throw()
    {
        if(StatTracker.gameOngoing && allowThrow)
        {
            onKnifeThrow();
            StartCoroutine(delayKnifeSpawn());
        }
    }

    public static void DisableSpawn()
    {
        allowSpawn = false;
    }

    public static void EnableSpawn()
    {
        allowSpawn = true;
    }

    public static void DisableTrow()
    {
        allowThrow = false;
    }


    void OnEnable() {

        InputControler.onSpaceClicked += Throw;
        UIController.onMessageDismiss += newLevelPrep;

    }

    void OnDisable() {

        InputControler.onSpaceClicked -= Throw;
        UIController.onMessageDismiss -= newLevelPrep;
        
    }

    void newLevelPrep()
    {
        EnableSpawn();
        spawnNewKnife();
    }

    
}
