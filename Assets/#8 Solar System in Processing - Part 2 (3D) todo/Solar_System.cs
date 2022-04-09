using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solar_System__Part_2_3D : MonoBehaviour
{
    public class Planet
    {
        float radius;
        float distance;
        Planet[] planets;
        float angle;
        float orbitspeed;

        public Planet(float r, float d, float o)
        {
            radius = r;
            distance = d;
            angle = P5JSExtension.random(2 * Mathf.PI);
            orbitspeed = o;
            //println(angle);
        }

        public void orbit()
        {
            angle = angle + orbitspeed;
            if (planets != null)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planets[i].orbit();
                }
            }
        }
        public void spawnMoons(int total, int level)
        {
            planets = new Planet[total];
            for (int i = 0; i < planets.Length; i++)
            {
                float r = radius / (level * 2);
                float d = P5JSExtension.random(50f, 150f);
                float o = P5JSExtension.random(-0.1f, 0.1f);
                planets[i] = new Planet(r, d / level, o);
                if (level < 3)
                {
                    int num = (int)(P5JSExtension.random(0f, 4f));
                    planets[i].spawnMoons(num, level + 1);
                }
            }
        }

        public void show()
        {
            P5JSExtension.push();
            P5JSExtension.fill(255, 100);
            P5JSExtension.rotate(angle);
            P5JSExtension.translate(distance, 0);
            P5JSExtension.ellipse(0, 0, radius * 2, radius * 2);
            if (planets != null)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planets[i].show();
                }
            }
            P5JSExtension.pop();
        }
    }
    Planet sun;

    void Start()
    {
        //600x600
        sun = new Planet(50, 0, 0);
        sun.spawnMoons(5, 1);
    }

    void OnGUI()
    {
        P5JSExtension.background(0);
        P5JSExtension.resetMatrix();
        P5JSExtension.translate(P5JSExtension.width / 2, P5JSExtension.height / 2);
        sun.show();
        sun.orbit();
    }
}
