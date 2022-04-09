using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inverse_Kinematics_Fixed_Point : MonoBehaviour
{
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
            sw = P5JSExtension.map(i,0,20,1, 10);
            len = len_;
            calculateB();
        }
        public Segment(Segment parent, float len_, float i)
        {
            a = parent.b;
            sw = P5JSExtension.map(i, 0, 20, 1, 10);
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
    Segment[] tentacle = new Segment[30];
    Vector2 bases;
    float len = 20;
    void Start()
    {
        //800x400
        tentacle[0] = new Segment(300, 200, len, 0);
        for (int i = 1;i< tentacle.Length; i++)
        {
            tentacle[i] = new Segment(tentacle[i - 1], len, i);
        }
        bases = new Vector2(P5JSExtension.width / 2, P5JSExtension.height);
    }
    void OnGUI()
    {
        P5JSExtension.background(51);

        int total = tentacle.Length;
        Segment end = tentacle[tentacle.Length - 1];
        end.follow(Input.mousePosition.x,P5JSExtension.height-Input.mousePosition.y);
        end.update();

        for(int i = total - 2; i >=0; i--)
        {
            tentacle[i].follow(tentacle[i+1]);
            tentacle[i].update();
            
        }

        tentacle[0].setA(bases);

        for (int i = 1; i < total; i++)
        {
            tentacle[i].setA(tentacle[i - 1].b);
        }

        for (int i = 0; i < total; i++)
        {
            tentacle[i].show();
        }
    }
}