using UnityEngine;
using UnityEngine.UI;

public class Supershapes2D : MonoBehaviour
{
    public Slider slider;

    float n1 = 0.3f;
    float n2 = 0.3f;
    float n3 = 0.3f;
    float m = 5;
    float a = 1;
    float b = 1;

    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 10;
    }

    float supershape(float theta)
    {
        var part1 = (1 / a) * Mathf.Cos(theta * m / 4);
        part1 = Mathf.Abs(part1);
        part1 = Mathf.Pow(part1,n2);

        var part2 = (1 / b) * Mathf.Sin(theta * m / 4);
        part2 = Mathf.Abs(part2);
        part2 = Mathf.Pow(part2, n3);

        var part3 = Mathf.Pow(part1 + part2,1/n1);
        if(part3 == 0)
        {
            return 0;
        }

        return (1/part3);
    }

    void OnGUI()
    {
        m = slider.value;
        P5JSExtension.background(51);
        var radius = 100;

        var total = 100;
        var increment = (2 * Mathf.PI) / total;

        P5JSExtension.resetShape();
        P5JSExtension.beginShape(MeshTopology.LineStrip);
        for (float angle = 0; angle < 2*Mathf.PI; angle += increment)
        {
            var r = supershape(angle);
            var x = radius * r * Mathf.Cos(angle);
            var y = radius * r * Mathf.Sin(angle);
            P5JSExtension.vertex(x, y);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape(P5JSExtension.CLOSED);
    }
}