using UnityEngine;
using UnityEngine.UI;

public class Butterfly_Generator : MonoBehaviour
{
    float yoff = 0;
    void OnGUI()
    {
        P5JSExtension.background(51);
        P5JSExtension.resetMatrix();
        P5JSExtension.translate(P5JSExtension.width / 2, P5JSExtension.height / 2);

        float r = 100;
        P5JSExtension.stroke(255);
        P5JSExtension.fill(255, 50);
        P5JSExtension.strokeWeight(1);

        float da = Mathf.PI / 300;
        float dx = 0.1f;

        float xoff = 0;

        P5JSExtension.resetShape();
        P5JSExtension.beginShape(MeshTopology.LineStrip);
        for (var a = -Mathf.PI / 2; a <= Mathf.PI / 2; a += da)
        {
            var n = P5JSExtension.noise(xoff,yoff);
            r = Mathf.Sin(2 * a) * P5JSExtension.map(n,0,1,50,100);
            var x = r * Mathf.Cos(a);
            var y = Mathf.Sin(yoff) * r * Mathf.Sin(a);
            xoff += dx;
            P5JSExtension.vertex(x, y);
        }
        for (var a = Mathf.PI / 2; a <= 3*Mathf.PI / 2; a += da)
        {
            var n = P5JSExtension.noise(xoff,yoff);
            r = Mathf.Sin(2 * a) * P5JSExtension.map(n, 0, 1, 50, 100);
            var x = r * Mathf.Cos(a);
            var y = Mathf.Sin(yoff) * r * Mathf.Sin(a);
            xoff -= dx;
            P5JSExtension.vertex(x, y);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();

        yoff += 0.01f;
    }
}