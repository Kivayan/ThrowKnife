using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputControler : MonoBehaviour
{

    public static event Action onSpaceClicked;

    void Update()
    {
        SpaceMonitor();
    }

    void SpaceMonitor()
    {
        if(Input.GetKeyDown("space"))
        {
            onSpaceClicked();
        }
    }

    void start()
    {

    }
}
