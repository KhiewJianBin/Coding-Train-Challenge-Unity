using System.Collections.Generic;
using UnityEngine;

public class _10PRINT : MonoBehaviour
{
    float x = 0;
    float y = 0;
    float spacing = 10;
    void Start()
    {
        
    }
    void OnGUI()
    {
        P5JSExtension.stroke(255);
        if (P5JSExtension.random(1f) < 0.9f)
        {
            P5JSExtension.line(x, y, x + spacing, y+ spacing);
        }
        else
        {
            P5JSExtension.line(x, y + spacing, x + spacing, y);
        }
        x = x + spacing;
        if(x > P5JSExtension.width)
        {
            x = 0;
            y = y + spacing;
        }
    }
}