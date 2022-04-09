using System.Collections.Generic;
using UnityEngine;

public class Sandpiles : MonoBehaviour
{
    Texture2D texture;
    int[][] sandpiles;
    void Start()
    {
        texture = new Texture2D(P5JSExtension.width, P5JSExtension.height);
        sandpiles = new int[P5JSExtension.width][];
        for(int i = 0; i < sandpiles.Length;i++)
        {
            sandpiles[i] = new int[P5JSExtension.height];
            for (int j = 0; j < sandpiles.Length; j++)
            {

            }
        }
        sandpiles[P5JSExtension.width / 2][P5JSExtension.height / 2] = 4;
    }
    void topple()
    {
        int[][] nextpiles = new int[P5JSExtension.width][];
        for (int x = 0; x < P5JSExtension.width; x++)
        {
            nextpiles[x] = new int[P5JSExtension.height];
            for (int y = 0; y < P5JSExtension.height; y++)
            {
                int num = sandpiles[x][y];
                if(num < 4)
                {
                    nextpiles[x][y] = sandpiles[x][y];
                }
            }
        }

        for (int x = 0; x < P5JSExtension.width; x++)
        {
            for (int y = 0; y < P5JSExtension.height; y++)
            {
                int num = sandpiles[x][y];
                if (num >= 4)
                {
                    nextpiles[x][y] = sandpiles[x][y] - 4;
                    nextpiles[x + 1][y]++;
                    nextpiles[x - 1][y]++;
                    nextpiles[x][y + 1]++;
                    nextpiles[x][y - 1]++;
                }
            }
        }
    }
    void render()
    {
        for (int x = 0; x < P5JSExtension.width; x++)
        {
            for (int y = 0; y < P5JSExtension.height; y++)
            {
                int num = sandpiles[x][y];
                Color col = new Color32(255, 255, 255, 0);
                if (num == 0)
                {
                    col = new Color32(0, 0, 0, 255);
                }
                else if (num == 1)
                {
                    col = new Color32(255, 0, 255, 255);
                }
                else if (num == 2)
                {
                    col = new Color32(0, 255, 255, 255);
                }
                else if (num == 3)
                {
                    col = new Color32(255, 255, 255, 255);
                }
                texture.SetPixel(x, y, col);
            }
        }
        texture.Apply();
    }
    void OnGUI()
    {
        P5JSExtension.frameRate(1);
        render();
        if (Event.current.type.Equals(EventType.Repaint))
        {
            Graphics.DrawTexture(new Rect(0, 0, texture.width, texture.height), texture);
        }
        topple();

    }
}