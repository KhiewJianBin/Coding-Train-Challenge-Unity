using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Random_Walker_with_Vectors_and_Levy_Flight : MonoBehaviour
{
    Vector2 pos = new Vector2(0,0);
    Vector2 prev;

    void Start()
    {
        pos.x = 200;
        pos.y = 200;

        P5JSExtension.background(51);
        P5JSExtension.dontclear();
    }
    void OnGUI()
    {
        P5JSExtension.stroke(255);
        P5JSExtension.strokeWeight(2);
        //P5JSExtension.point(pos.x, pos.y);
        P5JSExtension.line(pos.x, pos.y, prev.x, prev.y);
        prev = pos;

        Vector2 step = P5JSExtension.random2D();

        var r = P5JSExtension.random(100);
        if(r < 1)
        {
            step *= P5JSExtension.random(25, 100);
        }
        else
        {
            step = step.setMag(2);
        }

        pos = pos + step;
    }
}