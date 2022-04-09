using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fourier_Series : MonoBehaviour
{
    float time = 0f;
    List<float> wave = new List<float>();
    public Slider slider;

    void OnGUI()
    {
        slider.minValue = 1;
        slider.maxValue = 100;

        P5JSExtension.resetMatrix();
        P5JSExtension.translate(150,200);

        var x = 0f;
        var y = 0f;

        for(int i = 0; i < slider.value; i++)
        {
            var prevx = x;
            var prevy = y;
            var n = i * 2 + 1;
            var radius = 75 * (4 / (n * Mathf.PI));
            x += radius * Mathf.Cos(n * time);
            y += radius * Mathf.Sin(n * time);

            P5JSExtension.stroke(255,100);
            P5JSExtension.noFill();
            P5JSExtension.ellipse(prevx, prevy, radius * 2);

            //P5JSExtension.fill(255);
            P5JSExtension.stroke(255);
            P5JSExtension.line(prevx, prevy, x, y);
            //P5JSExtension.ellipse(x, y, 8);
        }
        wave.Add(y);

        P5JSExtension.translate(200, 0);
        P5JSExtension.line(x - 200, y, 0, wave[wave.Count - 1]);
        P5JSExtension.resetShape();
        P5JSExtension.beginShape(MeshTopology.LineStrip);
        for(int i = 0; i < wave.Count;i++)
        {
            P5JSExtension.vertex(i/2f,  wave[wave.Count - 1-i] * -1);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();

        time += 0.01f;

        if(wave.Count > 500)
        {
            wave.RemoveAt(0);
        }
    }
}