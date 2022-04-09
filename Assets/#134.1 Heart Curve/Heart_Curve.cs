using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart_Curve : MonoBehaviour
{
    List<Vector2> heart = new List<Vector2>();
    float a = 0;

    void OnGUI()
    {
        P5JSExtension.translate(P5JSExtension.width / 2, P5JSExtension.height / 2);

        P5JSExtension.stroke(255);
        P5JSExtension.strokeWeight(2);
        P5JSExtension.fill(150, 0, 100);
        P5JSExtension.resetShape();
        P5JSExtension.beginShape(MeshTopology.LineStrip);
        foreach (Vector2 v in heart)
        {
            P5JSExtension.vertex(v.x, P5JSExtension.height - v.y);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();

        float r = P5JSExtension.height / 40;
        float x = r * 16 * Mathf.Pow(Mathf.Sin(a), 3);
        float y = -r * (13 * Mathf.Cos(a) - 5 * Mathf.Cos(2 * a) - 2 * Mathf.Cos(3 * a) - Mathf.Cos(4 * a));
        heart.Add(new Vector2(x, y));

        // So that it stops
        if (a > 2*Mathf.PI)
        {
            //noLoop();
        }

        a += 0.01f;
    }
}