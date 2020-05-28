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

        ThrowMonitor();
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

    IEnumerator delay()
    {
        yield return new WaitForSeconds(spawnDelay);
        spawnNewKnife();
    }
    // TODO move to separate class, space will do different things depending on context (next on win/loose screen)
    void ThrowMonitor()
    {
        if(Input.GetKeyDown("space") && StatTracker.gameOngoing)
        {
            onKnifeThrow();
            StartCoroutine(delay());
        }
    }
    void DisableSpawn()
    {
        allowSpawn = false;
    }

    void OnEnable() {
        StatTracker.onLevelPass += DisableSpawn;
        StatTracker.onLevelFail += DisableSpawn;
    }

    void OnDiable() {
        StatTracker.onLevelPass -= DisableSpawn;
        StatTracker.onLevelFail -= DisableSpawn;
        
    }
}
