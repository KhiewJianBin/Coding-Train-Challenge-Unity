using UnityEngine;

public class Drop
{
    float x = Random.Range(0,PurpleRain.width);
    float y = Random.Range(-500, -50);
    float z = Random.Range(0, 20);
    float len;
    float yspeed;

    public Drop()
    {
        len = Extension.Map(z, 0, 20, 10, 20);
        yspeed = Extension.Map(z, 0, 20, 1, 20);
    }
    public void fall()
    {
        y = y + yspeed;
        float grav = Extension.Map(z, 0, 20, 0, 0.2f);
        yspeed = yspeed + 0.2f + grav;

        if (y > PurpleRain.height)
        {
            y = Random.Range(-200, -100);
            yspeed = Random.Range(4, 10);
        }
    }

    public void show()
    {
        float thick = Extension.Map(z, 0, 20, 1, 3);
        Drawing.DrawLine(new Vector2(x,y),new Vector2(x,y+10),new Color32(138, 43, 226,255), thick);
    }
}
