using System.Collections.Generic;
using UnityEngine;

public class Flappy_Bird : MonoBehaviour
{
    public class Bird
    {
        public float y = P5JSExtension.height / 2;
        public float x = 64;

        float gravity = 0.7f;
        float lift = -12;
        float velocity = 0;
 
        public void show()
        {
            P5JSExtension.fill(255);
            P5JSExtension.ellipse(x, y, 32, 32);
        }
        public void up()
        {
            velocity += lift;
        }

        public void update()
        {
            velocity += gravity;
            y += velocity;

            if (y > P5JSExtension.height)
            {
                y = P5JSExtension.height;
                velocity = 0;
            }

            if (y < 0)
            {
                y = 0;
                velocity = 0;
            }
        }
    }

    public class Pipe
    {
        float spacing = 175;
        float top = P5JSExtension.random(P5JSExtension.height / 6f, 3f / 4f * P5JSExtension.height);
        float bottom;
        float x = P5JSExtension.width;
        float w = 80;
        float speed = 6;

        bool highlight = false;

        public Pipe()
        {
            bottom = P5JSExtension.height - (top + spacing);
        }

        public bool hits(Bird bird)
        {
            if (bird.y <  top || bird.y > P5JSExtension.height -  bottom)
            {
                if (bird.x >  x && bird.x <  x +  w)
                {
                     highlight = true;
                    return true;
                }
            }
             highlight = false;
            return false;
        }
        public void show()
        {
            P5JSExtension.fill(255);
            if ( highlight)
            {
                P5JSExtension.fill(255, 0, 0);
            }
            P5JSExtension.rect( x, 0,  w,  top);
            P5JSExtension.rect( x, P5JSExtension.height -  bottom,  w,  bottom);
        }
        public void update()
        {
            x -= speed;
        }
        public bool offscreen()
        {
            if (x < -w)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    Bird bird;
    List<Pipe> pipes = new List<Pipe>();
    int frameCount = 0;
    void Start()
    {
        //640x480
        bird = new Bird();
        pipes.Add(new Pipe());
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            bird.up();
            // Debug.Log("SPACE");
        }
    }
    void OnGUI()
    {
        P5JSExtension.frameRate(30);

        P5JSExtension.background(0);

        for (var i = pipes.Count - 1; i >= 0; i--)
        {
            pipes[i].show();
            pipes[i].update();

            if (pipes[i].hits(bird))
            {
                Debug.Log("HIT");
            }

            if (pipes[i].offscreen())
            {
                pipes.RemoveAt(i);
            }
        }

        bird.update();
        bird.show();

        frameCount++;
        if (frameCount % 75 == 0)
        {
            pipes.Add(new Pipe());
        }
    }
}