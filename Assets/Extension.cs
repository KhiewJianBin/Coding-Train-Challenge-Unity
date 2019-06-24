using UnityEngine;

//extension methods to make it look like using PSJS function in unity
public class P5JSExtension : MonoBehaviour
{
    public static int width
    {
        get
        {
            return Screen.width;
        }
    }
    public static int height
    {
        get
        {
            return Screen.height;
        }
    }
    public static float map
        (float value,
        float start1,float stop1,
        float start2,float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }
    public static int random(int min,int max)
    {
        return Random.Range(min, max);
    }
    public static int floor(float value)
    {
        return Mathf.FloorToInt(value);
    }
    public static float constrain(float value,float min,float max)
    {
        return Mathf.Clamp(value, min, max);
    }
    public static float dist(float x1, float y1,float x2, float y2)
    {
        return Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2));
    }
    public static void background (byte rgb)
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color32(rgb, rgb, rgb, 255);
    }
    public static void background(byte r, byte g, byte b)
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color32(r, g, b, 255);
    }
    public static void frameRate(int rate)
    {
        Application.targetFrameRate = rate;
    }

    //shapes
    static Texture2D texture = new Texture2D(4,4);
    static Color32 textureColor = new Color32(255,255,255,255);
    static Color32 strokecolor = new Color32(255, 255, 255, 255);
    static float strokethickness = 1;
    public static void strokeWeight(float value)
    {
        strokethickness = value;
    }
    public static void stroke(byte r, byte g, byte b, byte a)
    {
        strokecolor = new Color32(r, g, b, 255);
    }
    public static void stroke(byte r, byte g, byte b)
    {
        stroke(r, g, b, 255);
    }
    public static void stroke(byte rgb)
    {
        stroke(rgb, rgb, rgb, 255);
    }
    public static void noStroke()
    {
        strokethickness = 0;
    }
    public static void fill(byte r, byte g, byte b,byte a)
    {
        textureColor = new Color32(r, g, b, a);
    }
    public static void fill(byte r,byte g,byte b)
    {
        fill(r, g, b, 255);
    }
    public static void fill(byte rgb)
    {
        fill(rgb, rgb, rgb, 255);
    }
    public static void ellipse(float px,float py,float sx,float sy)
    {
        Rect position = new Rect(px - sx / 2, py - sy / 2 , sx, sy);
        GUI.DrawTexture(position, texture, ScaleMode.ScaleToFit, false, 0, textureColor, 0, sx);
    }
    public static void rect(float px, float py, float sx, float sy)
    {
        Rect position = new Rect(px, py, sx, sy);
        GUI.DrawTexture(position, texture, ScaleMode.ScaleToFit, false, 0, textureColor, 0, 0);
    }
    public static void line(float p1x, float p1y, float p2x, float p2y)
    {
        Vector2 pos1 = new Vector2(p1x, p1y);
        Vector2 pos2 = new Vector2(p2x, p2y);
        Drawing.DrawLine(pos1, pos2, strokecolor,strokethickness);
    }
}