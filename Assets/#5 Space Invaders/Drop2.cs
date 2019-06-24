using UnityEngine;

public class Drop2
{
    public float x;
    public float y;
    public float r;
    public bool toDelete;

    public Drop2(float inx, float iny)
    {
        x = inx;
        y = iny;
        r = 8;
        toDelete = false;
    }

    public void show()
    {
        P5JSExtension.fill(150, 0, 255, 255);
        P5JSExtension.ellipse(x, y, r * 2, r * 2);
    }

    public void evaporate ()
    {
        toDelete = true;
    }

    public bool hits(Flower flower)
    {
        
        var d = P5JSExtension.dist(x,y,flower.x, flower.y);
        if (d < r + flower.r)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void move ()
    {
        y = y - 5;
    }
}
