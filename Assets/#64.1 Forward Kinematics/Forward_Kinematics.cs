using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forward_Kinematics : MonoBehaviour
{
    public class Segment
    {
        Vector2 a;
        float len;
        float angle;
        float selfAngle;

        float t;

        Segment parent = null;
        public Segment child = null;

        Vector2 b;

        public Segment(float x,float y,float len_,float angle_,float t_)
        {
            a = new Vector2(x, y);
            len = len_;
            angle = angle_;
            selfAngle = angle;
            calculateB();
            t = t_;
        }
        public Segment(Segment parent_,float len_,float angle_, float t_)
        {
            parent = parent_;
            a = new Vector2(parent.b.x, parent.b.y);
            len = len_;
            angle = angle_;
            calculateB();
            t = t_;
        }
        void calculateB()
        {
            float dx = len * Mathf.Cos(angle);
            float dy = len * Mathf.Sin(angle);
            b = new Vector2(a.x + dx, a.y + dy);
        }
        public void show()
        {
            P5JSExtension.stroke(255);
            P5JSExtension.strokeWeight(4);
            P5JSExtension.line(a.x, a.y, b.x, b.y);
        }
        public void wiggle()
        {
            float maxangle = 0.3f;
            float minangle = -0.3f;
            selfAngle = P5JSExtension.map(P5JSExtension.noise(t), 0, 1, maxangle, minangle);
            t += 0.03f;
            //selfAngle = selfAngle + 0.01f;
        }
        public void update()
        {
            angle = selfAngle;
            if(parent != null)
            {
                a = parent.b;
                angle += parent.angle;  
            }
            else
            {
                angle += -Mathf.PI / 2;
            }
            calculateB();
        }

    }
    Segment tentacle;
    //Segment seg2;

    void Start()
    {
        //600x400
        float t = 0;
        float len = 50;
        tentacle = new Segment(P5JSExtension.width/2f, P5JSExtension.height, 10, -45 * Mathf.Deg2Rad,t);

        Segment current = tentacle;
        for(int i = 0; i < 20;i++)
        {
            t += 0.1f;
            len = len * 0.75f;
            Segment next = new Segment(current, 10, 0,t);
            current.child = next;
            current = next;
        }
    }
    void OnGUI()
    {
        P5JSExtension.background(51);

        Segment next = tentacle;
        while(next != null)
        {
            next.wiggle();
            next.update();
            next.show();
            next = next.child;
        }
    }
}