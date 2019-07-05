using UnityEngine;
using UnityEngine.UI;

// cant follow exactly
public class Mandelbrot_Set : MonoBehaviour
{
    public Slider minSlider;
    public Slider maxSlider;

    Texture2D texture;
    void Start()
    {
        texture = new Texture2D(200, 200);

        minSlider.minValue = -2.5f;
        minSlider.maxValue = 0;

        maxSlider.minValue = 0;
        maxSlider.maxValue = 2.5f;
    }
    void OnGUI()
    {
        int maxiterations = 100;
        for (var x = 0; x < texture.width; x++)
        {
            for (var y = 0; y < texture.height; y++)
            {
                var a = P5JSExtension.map(x, 0, P5JSExtension.width, minSlider.value, maxSlider.value);
                var b = P5JSExtension.map(y, 0, P5JSExtension.height, minSlider.value, maxSlider.value);

                var ca = a;
                var cb = b;
                var n = 0;

                while (n < maxiterations)
                {
                    var aa = a * a - b * b;
                    var bb = 2 * a * b;
                    a = aa + ca;
                    b = bb + cb;
                    if (a * a + b * b > 16)
                    {
                        break;
                    }
                    n++;
                }

                var bright = P5JSExtension.map(n, 0, maxiterations, 0, 1);
                bright = P5JSExtension.map(Mathf.Sqrt(bright), 0, 1, 0, 255);

                if (n == maxiterations)
                {
                    bright = 0;
                }

                texture.SetPixel(x, y, new Color(bright / 255f, bright / 255f, bright / 255f));
            }
        }
        texture.Apply();

        if (Event.current.type.Equals(EventType.Repaint))
        {
            Graphics.DrawTexture(new Rect(0, 0, texture.width, texture.height), texture);
        }
    }
}