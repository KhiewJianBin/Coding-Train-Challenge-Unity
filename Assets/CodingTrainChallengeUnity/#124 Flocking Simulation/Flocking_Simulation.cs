using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static P5JSExtension;

public class Flocking_Simulation : P5JSBehaviour
{
    List<Boid> flock = new();

    public Slider alignSlider, cohesionSlider, seperationSlider;

    public static float alignSlidervalue = 1;
    public static float cohesionSlidervalue = 1;
    public static float seperationSlidervalue = 1;

    protected override void setup()
    {
        createCanvas(600, 400);

        alignSlider.value = 1;
        alignSlider.minValue = 0;
        alignSlider.maxValue = 5;

        cohesionSlider.value = 1;
        cohesionSlider.minValue = 0;
        cohesionSlider.maxValue = 5;

        seperationSlider.value = 1;
        seperationSlider.minValue = 0;
        seperationSlider.maxValue = 5;

        for (int i = 0; i < 100; i++)
        {
            flock.push(new Boid());
        }
    }

    protected override void Update()
    {
        base.Update();

        alignSlidervalue = seperationSlider.value;
        cohesionSlidervalue = cohesionSlider.value;
        seperationSlidervalue = seperationSlider.value;

        foreach (var boid in flock)
        {
            boid.edges();
            boid.flock(flock);
            boid.update();
        }
    }

    protected override void draw()
    {
        background(51);
        foreach (Boid boid in flock)
        {
            boid.show();
        }
    }


    public class Boid
    {
        Vector2 position;
        Vector2 velocity;
        Vector2 acceleration;
        float maxForce;
        float maxSpeed;

        public Boid()
        {
            this.position = new Vector2(random(width), random(height));
            this.velocity = random2D();
            this.velocity = velocity.setMag(random(2f, 4f));
            this.acceleration = new Vector2();
            this.maxForce = 0.2f;
            this.maxSpeed = 5;
        }

        public void edges()
        {
            if (this.position.x > width)
            {
                this.position.x = 0;
            }
            else if (this.position.x < 0)
            {
                this.position.x = width;
            }

            if (this.position.y > height)
            {
                this.position.y = 0;
            }
            else if (this.position.y < 0)
            {
                this.position.y = height;
            }
        }

        Vector2 align(List<Boid> boids)
        {
            var perceptionRadius = 50;
            var steering = createVector();
            var total = 0;
            foreach (var other in boids)
            {
                var d = dist(this.position.x, this.position.y, other.position.x, other.position.y);
                if (other != this && d < perceptionRadius)
                {
                    steering = steering.add(other.velocity);
                    total++;
                }
            }
            if (total > 0)
            {
                steering = steering.div(total);
                steering = steering.setMag(this.maxSpeed);
                steering = steering.sub(this.velocity);
                steering = steering.limit(this.maxForce);
            }
            return steering;
        }

        Vector2 seperation(List<Boid> boids)
        {
            var perceptionRadius = 50;
            var steering = createVector();
            var total = 0;
            foreach (var other in boids)
            {
                var d = dist(this.position.x, this.position.y, other.position.x, other.position.y);
                if (other != this && d < perceptionRadius)
                {
                    var diff = this.position - other.position;
                    diff = diff.div(d * d);
                    steering = steering.add(diff);
                    total++;
                }
            }
            if (total > 0)
            {
                steering = steering.div(total);
                steering = steering.setMag(this.maxSpeed);
                steering = steering.sub(this.velocity);
                steering = steering.limit(this.maxForce);
            }
            return steering;
        }

        Vector2 cohesion(List<Boid> boids)
        {
            var perceptionRadius = 50;
            var steering = createVector();
            var total = 0;
            foreach (var other in boids)
            {
                var d = dist(this.position.x, this.position.y, other.position.x, other.position.y);
                if (other != this && d < perceptionRadius)
                {
                    steering = steering.add(other.position);
                    total++;
                }
            }
            if (total > 0)
            {
                steering = steering.div(total);
                steering = steering.sub(this.position);
                steering = steering.setMag(maxSpeed);
                steering = steering.sub(this.velocity);
                steering = steering.limit(this.maxForce);
            }
            return steering;
        }
        
        public void flock(List<Boid> boids)
        {
            var alignment = this.align(boids);
            var cohesion = this.cohesion(boids);
            var seperation = this.seperation(boids);

            alignment = alignment.mult(alignSlidervalue);
            cohesion = cohesion.mult(cohesionSlidervalue);
            seperation = seperation.mult(seperationSlidervalue);

            this.acceleration = acceleration.add(alignment);
            this.acceleration = acceleration.add(cohesion);
            this.acceleration = acceleration.add(seperation);
        }
        public void update()
        {
            this.position = position.add(this.velocity);
            this.velocity = velocity.add(this.acceleration);
            this.velocity = velocity.limit(this.maxSpeed);
            this.acceleration = this.acceleration.mult(0);
        }

        public void show()
        {
            strokeWeight(6);
            stroke(255);
            point(this.position.x, this.position.y);
        }
    }
}