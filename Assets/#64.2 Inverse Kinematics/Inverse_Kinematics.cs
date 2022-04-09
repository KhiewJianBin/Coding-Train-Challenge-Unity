using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inverse_Kinematics : MonoBehaviour
{
    public class Segment
    {
        public Vector2 a;
        float angle = 0;
        float len;
        Vector2 b;
        public Segment parent;
        public Segment child;
        float sw = 0;

        public Segment(float x,float y, float len_,float i)
        {
            a = new Vector2(x, y);
            sw = P5JSExtension.map(i,0,20,1, 10);
            len = len_;
            calculateB();
        }
        public Segment(Segment parent_, float len_, float i)
        {
            parent = parent_;
            a = parent.b;
            sw = P5JSExtension.map(i, 0, 20, 1, 10);
            len = len_;
            calculateB();
        }
        public void follow()
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
    Segment tentacle;

    void Start()
    {
        //600x400
        Segment current = new Segment(300, 200, 10, 0);
        for (int i = 0;i<20; i++)
        {
            Segment next = new Segment(current, 10, i);
            current.child = next;
            current = next;
        }
        tentacle = current;
    }
    void OnGUI()
    {
        P5JSExtension.background(51);

        tentacle.follow(Input.mousePosition.x,P5JSExtension.height-Input.mousePosition.y);
        tentacle.update();
        tentacle.show();

        Segment next = tentacle.parent;
        while(next != null)
        {
            next.follow();
            next.update();
            next.show();
            next = next.parent;
        }
    }
}