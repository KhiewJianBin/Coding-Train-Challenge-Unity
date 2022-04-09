using System.Collections.Generic;
using UnityEngine;

public class Langtons_Ant : MonoBehaviour
{
    int[][] grid;
    int x;
    int y;
    int dir;

    int ANTUP = 0;
    int ANTRIGHT = 1;
    int ANTDOWN = 2;
    int ANTLEFT = 3;

    Texture2D ant;
    void Start()
    {
        //400x400
        ant = new Texture2D(P5JSExtension.width, P5JSExtension.height);
        for (int i = 0; i < P5JSExtension.width; i++)
        {
            for (int j = 0; j < P5JSExtension.height; j++)
            {
                ant.SetPixel(i, j, Color.white);
            }
        }
        ant.Apply();

        grid = new int[P5JSExtension.width][];
        for(int i = 0;i< P5JSExtension.width; i++)
        {
            grid[i] = new int[P5JSExtension.height];
        }
        x = P5JSExtension.width/2;
        y = P5JSExtension.height/2;
        dir = ANTUP;
    }
    void turnRight()
    {
        dir++;
        if(dir > ANTLEFT)
        {
            dir = ANTUP;
        }
    }
    void turnLeft()
    {
        dir--;
        if (dir < ANTUP)
        {
            dir = ANTLEFT;
        }
    }
    void moveForward()
    {
        if(dir == ANTUP)
        {
            y--;
        }
        else if (dir == ANTRIGHT)
        {
            x++;
        }
        else if ( dir == ANTDOWN)
        {
            y++;
        }
        else if (dir == ANTLEFT)
        {
            x--;
        }
        if(x > P5JSExtension.width-1)
        {
            x = 0;
        }
        else if (x<0)
        {
            x = P5JSExtension.width - 1;
        }
        if (y > P5JSExtension.height-1)
        {
            y = 0;
        }
        else if (y < 0)
        {
            y = P5JSExtension.height - 1;
        }
    }
    void OnGUI()
    {
        P5JSExtension.background(255);

        for(int n = 0; n < 500;n++)
        {
            try
            {
                var a = grid[x][y];
            }
            catch
            {
                print(x);
                print(y);
            }
            int state = grid[x][y];
            if (state == 0)
            {
                turnRight();
                grid[x][y] = 1;
            }
            else if (state == 1)
            {
                turnLeft();
                grid[x][y] = 0;
            }

            Color col = Color.white;
            if(grid[x][y] == 1)
            {
                col = Color.black;
            }
            ant.SetPixel(x, y, col);
            moveForward();
        }
        ant.Apply();

        if (Event.current.type.Equals(EventType.Repaint))
        {
            Graphics.DrawTexture(new Rect(0, 0, ant.width, ant.height), ant);
        }
    }
}