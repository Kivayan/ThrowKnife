using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{

    [SerializeField] StatTracker statTracker;
    [SerializeField] Text knifesText;
    [SerializeField] Text levelText;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject looseScreen;
    [SerializeField] GameObject MenuUI;
    GameObject activeScreen;

    public static event Action onMessageDismiss;


    public void ToogleUI(bool gameOn)
    {
        knifesText.gameObject.SetActive(gameOn);
        levelText.gameObject.SetActive(gameOn);
        MenuUI.SetActive(!gameOn);
    }

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
        SetLevelText();
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

    public void SetLevelText()
    {
        string level =  "Level " + statTracker.curLevelNumber.ToString();
        levelText.text = level;
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
                looseScreen.SetActive(false);
                onMessageDismiss();
            }
            if(StatTracker.levelWon)
            {
                winScreen.SetActive(false);
                onMessageDismiss();
            }
            
        } 
    }
        

}
