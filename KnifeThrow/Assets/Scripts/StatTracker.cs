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
    public static event CustomEvent onLevelStart;


    void Start()
    {
        
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



    private void OnEnable()
    {
        // I know it's dirty but I have to sort out order. Target knives arent loaded in proper order
        LoadTarget();
        onLevelStart += LoadTarget;
        KnifeSpawner.onKnifeThrow += SubstractKnife;
        onLevelStart();

    }
    private void OnDisable()
    {

        KnifeSpawner.onKnifeThrow -= SubstractKnife;
        onLevelStart += LoadTarget;
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
