using System.Collections.Generic;
using UnityEngine;

public class Chaos_Game_Part_2 : MonoBehaviour
{
    int frameCount = 0;

    List<Vector2> points = new List<Vector2>();

    Vector2 current;
    float percent = 0.9f;
    Vector2 previous;

    void Start()
    {
        points.Clear();
        float n = 15;

        for (int i = 0; i < n; i++)
        {
            var angle = i * 2 * Mathf.PI / n;
            var v = P5JSExtension.fromAngle(angle);
            v *= P5JSExtension.width/2;
            v += new Vector2(P5JSExtension.width / 2, P5JSExtension.height / 2);
            points.Add(v);
        }

        reset();
    }
    void reset()
    {
        current = new Vector2(P5JSExtension.random(P5JSExtension.width), P5JSExtension.random(P5JSExtension.height));
        P5JSExtension.background(0); Invoke("dontclear", 0.1f);
    }
    //extra
    void dontclear()
    {
        P5JSExtension.dontclear();
    }
    void OnGUI()
    {
        frameCount++;
        if (frameCount % 100 == 0)
        {
            //reset();
        }

        P5JSExtension.stroke(255);
        P5JSExtension.strokeWeight(8);
        foreach (var p in points)
        {
            P5JSExtension.point(p.x, p.y);
        }

        for(int i = 0; i < 1000; i++)
        {
            P5JSExtension.strokeWeight(1);
            P5JSExtension.stroke(255);

            var next = P5JSExtension.random(points);
            if(next != previous)
            {
                current.x = Mathf.Lerp(current.x, next.x, percent);
                current.y = Mathf.Lerp(current.y, next.y, percent);
                P5JSExtension.point(current.x, current.y);
            }

            previous = next;
        }
    }
}