using UnityEngine;

//source
//https://wiki.unity3d.com/index.php/DrawLine
//slightly modified to prevent scaling of a line with 0 size (line/point)

public class Starfield : MonoBehaviour
{
    //-------------------------------------------------
    public static int width;
    public static int height;
    //-------------------------------------------------

    public Texture2D starTexture;
    Star[] stars = new Star[800];

    public static float speed;

    void Awake()
    {
        width = Screen.width;
        height = Screen.height;
    }
    void Start()
    {
        for (int i = 0; i < 800; i++)
        {
            stars[i] = new Star();
        }
    }
    void OnGUI()
    {
        speed = Extension.Map(Input.mousePosition.x, 0, width, 0, 20);

        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color(0,0,0,0);
        for (int i = 0; i < 800; i++)
        {
            stars[i].Update();
            stars[i].Show(starTexture);
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
        x = Random.Range(-Starfield.width, Starfield.width);
        y = Random.Range(-Starfield.height, Starfield.height);
        z = Random.Range(0, Starfield.width);
        pz = z;
    }
    public void Update()
    {
        z = z - Starfield.speed;
        if (z < 1)
        {
            z = Screen.width;
            x = Random.Range(-Starfield.width, Starfield.width);
            y = Random.Range(-Starfield.height, Starfield.height);
            pz = z;
        }
    }
    public void Show(Texture2D texture)
    {
        float sx = Extension.Map(x / z, 0, 1, 0, Starfield.width);
        float sy = Extension.Map(y / z, 0, 1, 0, Starfield.height);
        float r = Extension.Map(z, 0, Starfield.width, 16, 0);

        Rect position = new Rect(sx - r / 2 + Starfield.width / 2, sy - r / 2 + Starfield.height / 2, r, r);
        GUI.DrawTexture(position, texture);

        float px = Extension.Map(x / pz, 0, 1, 0, Starfield.width);
        float py = Extension.Map(y / pz, 0, 1, 0, Starfield.height);

        pz = z;

        Drawing.DrawLine(new Vector2(px + Starfield.width / 2, py + Starfield.height / 2), new Vector2(sx + Starfield.width / 2, sy + Starfield.height / 2));
    }
}