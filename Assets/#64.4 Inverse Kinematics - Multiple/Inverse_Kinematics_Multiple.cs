using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inverse_Kinematics_Multiple : MonoBehaviour
{
    public class Tentacle
    {
        Segment[] segments = new Segment[5];
        Vector2 bases;
        float len = 50;

        public Tentacle(float x,float y)
        {
            bases = new Vector2(x, y);
            segments[0] = new Segment(300, 200, len, 0);
            for (int i = 1; i < segments.Length; i++)
            {
                segments[i] = new Segment(segments[i - 1], len, i);
            }
        }
        public void update()
        {
            int total = segments.Length;
            Segment end = segments[segments.Length - 1];
            end.follow(pos.x, pos.y);
            end.update();

            for (int i = total - 2; i >= 0; i--)
            {
                segments[i].follow(segments[i + 1]);
                segments[i].update();

            }

            segments[0].setA(bases);

            for (int i = 1; i < total; i++)
            {
                segments[i].setA(segments[i - 1].b);
            }


        }
        public void show()
        {
            foreach (Segment s in segments)
            {
                s.show();
            }
        }
    }
    public class Segment
    {
        public Vector2 a;
        float angle = 0;
        float len;
        public Vector2 b;
        //public Segment child;
        float sw = 0;

        public Segment(float x,float y, float len_,float i)
        {
            a = new Vector2(x, y);
            sw = 4;//P5JSExtension.map(i,0,20,1, 10);
            len = len_;
            calculateB();
        }
        public Segment(Segment parent, float len_, float i)
        {
            a = parent.b;
            sw = 4;//P5JSExtension.map(i,0,20,1, 10);
            len = len_;
            calculateB();
        }
        public void follow(Segment child)
        {
            float targetX = child.a.x;
            float targetY = child.a.y;
            follow(targetX, targetY);
        }
        public void follow(float tx,float ty)
        {
            Vector2 target = new Vector2(tx, ty);
            Vector2 dir = (target - a);
            angle = dir.heading();

            dir = dir.setMag(len);
            dir *= -1;

            a = target + dir;
        }
        public void setA(Vector2 pos)
        {
            a = pos;
            calculateB();
        }
        void calculateB()
        {
            float dx = len * Mathf.Cos(angle);
            float dy = len * Mathf.Sin(angle);
            b.Set(a.x + dx, a.y + dy);
        }
        public void update()
        {
            calculateB();
        }
        public void show()
        {
            P5JSExtension.stroke(255);
            P5JSExtension.strokeWeight(sw);
            P5JSExtension.line(a.x, a.y, b.x, b.y);
        }
    }

    List<Tentacle> tentacles;

    public static Vector2 pos;
    Vector2 vel;
    Vector2 gravity;

    void Start()
    {
        //800x600
        pos = new Vector2(0, 0);
        vel = new Vector2(2, 1.3f);
        gravity = new Vector2(0, 0.1f);
        vel *= 3;

        tentacles = new List<Tentacle>();

        float da = 2 * Mathf.PI / 2;
        for(float a = 0; a< 2 * Mathf.PI; a+= da)
        {
            float x = P5JSExtension.width / 2 + Mathf.Cos(a) * 300;
            float y = P5JSExtension.height / 2 + Mathf.Sin(a) * 300;
            tentacles.Add(new Tentacle(x, y));
        }
    }
    void OnGUI()
    {
        P5JSExtension.background(51);
        P5JSExtension.noFill();
        P5JSExtension.ellipse(P5JSExtension.width / 2, P5JSExtension.height / 2, 400, 400);
        foreach (Tentacle t in tentacles)
        {
            t.update();
            t.show();
        }

        pos += vel;
        vel += gravity;
        P5JSExtension.noStroke();
        P5JSExtension.fill(100, 255, 0);
        P5JSExtension.ellipse(pos.x, pos.y, 32, 32);

        if(pos.x > P5JSExtension.width || pos.x< 0)
        {
            vel.x *= -1;
        }
        if (pos.y > P5JSExtension.height)
        {
            pos.y = P5JSExtension.height;
            vel.y *= -1;
            vel *= 0.99f;
        }
    }
}