using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] GameObject KnifeModel;
    [SerializeField] Transform spawnpoint;
    [SerializeField] KnifeControl curKnife;
    [SerializeField] float spawnDelay = 0.1f;
    [SerializeField] bool allowSpawn = true;

    public delegate void CustomEvent();
    public static event CustomEvent onKnifeThrow;


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
            print("new knifeSpawn");
        }
    }

    IEnumerator delayKnifeSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        spawnNewKnife();
    }

    void Throw()
    {
        if(StatTracker.gameOngoing)
        {
            onKnifeThrow();
            StartCoroutine(delayKnifeSpawn());
        }
    }

    void DisableSpawn()
    {
        allowSpawn = false;
    }

    void OnEnable() {
        StatTracker.onLevelPass += DisableSpawn;
        StatTracker.onLevelFail += DisableSpawn;
        InputControler.onSpaceClicked += Throw;
    }

    void OnDisable() {
        StatTracker.onLevelPass -= DisableSpawn;
        StatTracker.onLevelFail -= DisableSpawn;
        InputControler.onSpaceClicked -= Throw;
        
    }

    
}
