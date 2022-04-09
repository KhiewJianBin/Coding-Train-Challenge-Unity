using UnityEngine;
using UnityEngine.UI;

public class Superellipse : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 10;
    }

    void OnGUI()
    {
        var a = 100;
        var b = 100;
        var n = slider.value;

        P5JSExtension.resetShape();
        P5JSExtension.beginShape(MeshTopology.LineStrip);
        for (float angle = 0; angle < 2*Mathf.PI; angle += 0.1f)
        {
            var na = 2 / n;
            var x = Mathf.Pow(Mathf.Abs(Mathf.Cos(angle)), na) * a * sgn(Mathf.Cos(angle));
            var y = Mathf.Pow(Mathf.Abs(Mathf.Sin(angle)), na) * b * sgn(Mathf.Sin(angle));
            P5JSExtension.vertex(x, y);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape(P5JSExtension.CLOSED);
    }

    float sgn(float val)
    {
        if (val == 0)
        {
            return 0;
        }
        return val / Mathf.Abs(val);
    }
}