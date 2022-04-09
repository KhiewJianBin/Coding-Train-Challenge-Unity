using System.Collections.Generic;
using UnityEngine;

public class Barnsley_Fern : MonoBehaviour
{
    float x = 0;
    float y = 0;

    void Start()
    {
        P5JSExtension.background(0);
        P5JSExtension.dontclear();
    }

    void nextPoint()
    {
        float nextX = 0;
        float nextY = 0;
        float r = P5JSExtension.random(1f);

        if (r < 0.01f)
        {
            //1
            nextX = 0;
            nextY = 0.16f * y;
        }
        else if (r < 0.86f)
        {
            //3
            nextX = 0.85f * x + 0.04f * y;
            nextY = -0.04f * x + 0.85f * y + 1.6f;
        }
        else if (r < 0.93f)
        {
            //2
            nextX = 0.20f * x + -0.26f * y;
            nextY = 0.23f * x + 0.22f * y + 1.6f;
        }
        else
        {
            //4
            nextX = -0.15f * x + 0.28f * y;
            nextY = 0.26f * x + 0.24f * y + 0.44f;
        }

        x = nextX;
        y = nextY;
    }
    void drawPoint()
    {
        P5JSExtension.stroke(255);
        P5JSExtension.strokeWeight(2);
        float px = P5JSExtension.map(x, -2.1820f, 2.6558f, 0, P5JSExtension.width);
        float py = P5JSExtension.map(y, 0, 9.9983f, P5JSExtension.height, 0);
        P5JSExtension.point(px, py);
    }

    void OnGUI()
    {
        for (int i = 0; i < 100; i++)
        {
            drawPoint();
            nextPoint();
        }
    }
}