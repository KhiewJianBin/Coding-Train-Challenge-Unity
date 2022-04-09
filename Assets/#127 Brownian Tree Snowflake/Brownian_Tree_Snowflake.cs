using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brownian_Tree_Snowflake : MonoBehaviour
{
    
    class Particle
    {
        Vector2 pos;
        float r;
        public Particle(float x,float y)
        {
            pos = new Vector2(x,y);
            r = 3;
        }
        public void update()
        {
            pos.x -= 1;
            pos.y += P5JSExtension.random(-3f, 3f);

            float angle = pos.heading();
            angle = P5JSExtension.constrain(angle, 0, Mathf.PI / 6);
            float magnitude = pos.magnitude;
            pos = P5JSExtension.fromAngle(angle);
            pos = pos.setMag(magnitude);
        }
        public void show()
        {
            P5JSExtension.fill(255);
            P5JSExtension.stroke(255);
            P5JSExtension.ellipse(pos.x, pos.y, r * 2, r *2);
        }
        public bool finished()
        {
            return (pos.x < 1);
        }
        public bool intersects(List<Particle> snowflakes)
        {
            bool result = false;
            foreach (Particle s in snowflakes)
            {
                float d = P5JSExtension.dist(s.pos.x, s.pos.y, pos.x, pos.y);
                if(d<r*2)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }

    Particle current;
    List<Particle> snowflake;
    void Start()
    {
        current = new Particle(P5JSExtension.width / 2, 0);
        snowflake = new List<Particle>();
    }
    void OnGUI()
    {
        P5JSExtension.resetMatrix();
        P5JSExtension.translate(P5JSExtension.width / 2, P5JSExtension.height / 2);
        P5JSExtension.rotate(Mathf.PI / 6);
        P5JSExtension.background(0);

        int count = 0;
        while (!current.finished() && !current.intersects(snowflake))
        {
            current.update();
            count++;
        }

        snowflake.Add(current);
        current = new Particle(P5JSExtension.width / 2, 0);

        for (int i = 0;i<6; i++)
        {
            P5JSExtension.rotate(Mathf.PI / 3);
            current.show();
            foreach (Particle p in snowflake)
            {
                p.show();
            }

            //cant do scaling properly
            P5JSExtension.push();
            P5JSExtension.scale(1,-1);
            current.show();
            foreach (Particle p in snowflake)
            {
                p.show();
            }
            P5JSExtension.pop();
        }
    }
}