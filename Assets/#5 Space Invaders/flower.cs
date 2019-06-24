using UnityEngine;

public class Flower
{
    public float x;
    public float y;
    public float r;
    public int xdir;
    
    public Flower(float inx,float iny)
    {
        x = inx;
        y = iny;
        r = 30;
        xdir = 1;
    }

    public void grow()
    {
        r = r + 2;
    }
    public void shiftDown()
    {
        xdir *= -1;
        y += r;
    }

    public void move()
    {
        x = x + xdir;
    }

    public void show()
    {
        P5JSExtension.fill(255, 0, 200, 150);
        P5JSExtension.ellipse(x, y, r * 2, r * 2);
    }
}
