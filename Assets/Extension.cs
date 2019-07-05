using System.Collections.Generic;
using UnityEngine;
using static Fractal_Trees_Object_Oriented;

//extension methods to make it look like using PSJS function in unity
public static class P5JSExtension
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
    //Math functions
    public static float radians(float deg)
    {
        return deg * Mathf.Deg2Rad;
    }
    public static float map
        (float value,
        float start1, float stop1,
        float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }
    public static int random(int min, int max)
    {
        return Random.Range(min, max);
    }
    public static float random(float min, float max)
    {
        return Random.Range(min, max);
    }
    public static int random(int value)
    {
        if (value > 0)
            return random(0, value);
        else
            return random(value, 0);
    }
    public static float random(float value)
    {
        if (value > 0)
            return random(0, value);
        else
            return random(value, 0);
    }
    public static Vector2 random2D()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
    public static Vector3 random3D()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
    public static float noise(float x, float y)
    {
        return Mathf.PerlinNoise(x, y);
    }
    public static int floor(float value)
    {
        return Mathf.FloorToInt(value);
    }
    public static float constrain(float value, float min, float max)
    {
        return Mathf.Clamp(value, min, max);
    }
    public static float dist(float x1, float y1, float x2, float y2)
    {
        return Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2));
    }
    public static float dist(Vector2 v1, Vector2 v2)
    {
        return Vector2.Distance(v1, v2);
    }

    //Scene, canvas
    public static void background(byte rgb)
    {
        background(rgb, rgb, rgb);
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

    //generating shapes draw
    static Texture2D texture = new Texture2D(4, 4);
    static Color32 textureColor = new Color32(255, 255, 255, 255);
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
        //strokethickness = 0;
    }
    public static void fill(byte r, byte g, byte b, byte a)
    {
        textureColor = new Color32(r, g, b, a);
    }
    public static void fill(byte r, byte g, byte b)
    {
        fill(r, g, b, 255);
    }
    public static void fill(byte rgb)
    {
        fill(rgb, rgb, rgb, 255);
    }
    public static void ellipse(float px, float py, float sx, float sy)
    {
        Rect position = new Rect(px - sx / 2, py - sy / 2, sx, sy);
        GUI.DrawTexture(position, texture, ScaleMode.ScaleToFit, false, 0, textureColor, 0, sx);
    }
    public static void rect(float px, float py, float sx, float sy)
    {
        Rect position = new Rect(px, py, sx, sy);
        GUI.DrawTexture(position, texture, ScaleMode.ScaleToFit, false, 0, textureColor, 0, 0);
    }
    public static void line(float p1x, float p1y, float p2x, float p2y)
    {
        p1x += originx;
        p1y += originy;
        p2x += originx;
        p2y += originy;

        Vector2 pos1 = new Vector2(p1x, p1y);
        Vector2 pos2 = new Vector2(p2x, p2y);
        pos1 = rotateWithRespectToOrigin(pos1);
        pos2 = rotateWithRespectToOrigin(pos2);

        Drawing.DrawLine(pos1, pos2, strokecolor, strokethickness);
    }


    //3d draw

    static bool hasbeginShape = false;
    static Mesh mesh = new Mesh();
    static MeshTopology meshtopology;
    static List<Vector3> vertices = new List<Vector3>();
    static List<int> indices = new List<int>();

    public static void beginShape(MeshTopology meshTopology)
    {
        meshtopology = meshTopology;
        mesh.Clear();
        hasbeginShape = true;
    }
    public static void vertex(float x, float y, float z)
    {
        if (hasbeginShape)
        {
            vertices.Add(new Vector3(x, y, z));
            if (meshtopology == MeshTopology.LineStrip ||
                meshtopology == MeshTopology.Points ||
                meshtopology == MeshTopology.Lines)
            {
                indices.Add(vertices.Count - 1);
            }
            else //triangle strip hardcode
            {
                if(vertices.Count>2)
                {
                    if(vertices.Count%2 == 0)
                    {
                        indices.Add(vertices.Count - 1);
                        indices.Add(vertices.Count - 1 - 1);
                        indices.Add(vertices.Count - 1 - 2);
                    }
                    else
                    {
                        indices.Add(vertices.Count - 1 - 2);
                        indices.Add(vertices.Count - 1 - 1);
                        indices.Add(vertices.Count - 1);
                    }
                }
            }
        }
    }
    public static void vertex(float x, float y)
    {
        vertex(x, y, 0);
    }
    public static bool CLOSED = true;
    public static Mesh endShape(bool closed = false)
    {
        if (closed)
        {
            indices.Add(0);
        }
        mesh.SetVertices(vertices);
        mesh.SetIndices(indices, meshtopology, 0);
        hasbeginShape = false;

        vertices.Clear();
        indices.Clear();
        mesh.RecalculateNormals();
        return mesh;
    }











    static float originx = 0;
    static float originy = 0;
    static float rotation = 0;

    static Stack<float> savedoriginx = new Stack<float>();
    static Stack<float> savedoriginy = new Stack<float>();
    static Stack<float> savedrotation = new Stack<float>();

    public static void translate(float x, float y)
    {
        Vector2 neworigin = new Vector2(originx + x, originy + y);
        Vector2 rotatedvector = rotateWithRespectToOrigin(new Vector2(neworigin.x, neworigin.y));
        originx = rotatedvector.x;
        originy = rotatedvector.y;
    }
    public static void rotate(float radians)
    {
        rotation += radians;
    }
    public static Vector2 rotate(this Vector2 v, float radians)
    {
        return new Vector2(Mathf.Cos(radians) * v.x - Mathf.Sin(radians) * v.y, Mathf.Sin(radians) * v.x + Mathf.Cos(radians) * v.y);
    }
    public static void setMag(this Vector2 v, float value)
    {
        v.Normalize();
        v *= value;
    }
    public static void setMag(this Vector3 v, float value)
    {
        v.Normalize();
        v *= value;
    }
    public static PVector2 rotate(this PVector2 v, float radians)
    {
        return new PVector2(Mathf.Cos(radians) * v.x - Mathf.Sin(radians) * v.y, Mathf.Sin(radians) * v.x + Mathf.Cos(radians) * v.y);
    }

    static Vector2 rotateWithRespectToOrigin(Vector2 inv)
    {
        inv -= new Vector2(originx, originy);
        inv = new Vector2(Mathf.Cos(rotation) * inv.x - Mathf.Sin(rotation) * inv.y, Mathf.Sin(rotation) * inv.x + Mathf.Cos(rotation) * inv.y);
        return inv + new Vector2(originx, originy);
    }

    public static void push()
    {
        savedoriginx.Push(originx);
        savedoriginy.Push(originy);
        savedrotation.Push(rotation);
    }
    public static void pop()
    {
        originx = savedoriginx.Pop();
        originy = savedoriginy.Pop();
        rotation = savedrotation.Pop();
    }
    public static void resetMatrix()
    {
        originx = 0;
        originy = 0;
        rotation = 0;
    }
}