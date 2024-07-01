using System.Collections.Generic;
using UnityEngine;
using static P5JSExtension;


public class Solar_System : P5JSBehaviour
{
    Planet sun;

    protected override void setup()
    {
        createCanvas(600, 600);
        sun = new Planet(50, 0, 0, random(TWO_PI));
        sun.spawnMoons(5, 1);
    }

    protected override void Update()
    {
        base.Update();

        sun.orbit();
    }

    protected override void draw()
    {
        background(51);
        translate(width / 2, height / 2);
        sun.show();
    }

    public class Planet
    {
        float radius;
        float distance;
        float orbitspeed;
        float angle;
        List<Planet> planets;

        public Planet(float radius, float distance, float orbitspeed, float angle)
        {
            this.radius = radius;
            this.distance = distance;
            this.orbitspeed = orbitspeed;
            this.angle = angle;
            this.planets = new();
        }

        public void orbit()
        {
            this.angle += this.orbitspeed;
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].orbit();
            }
        }
        public void spawnMoons(int total, int level)
        {
            for (int i = 0; i < total; i++)
            {
                var r = this.radius / (level * 2);
                var d = random(50f, 150f);
                var o = random(-0.1f, 0.1f);
                var a = random(TWO_PI);
                this.planets.push(new Planet(r, d / level, o, a));
                if (level < 3)
                {
                    var num = Mathf.FloorToInt(random(0f, 4f));
                    planets[i].spawnMoons(num, level + 1);
                }
            }
        }

        public void show()
        {
            push();
            fill(255, 100);
            rotate(this.angle);
            translate(this.distance, 0);
            ellipse(0, 0, this.radius * 2);
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].show();
            }
            pop();
        }
    }
}