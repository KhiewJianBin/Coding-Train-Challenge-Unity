using UnityEngine;

public class PurpleRain : MonoBehaviour
{
    public class Drop
    {
        float x = P5JSExtension.random(0, P5JSExtension.width);
        float y = P5JSExtension.random(-500, -50);
        float z = P5JSExtension.random(0, 20);
        float len;
        float yspeed;

        public Drop()
        {
            len = P5JSExtension.map(z, 0, 20, 10, 20);
            yspeed = P5JSExtension.map(z, 0, 20, 1, 20);
        }
        public void fall()
        {
            y = y + yspeed;
            float grav = P5JSExtension.map(z, 0, 20, 0, 0.2f);
            yspeed = yspeed + 0.2f + grav;

            if (y > P5JSExtension.height)
            {
                y = P5JSExtension.random(-200, -100);
                yspeed = P5JSExtension.random(4, 10);
            }
        }

        public void show()
        {
            float thick = P5JSExtension.map(z, 0, 20, 1, 3);
            P5JSExtension.strokeWeight(thick);
            P5JSExtension.stroke(138, 43, 226);
            P5JSExtension.line(x, y, x, y + len);
        }
    }

    Drop[] drops = new Drop[500];
    void Start()
    {
        for (int i = 0; i < drops.Length; i++)
        {
            drops[i] = new Drop();
        }
    }

    void OnGUI()
    {
        P5JSExtension.background(230, 230, 250);
        for (int i = 0; i < drops.Length; i++)
        {
            drops[i].fall();
            drops[i].show();
        }
    }
}
