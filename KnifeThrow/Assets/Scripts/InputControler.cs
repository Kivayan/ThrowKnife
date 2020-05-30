using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControler : MonoBehaviour
{
    public delegate void CustomEvent();
    public static event CustomEvent onSpaceClicked;

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
}
