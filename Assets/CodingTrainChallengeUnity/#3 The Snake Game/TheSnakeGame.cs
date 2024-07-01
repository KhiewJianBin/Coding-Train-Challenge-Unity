using System.Collections.Generic;
using UnityEngine;
using static P5JSExtension;

public class TheSnakeGame : P5JSBehaviour
{
    public class Snake
    {
        float x;
        float y;
        float xspeed;
        float yspeed;
        public int total;
        List<Vector2> tail;

        public Snake()
        {
            this.x = 0;
            this.y = 0;
            this.xspeed = 1;
            this.yspeed = 0;
            this.total = 0;
            this.tail = new List<Vector2>();
        }
        public bool eat(Vector2 pos)
        {
            float d = dist(this.x, this.y, pos.x, pos.y);
            if (d < 1)
            {
                this.total++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void dir(float x, float y)
        {
            xspeed = x;
            yspeed = y;
        }

        public void death()
        {
            for (var i = 0; i < this.tail.Count; i++)
            {
                Vector2 pos = this.tail[i];
                var d = dist(this.x, this.y, pos.x, pos.y);
                if (d < 1)
                {
                    Debug.Log("starting over");
                    this.total = 0;
                    this.tail = new();
                }
            }
        }

        public void Update()
        {
            for (var i = 0; i < this.tail.Count - 1; i++)
            {
                this.tail[i] = this.tail[i + 1];
            }

            // Special case
            //if (this.total >= 1)
            //{
            //    this.tail.Add(createVector(this.x, this.y));
            //}
            if (tail.Count < total)
            {
                tail.Add(new Vector2(x, y));
            }
            else if (tail.Count >= 1)
            {
                tail[tail.Count - 1] = new Vector2(x, y);
            }

            this.x = this.x + this.xspeed * scl;
            this.y = this.y + this.yspeed * scl;

            this.x = constrain(this.x, 0, width - scl);
            this.y = constrain(this.y, 0, height - scl);
        }

        public void Show()
        {
            fill(255);
            for (int i = 0; i < this.tail.Count; i++)
            {
                rect(this.tail[i].x, this.tail[i].y, scl, scl);
            }

            rect(this.x, this.y, scl, scl);
        }
    }

    public static Snake s;
    public static float scl = 20;
    Vector2 food;

    protected override void setup()
    {
        createCanvas(600, 600);
        s = new Snake();
        frameRate(10);
        pickLocation();
    }

    void pickLocation()
    {
        int cols = floor(width / scl);
        int rows = floor(height / scl);
        food = createVector(random(0, cols), random(0, rows));
        food = food.mult(scl);
    }

    protected override void mousePressed()
    {
        s.total++;
    }

    protected override void Update()
    {
        base.Update();

        if (s.eat(food))
        {
            pickLocation();
        };

        s.death();
        s.Update();
    }

    protected override void draw()
    {
        background(51);

        s.Show();

        fill(255, 0, 100);
        rect(food.x, food.y, scl, scl);
    }

    protected override void keyPressed()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            s.dir(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            s.dir(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            s.dir(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            s.dir(-1, 0);
        }
    }
}