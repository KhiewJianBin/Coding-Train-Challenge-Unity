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
        float d = Vector2.Distance(new Vector2(x, y), new Vector2(pos.x, pos.y));
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
            var d = Vector2.Distance(new Vector2(x,y),new Vector2(pos.x, pos.y));
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

        x = Mathf.Clamp(x, 0, TheSnakeGame.width - TheSnakeGame.scl);
        y = Mathf.Clamp(y, 0, TheSnakeGame.height - TheSnakeGame.scl);
    }
    public void Show(Texture2D texture)
    {
        for (int i = 0; i < tail.Count; i++)
        {
            Rect pos = new Rect(tail[i].x, tail[i].y, TheSnakeGame.scl, TheSnakeGame.scl);
            GUI.DrawTexture(pos, texture);
        }

        Rect position = new Rect(x, y, TheSnakeGame.scl, TheSnakeGame.scl);
        GUI.DrawTexture(position, texture);
    }
    
}