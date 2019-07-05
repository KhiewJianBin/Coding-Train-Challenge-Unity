using UnityEngine;
using UnityEngine.UI;

// cant follow exactly
public class Julia_Set : MonoBehaviour
{
    Texture2D texture;
    float angle = 0;
    void Start()
    {
        texture = new Texture2D(200, 200);
    }
    void OnGUI()
    {
        int maxiterations = 100;
        for (var x = 0; x < texture.width; x++)
        {
            for (var y = 0; y < texture.height; y++)
            {
                var a = P5JSExtension.map(x, 0, P5JSExtension.width, -2, 2);
                var b = P5JSExtension.map(y, 0, P5JSExtension.height, -2, 2);

                //var ca = P5JSExtension.map(Input.mousePosition.x, 0, P5JSExtension.width,-1,1);
                //var cb = P5JSExtension.map(Input.mousePosition.y, 0, P5JSExtension.height, -1, 1);
                float ca = Mathf.Cos(angle * 0.03213f * Mathf.Deg2Rad);
                float cb = Mathf.Sin(angle * Mathf.Deg2Rad);
                angle += 0.0002f;

                var n = 0;

                while (n < maxiterations)
                {
                    var aa = a * a;
                    var bb = b * b;
                    if (a * a + b * b > 4)
                    {
                        break;
                    }
                    float twoab = 2 * a * b;
                    a = aa - bb + ca;
                    b = twoab + cb;
                    n++;
                }

                

                if (n == maxiterations)
                {
                    texture.SetPixel(x, y, Color.black);
                }
                else
                {
                    var hu = P5JSExtension.map(n, 0, maxiterations, 0, 1);
                    hu = P5JSExtension.map(Mathf.Sqrt(hu), 0, 1, 0, 255);
                    texture.SetPixel(x, y, Color.HSVToRGB(hu / 255f, 255 / 255f, 150 / 255f));
                }
            }
        }
        texture.Apply();

        if (Event.current.type.Equals(EventType.Repaint))
        {
            Graphics.DrawTexture(new Rect(0, 0, texture.width, texture.height), texture);
        }
    }
}