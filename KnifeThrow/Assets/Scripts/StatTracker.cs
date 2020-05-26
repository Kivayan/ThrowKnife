using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    public int knifesTotal;
    public int knifesRemaining;
    public static bool gameOngoing = true;

    public Target curTarget;
    public delegate void CustomEvent();
    public static event CustomEvent onLevelPass;
    public static event CustomEvent onLevelFail;


    void Start()
    {
        LoadTarget();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SubstractKnife()
    {
        knifesRemaining -= 1;

        if (knifesRemaining == 0 && onLevelPass != null)
        {
            onLevelPass();
        }
    }

    public void LevelEnd()
    {

    }


    public void StartLevel()
    {

    }

    private void OnEnable()
    {

        KnifeSpawner.onKnifeThrow += SubstractKnife;
        onLevelPass += LevelEnd;
    }
    private void OnDisable()
    {

        KnifeSpawner.onKnifeThrow -= SubstractKnife;
        onLevelPass -= LevelEnd;
    }

    public static void ThrowFailed()
    {
        onLevelFail();
        print("faaail");
    }

    private void LoadTarget()
    {
        curTarget = GetComponentInChildren<Target>();
        knifesTotal = curTarget.knifes;
        knifesRemaining = curTarget.knifes;
    }
}
