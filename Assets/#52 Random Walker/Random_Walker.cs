using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Random_Walker : MonoBehaviour
{
    float x;
    float y;

    void Start()
    {
        x = 200;
        y = 200;

        P5JSExtension.background(51);
        P5JSExtension.dontclear();
    }
    void OnGUI()
    {
        P5JSExtension.stroke(255);
        P5JSExtension.strokeWeight(2);
        P5JSExtension.point(x, y);

        var r = Mathf.FloorToInt(P5JSExtension.random(4f));

        switch(r)
        {
            case 0:
                x = x + 1;
                break;
            case 1:
                x = x - 1;
                break;
            case 2:
                y = y + 1;
                break;
            case 3:
                y = y - 1;
                break;
        }
    }
}