using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flocking_Simulation : MonoBehaviour
{
    public class Boid
    {
        Vector2 position;
        Vector2 velocity;
        Vector2 acceleration;
        float maxForce;
        float maxSpeed;

        public Boid()
        {
            position = new Vector2(P5JSExtension.random(P5JSExtension.width), P5JSExtension.random(P5JSExtension.height));
            velocity = P5JSExtension.random2D();
            velocity = velocity.setMag(P5JSExtension.random(2f, 4f));
            acceleration = new Vector2();
            maxForce = 0.2f;
            maxSpeed = 4;
        }

        public void edges()
        {
            if(position.x > P5JSExtension.width)
            {
                position.x = 0;
            }
            else if (position.x < 0)
            {
                position.x = P5JSExtension.width;
            }

            if (position.y > P5JSExtension.height)
            {
                position.y = 0;
            }
            else if (position.y < 0)
            {
                position.y = P5JSExtension.height;
            }
        }

        Vector2 align(List<Boid>boids)
        {
            var perceptionRadius = 50;
            var steering = new Vector2();
            var total = 0;
            foreach (Boid other in boids)
            {
                var d = P5JSExtension.dist(position.x, position.y, other.position.x, other.position.y);
                if(other != this && d < perceptionRadius)
                {
                    steering += other.velocity;
                    total++;
                }
            }
            if(total > 0)
            {
                steering /= total;
                steering = steering.setMag(maxSpeed);
                steering -= velocity;
                steering = steering.limit(maxForce);
            }
            return steering;
        }

        Vector2 cohesion(List<Boid> boids)
        {
            var perceptionRadius = 50;
            var steering = new Vector2();
            var total = 0;
            foreach (Boid other in boids)
            {
                var d = P5JSExtension.dist(position.x, position.y, other.position.x, other.position.y);
                if (other != this && d < perceptionRadius)
                {
                    steering += other.position;
                    total++;
                }
            }
            if (total > 0)
            {
                steering /= total;
                steering -= position;
                steering = steering.setMag(maxSpeed);
                steering -= velocity;
                steering = steering.limit(maxForce);
            }
            return steering;
        }
        Vector2 seperation(List<Boid> boids)
        {
            var perceptionRadius = 50;
            var steering = new Vector2();
            var total = 0;
            foreach (Boid other in boids)
            {
                var d = P5JSExtension.dist(position.x, position.y, other.position.x, other.position.y);
                if (other != this && d < perceptionRadius)
                {
                    var diff = position - other.position;
                    diff /= (d);
                    steering += diff;
                    total++;
                }
            }
            if (total > 0)
            {
                steering /= total;
                steering = steering.setMag(maxSpeed);
                steering -= velocity;
                steering = steering.limit(maxForce);
            }
            return steering;
        }

        public void show()
        {
            P5JSExtension.strokeWeight(8);
            P5JSExtension.stroke(255);
            P5JSExtension.point(position.x, position.y);
        }
        public void flock(List<Boid> boids)
        {
            acceleration*= 0;
            var alignment = align(boids);
            var cohesion = this.cohesion(boids);
            var seperation = this.seperation(boids);

            seperation *= alignSlidervalue;
            cohesion *= cohesionSlidervalue;
            seperation *= seperationSlidervalue;

            acceleration += seperation;
            acceleration += alignment;
            acceleration += cohesion;
        }
        public void update()
        {
            position += velocity;
            velocity += acceleration;
            velocity = velocity.limit(maxSpeed);
        }
    }
    List<Boid> flock = new List<Boid>();
    public Slider alignSlider;
    public Slider cohesionSlider;
    public Slider seperationSlider;

    public static float alignSlidervalue = 1;
    public static float cohesionSlidervalue = 1;
    public static float seperationSlidervalue = 1;

    void Start()
    {
        //640 360
        alignSlider.value = 1;
        alignSlider.minValue = 0;
        alignSlider.maxValue = 5;

        cohesionSlider.value = 1;
        cohesionSlider.minValue = 0;
        cohesionSlider.maxValue = 5;

        seperationSlider.value = 1;
        seperationSlider.minValue = 0;
        seperationSlider.maxValue = 5;

        for (int i = 0;i < 100;i++)
        {
            flock.Add(new Boid());
        }
    }
    void OnGUI()
    {
        alignSlidervalue = seperationSlider.value;
        cohesionSlidervalue = cohesionSlider.value;
        seperationSlidervalue = seperationSlider.value;

        P5JSExtension.frameRate(30);
        P5JSExtension.background(51);

        foreach(Boid boid in flock)
        {
            boid.edges();
            boid.flock(flock);
            boid.update();
            boid.show();
        }
    }
}