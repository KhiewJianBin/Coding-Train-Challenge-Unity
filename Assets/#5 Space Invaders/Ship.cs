using UnityEngine;

public class Ship
{
    public float x = P5JSExtension.width / 2;
    public int xdir = 0;
    
    public void show()
    {
        P5JSExtension.fill(255);
        //rect mode enter hard code
        P5JSExtension.rect(x - 20 / 2, P5JSExtension.height - 20 - 60 / 2, 20, 60);
    }
    public void setDir(int dir)
    {
        xdir = dir;
    }

    public void move()
    {
        x += xdir * 5;
    }
}
