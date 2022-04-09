using UnityEngine;

public class Phyllotaxis : MonoBehaviour
{
    int n = 0;
    int c = 4;

    void Start()
    {
        P5JSExtension.colorMode(P5JSExtension.HSB);
        P5JSExtension.background(0);
    }
    void OnGUI()
    {
        var a = n * 137.5f;
        var r = c * Mathf.Sqrt(n);
        var x = r * Mathf.Cos(a) + P5JSExtension.width / 2;
        var y = r * Mathf.Sin(a) + P5JSExtension.height / 2;
        P5JSExtension.fill((a-r)%255,255,255);
        P5JSExtension.ellipse(x, y, 4, 4);
        n++;
    }
}