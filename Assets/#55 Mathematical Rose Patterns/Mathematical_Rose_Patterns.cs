using UnityEngine;
using UnityEngine.UI;

public class Mathematical_Rose_Patterns : MonoBehaviour
{
    float d = 8f;
    float n = 5f;
    public Slider sliderd;
    public Slider slidern;

    void Start()
    {
        sliderd.minValue = 1;
        sliderd.maxValue = 10;
        slidern.minValue = 1;
        slidern.maxValue = 10;

        P5JSExtension.translate(P5JSExtension.width / 2, P5JSExtension.height / 2);
    }
    void OnGUI()
    {
        d = sliderd.value;
        n = slidern.value;
        var k = n / d;
        P5JSExtension.resetShape();
        P5JSExtension.beginShape(MeshTopology.LineStrip);
        for (float a = 0; a < Mathf.PI * 2 * d; a += 0.02f)
        {
            var r = 200 * Mathf.Cos(k*a);
            var x = r * Mathf.Cos(a);
            var y = r * Mathf.Sin(a);
            P5JSExtension.vertex(x, y);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();
    }
}