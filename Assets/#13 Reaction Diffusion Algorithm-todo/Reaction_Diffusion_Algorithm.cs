using System;
using UnityEngine;

//todo use render texture.setpixel for this
public class Reaction_Diffusion_Algorithm : MonoBehaviour
{
    chemical[][] grid;
    chemical[][] next;

    float dA = 1;
    float dB = 0.5f;
    float feed = 0.055f;
    float k = 0.062f;

    struct chemical
    {
        public float a;   
        public float b;
    }

    Texture2D texture;
    void Start()
    {
        texture = new Texture2D(200, 200);
        grid = new chemical[P5JSExtension.width][];
        next = new chemical[P5JSExtension.width][];
        for (int x = 0; x < P5JSExtension.width; x++)
        {
            grid[x] = new chemical[P5JSExtension.height];
            next[x] = new chemical[P5JSExtension.height];
            for (var y = 0; y < P5JSExtension.height; y++)
            {
                chemical c;
                c.a = 1; c.b = 0;
                grid[x][y] = c;

                c.a = 1; c.b = 0;
                next[x][y] = c;
            }
        }

        for (int i = 100;i<110;i++)
        {
            for (int j = 100; j < 110; j++)
            {
                grid[i][j].b = 1;
            }
        }
    }
    void Update()
    {
        for (int x = 1; x < P5JSExtension.width - 1; x++)
        {
            for (var y = 1; y < P5JSExtension.height - 1; y++)
            {
                var a = grid[x][y].a;
                var b = grid[x][y].b;
                next[x][y].a = a +
                             (dA * laplaceA(x, y)) -
                             (a * b * b) +
                             (feed * (1 - a));
                next[x][y].b = b +
                             (dB * laplaceB(x, y)) +
                             (a * b * b) -
                             ((k + feed) * b);

                next[x][y].a = P5JSExtension.constrain(next[x][y].a, 0, 1);
                next[x][y].b = P5JSExtension.constrain(next[x][y].b, 0, 1);
            }
        }

        for (int x = 0; x < P5JSExtension.width; x++)
        {
            for (var y = 0; y < P5JSExtension.height; y++)
            {
                var a = next[x][y].a;
                var b = next[x][y].b;
                var c = Mathf.Floor((a - b) * 255);
                c = P5JSExtension.constrain(c, 0, 255);
                texture.SetPixel(x, y, new Color32((byte)c, (byte)c, (byte)c, 255));
            }
        }
        texture.Apply();
        swap();
    }
    void OnGUI()
    {
        if (Event.current.type.Equals(EventType.Repaint))
        {
            Graphics.DrawTexture(new Rect(0, 0, texture.width, texture.height), texture);
        }
    }

    void swap()
    {
        var temp = grid;
        grid = next;
        next = temp;
    }

    float laplaceA(int x,int y)
    {
        float sumA = 0;
        sumA += grid[x][y].a * -1;
        sumA += grid[x-1][y].a * 0.2f;
        sumA += grid[x+1][y].a * 0.2f;
        sumA += grid[x][y+1].a * 0.2f;
        sumA += grid[x][y-1].a * 0.2f;
        sumA += grid[x-1][y-1].a * 0.05f;
        sumA += grid[x+1][y-1].a * 0.05f;
        sumA += grid[x+1][y+1].a * 0.05f;
        sumA += grid[x-1][y+1].a * 0.05f;
        return sumA;
    }
    float laplaceB(int x,int y)
    {
        float sumB = 0;
        sumB += grid[x][y].b * -1;
        sumB += grid[x - 1][y].b * 0.2f;
        sumB += grid[x + 1][y].b * 0.2f;
        sumB += grid[x][y + 1].b * 0.2f;
        sumB += grid[x][y - 1].b * 0.2f;
        sumB += grid[x - 1][y - 1].b * 0.05f;
        sumB += grid[x + 1][y - 1].b * 0.05f;
        sumB += grid[x + 1][y + 1].b * 0.05f;
        sumB += grid[x - 1][y + 1].b * 0.05f;
        return sumB;
    }
}
