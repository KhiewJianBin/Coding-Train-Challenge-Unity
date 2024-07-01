using UnityEngine;
using System.Collections.Generic;
using static P5JSExtension;


public class SpaceInvaders : P5JSBehaviour
{
    Ship ship;
    List<Flower> flowers = new();
    List<Drop> drops = new();

    protected override void setup()
    {
        createCanvas(600, 400);
        ship = new Ship();
        for (var i = 0; i < 6; i++)
        {
            flowers.Add(new Flower(i * 80 + 80, 60));
        }
    }

    protected override void Update()
    {
        base.Update();

        ship.move();

        for (var i = 0; i < drops.Count; i++)
        {
            drops[i].move();
            for (var j = 0; j < flowers.Count; j++)
            {
                if (drops[i].hits(flowers[j]))
                {
                    flowers[j].grow();
                    drops[i].evaporate();
                }
            }
        }

        var edge = false;

        for (var i = 0; i < flowers.Count; i++)
        {
            flowers[i].move();
            if (flowers[i].x > width || flowers[i].x < 0)
            {
                edge = true;
            }
        }

        if (edge)
        {
            for (var i = 0; i < flowers.Count; i++)
            {
                flowers[i].shiftDown();
            }
        }

        for (var i = drops.Count - 1; i >= 0; i--)
        {
            if (drops[i].toDelete)
            {
                drops.splice(i, 1);
            }
        }
    }

    protected override void draw()
    {
        background(51);
        ship.show();

        for (var i = 0; i < drops.Count; i++)
        {
            drops[i].show();
        }

        for (var i = 0; i < flowers.Count; i++)
        {
            flowers[i].show();
        }
    }
    protected override void keyReleased()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ship.setDir(0);
        }
    }
    protected override void keyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Drop drop = new Drop(ship.x, height);
            drops.Add(drop);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ship.setDir(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ship.setDir(-1);
        }
    }

    public class Ship
    {
        public float x = width / 2;
        public int xdir = 0;

        public void show()
        {
            fill(255);
            rectMode(CENTER);
            rect(this.x, height - 20, 20, 60);
        }
        public void setDir(int dir)
        {
            this.xdir = dir;
        }

        public void move()
        {
            this.x += this.xdir * 5;
        }
    }

    public class Drop
    {
        public float x;
        public float y;
        public float r;
        public bool toDelete;

        public Drop(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.r = 8;
            this.toDelete = false;
        }

        public void show()
        {
            noStroke();
            fill(150, 0, 255);
            ellipse(this.x, this.y, this.r * 2, this.r * 2);
        }

        public void evaporate()
        {
            toDelete = true;
        }

        public bool hits(Flower flower)
        {
            var d = dist(this.x, this.y, flower.x, flower.y);
            if (d < this.r + flower.r)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void move()
        {
            y = y - 5;
        }
    }

    public class Flower
    {
        public float x;
        public float y;
        public float r;
        public int xdir;

        public Flower(float inx, float iny)
        {
            this.x = inx;
            this.y = iny;
            this.r = 30;

            this.xdir = 1;
        }

        public void grow()
        {
            this.r = this.r + 2;
        }
        public void shiftDown()
        {
            this.xdir *= -1;
            this.y += this.r;
        }

        public void move()
        {
            this.x = this.x + this.xdir;
        }

        public void show()
        {
            noStroke();
            fill(255, 0, 200, 150);
            ellipse(this.x, this.y, this.r * 2, this.r * 2);
        }
    }
}
