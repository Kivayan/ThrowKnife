using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] StatTracker statTracker;
    [SerializeField] Text knifesText;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject looseScreen;

    public delegate void CustomEvent();
    public static event CustomEvent onMessageDismiss;


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

        //KnifeSpawner.onKnifeThrow += SetKnifesText;
        StatTracker.onLevelPass += showWin;
        StatTracker.onLevelFail += showLoose;
        InputControler.onSpaceClicked += hideLoose;
        InputControler.onSpaceClicked += hideWin;

    }

    void OnDisable()
    {

        //KnifeSpawner.onKnifeThrow -= SetKnifesText;
        StatTracker.onLevelPass -= showWin;
        StatTracker.onLevelFail -= showLoose;
        InputControler.onSpaceClicked -= hideLoose;
        InputControler.onSpaceClicked -= hideWin;
    }

    void SetKnifesText()
    {
        string KnifeCount = "Knifes " + statTracker.knifesRemaining.ToString() + "/" + statTracker.knifesTotal.ToString();
        knifesText.text = KnifeCount;
    }

    void showLoose()
    {
        looseScreen.SetActive(true);
    }

    void showWin()
    {
        winScreen.SetActive(true);
        Debug.Log("Showing Text");
    }


    void hideLoose()
    {
        if(!StatTracker.gameOngoing && !StatTracker.levelWon)
        {
            Debug.Log("HIDING LOOSE");
            looseScreen.SetActive(false);
            onMessageDismiss();
        }
    } 

    void hideWin()
    {
        if(!StatTracker.gameOngoing && StatTracker.levelWon)
        {
            Debug.Log("HIDING WIN");
            winScreen.SetActive(false);
            onMessageDismiss();
        }
    }



}
