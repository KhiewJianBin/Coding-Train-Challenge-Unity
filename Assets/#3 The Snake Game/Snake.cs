using System.Collections.Generic;
using UnityEngine;

public class Snake
{
    float x;
    float y;
    float xspeed;
    float yspeed;
    public int total;
    
    List<Vector2> tail;

    public Snake()
    {
        x = 0;
        y = 0;
        xspeed = 1;
        yspeed = 0;
        total = 0;
        tail = new List<Vector2>();
    }
    public bool eat(Vector2 pos)
    {
        float d = P5JSExtension.dist(x, y, pos.x, pos.y);
        if (d < 1)
        {
            total++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void dir(float x, float y)
    {
        xspeed = x;
        yspeed = y;
    }

    public void death()
    {
        for (int i = 0; i < tail.Count; i++)
        {
            Vector2 pos = tail[i];
            var d = P5JSExtension.dist(x, y, pos.x, pos.y);
            if (d < 1)
            {
                Debug.Log("starting over");
                total = 0;
                tail.Clear();
            }
        }
    }
    public void Update()
    {
        for (int i = 0;i < tail.Count - 1; i++)
        {
            tail[i] = tail[i+1];
        }
        if (tail.Count < total)
        {
            tail.Add(new Vector2(x, y));
        }
        else if (tail.Count >= 1)
        {
            tail[tail.Count - 1] = new Vector2(x, y);
        }
        Debug.Log(tail.Count);

        x = x + xspeed * TheSnakeGame.scl;
        y = y + yspeed * TheSnakeGame.scl;

        x = P5JSExtension.constrain(x, 0, P5JSExtension.width - TheSnakeGame.scl);
        y = P5JSExtension.constrain(y, 0, P5JSExtension.height - TheSnakeGame.scl);
    }
    public void Show()
    {
        P5JSExtension.fill(255);
        for (int i = 0; i < tail.Count; i++)
        {
            P5JSExtension.rect(tail[i].x, tail[i].y, TheSnakeGame.scl, TheSnakeGame.scl);
        }

        P5JSExtension.rect(x, y, TheSnakeGame.scl, TheSnakeGame.scl);
    }
}