using UnityEngine;
using System.Collections.Generic;


public class SpaceInvaders : MonoBehaviour
{
    public class Ship
    {
        public float x = P5JSExtension.width / 2;
        public int xdir = 0;

        public void show()
        {
            P5JSExtension.fill(255);
            P5JSExtension.rectMode(P5JSExtension.CENTER);
            P5JSExtension.rect(x, P5JSExtension.height - 20, 20, 60);
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

    public class Drop
    {
        public float x;
        public float y;
        public float r;
        public bool toDelete;

        public Drop(float inx, float iny)
        {
            x = inx;
            y = iny;
            r = 8;
            toDelete = false;
        }

        public void show()
        {
            P5JSExtension.noStroke();
            P5JSExtension.fill(150, 0, 255, 255);
            P5JSExtension.ellipse(x, y, r * 2, r * 2);
        }

        public void evaporate()
        {
            toDelete = true;
        }

        public bool hits(Flower flower)
        {
            var d = P5JSExtension.dist(x, y, flower.x, flower.y);
            if (d < r + flower.r)
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
            x = inx;
            y = iny;
            r = 30;
            xdir = 1;
        }

        public void grow()
        {
            r = r + 2;
        }
        public void shiftDown()
        {
            xdir *= -1;
            y += r;
        }

        public void move()
        {
            x = x + xdir;
        }

        public void show()
        {
            P5JSExtension.noStroke();
            P5JSExtension.fill(255, 0, 200, 150);
            P5JSExtension.ellipse(x, y, r * 2, r * 2);
        }
    }


    Ship ship;
    List<Flower> flowers = new List<Flower>();
    List<Drop> drops = new List<Drop>();

    void Start()
    {
        ship = new Ship();
        for (var i = 0; i < 6; i++)
        {
            flowers.Add(new Flower(i * 80 + 80, 60));
        }
    }

    void Update()
    {
        keyPressed();
        keyReleased();
    }
    void keyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Drop drop = new Drop(ship.x, P5JSExtension.height);
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
    void keyReleased()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ship.setDir(0);
        }
    }

    void OnGUI()
    {
        P5JSExtension.background(51);

        ship.show();
        ship.move();

        for (var i = 0; i < drops.Count; i++)
        {
            drops[i].show();
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
            flowers[i].show();
            flowers[i].move();
            if (flowers[i].x > P5JSExtension.width || flowers[i].x < 0)
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
                drops.RemoveAt(i);
            }
        }
    }
}
