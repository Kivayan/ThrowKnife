using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    public GameObject KnifeModel;
    public Transform spawnpoint;
    public KnifeControl curKnife;
    public float spawnDelay = 0.1f;
    public bool allowSpawn = true;
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

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(spawnDelay);
        spawnNewKnife();
    }

    private void ThrowMonitor()
    {
        if(Input.GetKeyDown("space") && StatTracker.gameOngoing)
        {
            onKnifeThrow();
            StartCoroutine(delay());
        }
    }
    public void DisableSpawn()
    {
        allowSpawn = false;
    }

    private void OnEnable() {
        StatTracker.onLevelPass += DisableSpawn;
        StatTracker.onLevelFail += DisableSpawn;
    }

    private void OnDiable() {
        StatTracker.onLevelPass -= DisableSpawn;
        StatTracker.onLevelFail -= DisableSpawn;
        
    }
}
