using System.Collections.Generic;
using UnityEngine;

public class Double_Pendulum : MonoBehaviour
{
    float r1 = 125;
    float r2 = 125;
    float m1 = 10;
    float m2 = 10;
    float a1 = 0;
    float a2 = 0;
    float a1_v = 0;
    float a2_v = 0;
    float g = 1;

    float px2 = -1;
    float py2 = -1;
    float cx, cy;

    List<Vector2> lines = new List<Vector2>();
    List<Vector2> lines2 = new List<Vector2>();

    void Start()
    {
        //500x300
        a1 = Mathf.PI / 2;
        a2 = Mathf.PI / 2;
        cx = P5JSExtension.width / 2;
        cy = 50;

        P5JSExtension.background(175);
    }
    void OnGUI()
    {
        P5JSExtension.frameRate(30);

        var num1 = -g * (2 * m1 + m2) * Mathf.Sin(a1);
        var num2 = -m2 * g * Mathf.Sin(a1 - 2 * a2);
        var num3 = -2 * Mathf.Sin(a1 - a2) * m2;
        var num4 = a2_v * a2_v * r2 + a1_v * a1_v * r1 * Mathf.Cos(a1 - a2);
        var den = r1 * (2 * m1 + m2 - m2 * Mathf.Cos(2 * a1 - 2 * a2));
        var a1_a = (num1 + num2 + num3 * num4) / den;

        num1 = 2 * Mathf.Sin(a1 - a2);
        num2 = (a1_v * a1_v * r1 * (m1 + m2));
        num3 = g * (m1 + m2) * Mathf.Cos(a1);
        num4 = a2_v * a2_v * r2 * m2 * Mathf.Cos(a1 - a2);
        den = r2 * (2 * m1 + m2 - m2 * Mathf.Cos(2 * a1 - 2 * a2));
        var a2_a = (num1 * (num2 + num3 + num4)) / den;

        P5JSExtension.resetMatrix();
        P5JSExtension.translate(cx, cy);
        P5JSExtension.stroke(0);
        P5JSExtension.strokeWeight(2);

        var x1 = r1 * Mathf.Sin(a1);
        var y1 = r1 * Mathf.Cos(a1);

        var x2 = x1 + r2 * Mathf.Sin(a2);
        var y2 = y1 + r2 * Mathf.Cos(a2);

        P5JSExtension.line(0, 0, x1, y1);
        P5JSExtension.fill(0);
        P5JSExtension.ellipse(x1, y1, m1, m1);

        P5JSExtension.line(x1, y1, x2, y2);
        P5JSExtension.fill(0);
        P5JSExtension.ellipse(x2, y2, m2, m2);

        a1_v += a1_a;
        a2_v += a2_a;
        a1 += a1_v;
        a2 += a2_v;

        // a1_v *= 0.99;
        // a2_v *= 0.99;

        //note quite the same, but same effect
        lines.Add(new Vector2(px2, py2));
        lines2.Add(new Vector2(x2, y2));
        for (int i = 0; i < lines.Count;i++)
        {
            P5JSExtension.line(lines[i].x, lines[i].y, lines2[i].x, lines2[i].y);
        }

        px2 = x2;
        py2 = y2;
    }
}