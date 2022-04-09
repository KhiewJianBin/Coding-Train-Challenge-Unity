using System.Collections.Generic;
using UnityEngine;

public class Pi_Approximation_with_Leibniz_Series : MonoBehaviour
{
    float pi = 4;
    int interations = 0;
    List<float> history = new List<float>();

    float minY = 2;
    float maxY = 4;

    void OnGUI()
    {
        float den = interations * 2 + 3;
        if (interations % 2 == 0)
        {
            pi -= 4 / den;
        }
        else
        {
            pi += 4 / den;
        }

        history.Add(pi);
        float piY = P5JSExtension.map(Mathf.PI, minY, maxY, P5JSExtension.height, 0);
        P5JSExtension.line(0, piY, P5JSExtension.width, piY);
        float spacing = P5JSExtension.width / (float)history.Count;
        P5JSExtension.stroke(255);
        P5JSExtension.noFill();
        P5JSExtension.resetShape();
        P5JSExtension.beginShape(MeshTopology.LineStrip);
        for(int i = 0; i < history.Count;i++)
        {
            var x = i * spacing;
            var y = P5JSExtension.map(history[i], minY, maxY, P5JSExtension.height,0);
            P5JSExtension.vertex(x, P5JSExtension.height - y);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();


        P5JSExtension.text(pi, P5JSExtension.width / 2, P5JSExtension.height / 2);
        interations++;
    }
}