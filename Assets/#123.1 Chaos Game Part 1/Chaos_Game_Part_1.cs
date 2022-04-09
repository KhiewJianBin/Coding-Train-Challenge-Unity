using System.Collections.Generic;
using UnityEngine;

public class Chaos_Game_Part_1 : MonoBehaviour
{
    float ax, ay;
    float bx, by;
    float cx, cy;
    float dx, dy;

    float x, y;
    void Start()
    {
        ax = P5JSExtension.width/2;
        ay = 0;
        bx = 0;
        by = P5JSExtension.height;
        cx = P5JSExtension.width;
        cy = P5JSExtension.height;

        x = P5JSExtension.random(P5JSExtension.width);
        y = P5JSExtension.random(P5JSExtension.height);

        //P5JSExtension.background(0);
    }
    void OnGUI()
    {
        P5JSExtension.stroke(255);
        P5JSExtension.strokeWeight(8);
        P5JSExtension.point(ax, ay);
        P5JSExtension.point(bx, by);
        P5JSExtension.point(cx, cy);

        for(int i = 0; i < 100;i++)
        {
            P5JSExtension.strokeWeight(2);
            P5JSExtension.point(x, y);

            var r = Mathf.Floor(P5JSExtension.random(3));

            if (r == 0)
            {
                P5JSExtension.stroke(255, 0, 255);
                x = Mathf.Lerp(x, ax, 0.5f);
                y = Mathf.Lerp(y, ay, 0.5f);
            }
            else if (r == 1)
            {
                P5JSExtension.stroke(0, 0, 255);
                x = Mathf.Lerp(x, bx, 0.5f);
                y = Mathf.Lerp(y, by, 0.5f);
            }
            else if (r == 2)
            {
                P5JSExtension.stroke(255, 255, 0);
                x = Mathf.Lerp(x, cx, 0.5f);
                y = Mathf.Lerp(y, cy, 0.5f);
            }
        }
    }
}