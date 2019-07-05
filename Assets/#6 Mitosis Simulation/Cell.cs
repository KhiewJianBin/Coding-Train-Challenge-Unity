using UnityEngine;

public class Cell
{
    Vector2 pos;
    float r;
    Color32 c;

    public Cell(Vector2? pos, float r = 60,Color32 c = new Color32())
    {
        if (pos.HasValue)
        {
            this.pos = pos.Value;
        }
        else
        {
            this.pos = new Vector2(P5JSExtension.random(P5JSExtension.width), P5JSExtension.random(P5JSExtension.height));
        }
        this.r = r;
        if (c.r == 0 && c.g == 0 && c.b == 0 && c.a == 0)
        {
            this.c = new Color32((byte)P5JSExtension.random(100, 255), 0, (byte)P5JSExtension.random(100, 255), 100);
        }
        else
        {
            this.c = c;
        }
    }
    public bool clicked(float x, float y)
    {
        var d = P5JSExtension.dist(pos.x, pos.y, x, y);

        if (d < r)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Cell mitosis()
    {
        //this.pos.x += random(-this.r, this.r);
        var cell = new Cell(pos, r * 0.8f, c);
        return cell;
    }

    public void move()
    {
        Vector2 vel = P5JSExtension.random2D();
        pos += vel;
    }
    public void show()
    {
        P5JSExtension.fill(c.r,c.g,c.b,c.a);
        P5JSExtension.ellipse(pos.x, pos.y, r, r);
    }
}