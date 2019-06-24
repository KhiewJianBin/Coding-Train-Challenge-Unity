using UnityEngine;

//source
//https://wiki.unity3d.com/index.php/DrawLine
//slightly modified to prevent scaling of a line with 0 size (line/point)

public class Starfield : MonoBehaviour
{
    Star[] stars = new Star[800];

    public static float speed;
    
    void Start()
    {
        for (int i = 0; i < 800; i++)
        {
            stars[i] = new Star();
        }
    }
    void OnGUI()
    {
        speed = P5JSExtension.map(Input.mousePosition.x, 0, P5JSExtension.width, 0, 20);

        P5JSExtension.background(0);

        for (int i = 0; i < 800; i++)
        {
            stars[i].Update();
            stars[i].Show();
        }
    }
}

public class Star
{
    float x;
    float y;
    float z;

    float pz;

    public Star()
    {
        P5JSExtension.background(0);

        x = P5JSExtension.random(-P5JSExtension.width, P5JSExtension.width);
        y = P5JSExtension.random(-P5JSExtension.height, P5JSExtension.height);
        z = P5JSExtension.random(0, P5JSExtension.width);
        pz = z;
    }
    public void Update()
    {
        z = z - Starfield.speed;
        if (z < 1)
        {
            z = P5JSExtension.width;
            x = P5JSExtension.random(-P5JSExtension.width, P5JSExtension.width);
            y = P5JSExtension.random(-P5JSExtension.height, P5JSExtension.height);
            pz = z;
        }
    }
    public void Show()
    {
        float sx = P5JSExtension.map(x / z, 0, 1, 0, P5JSExtension.width);
        float sy = P5JSExtension.map(y / z, 0, 1, 0, P5JSExtension.height);
        float r = P5JSExtension.map(z, 0, P5JSExtension.width, 16, 0);

        //translate canvas
        sx += P5JSExtension.width / 2;
        sy += P5JSExtension.height / 2;
        P5JSExtension.ellipse(sx, sy , r, r);

        float px = P5JSExtension.map(x / pz, 0, 1, 0, P5JSExtension.width);
        float py = P5JSExtension.map(y / pz, 0, 1, 0, P5JSExtension.height);

        pz = z;

        //translate canvas
        px += P5JSExtension.width / 2;
        py += P5JSExtension.height / 2;
        P5JSExtension.line(px, py, sx, sy);
    }
}