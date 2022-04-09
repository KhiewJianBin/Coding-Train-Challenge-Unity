using System.Collections.Generic;
using UnityEngine;

public class Recursion : MonoBehaviour
{
    
    void Start()
    {
        
    }
    void OnGUI()
    {
        P5JSExtension.stroke(255);
        P5JSExtension.noFill();
        drawCircle(300, 200, 300);
    }
    void drawCircle(float x,float y,float d)
    {
        P5JSExtension.ellipse(x, y, d);
        if(d > 2)
        {
            var newD = d * 0.25f;
            drawCircle(x + newD, y, newD);
            drawCircle(x - newD, y, newD);
            //drawCircle(x, y - d * 0.5f, d * 0.5f);
            //drawCircle(x, y + d * 0.5f, d * 0.5f);
        }
    }
}