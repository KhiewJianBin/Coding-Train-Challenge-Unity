using UnityEngine;
using UnityEngine.UI;

public class Fractal_Trees_Recursive : MonoBehaviour
{
    float angle = 0;
    public Slider slider;

    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 2 * Mathf.PI;
    }

    void OnGUI()
    {
        P5JSExtension.resetMatrix();
        P5JSExtension.background(51);
        angle = slider.value;
        P5JSExtension.stroke(255);
        P5JSExtension.translate(P5JSExtension.width/2, P5JSExtension.height);
        branch(100);
    }

    void branch(float len)
    {
        P5JSExtension.line(0, 0, 0, - len);
        P5JSExtension.translate(0, -len);
        if( len > 4)
        {
            P5JSExtension.push();
            P5JSExtension.rotate(angle);
            branch(len * 0.67f);
            P5JSExtension.pop();
            P5JSExtension.push();
            P5JSExtension.rotate(-angle);
            branch(len * 0.67f);
            P5JSExtension.pop();
        }
    }
}
