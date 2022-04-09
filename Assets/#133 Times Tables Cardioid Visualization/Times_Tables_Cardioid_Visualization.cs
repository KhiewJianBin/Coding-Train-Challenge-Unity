using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Times_Tables_Cardioid_Visualization : MonoBehaviour
{
    float r;
    float factor = 0;

    void Start()
    {
        //640 x 640
        r = P5JSExtension.width / 2 - 16;
    }
    Vector2 getVector(float index, float total)
    {
        float angle = P5JSExtension.map(index%total, 0, total, 0, 2 * Mathf.PI);
        Vector2 v = P5JSExtension.fromAngle(angle+Mathf.PI);
        v = v * r;
        return v;
    }
    void OnGUI()
    {
        P5JSExtension.background(0);

        int total = 200;//(int)P5JSExtension.map(Input.mousePosition.x, 0, P5JSExtension.width, 0, 200);

        factor += 0.01f;

        P5JSExtension.resetMatrix();
        P5JSExtension.translate(P5JSExtension.width / 2, P5JSExtension.height / 2);
        P5JSExtension.stroke(255);
        P5JSExtension.noFill(); 
        P5JSExtension.circle(0, 0, r * 2);
        for(int i = 0; i < total; i++)
        {
            Vector2 v = getVector(i,total);
            P5JSExtension.fill(255);
            P5JSExtension.circle(v.x, v.y, 16);
        }

        for (int i = 0; i < total; i ++)
        {
            var a = getVector(i,total);
            var b = getVector(i*factor,total);
            P5JSExtension.line(a.x, a.y, b.x, b.y);
        }

    }
}