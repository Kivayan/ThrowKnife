using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{

    [SerializeField] StatTracker statTracker;
    [SerializeField] Text knifesText;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject looseScreen;
    GameObject activeScreen;

    public static event Action onMessageDismiss;


    void Start()
    {
        statTracker = GetComponentInParent<StatTracker>();

        SetKnifesText();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO Trigger only after knifet throw. Order prolems for now
        SetKnifesText();
    }

    void OnEnable()
    {
        StatTracker.onLevelEnd +=ShowEndScreen;
        InputControler.onSpaceClicked += hideEndScreen;
    }

    void OnDisable()
    {


        StatTracker.onLevelEnd -=ShowEndScreen;
        InputControler.onSpaceClicked -= hideEndScreen;
    }

    void SetKnifesText()
    {
        string KnifeCount = "Knifes " + statTracker.knifesRemaining.ToString() + "/" + statTracker.knifesTotal.ToString();
        knifesText.text = KnifeCount;
    }

    void ShowEndScreen(LevelResult result)
    {
        if(result.lvlResult == LevelResult.Result.win)
        {
            winScreen.SetActive(true);
            activeScreen = winScreen;
        }

        if(result.lvlResult == LevelResult.Result.loose)
        {
            looseScreen.SetActive(true);
            activeScreen  = looseScreen;
        }
    }

    void hideEndScreen()
    {
        if(!StatTracker.gameOngoing)
        {
            if (!StatTracker.levelWon)
            {
                Debug.Log("HIDING LOOSE");
                looseScreen.SetActive(false);
                onMessageDismiss();
            }
            if(StatTracker.levelWon)
            {
                Debug.Log("HIDING WIN");
                winScreen.SetActive(false);
                onMessageDismiss();
            }
            
        } 
    }
        

}
