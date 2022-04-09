using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fractal_Spirograph : MonoBehaviour
{
    static int k = -4;
    class Orbit
    {
        public float x;
        public float y;
        float r;
        int n;
        Orbit parent;
        public Orbit child;
        float speed;
        float angle;

        public Orbit(float x_,float y_,float r_,int n_,Orbit p = null)
        {
            x = x_;
            y = y_;
            r = r_;
            n = n_;
            speed = (Mathf.Pow(k, n - 1) * Mathf.Deg2Rad)/ resolution;
            parent = p;
            child = null;
            angle = -Mathf.PI/2;
        }
        public Orbit addChild()
        {
            float newr = r / 3f;
            float newx = x + r + newr;
            float newy = y;
            child = new Orbit(newx, newy, newr, n+1,this);
            return child;
        }
        public void update()
        {
            if (parent != null)
            {
                angle += speed;
                float rsum = r + parent.r;
                x = parent.x + rsum * Mathf.Cos(angle);
                y = parent.y + rsum * Mathf.Sin(angle);
                //P5JSExtension.ellipse(x2, y2, r2 * 2, r2 * 2);
            }
        }
        public void show()
        {
            P5JSExtension.stroke(255,100);
            P5JSExtension.strokeWeight(1);
            P5JSExtension.noFill();
            P5JSExtension.ellipse(x, y, r * 2, r * 2);
        }
        
    }

    List<Vector2> path;

    float angle = 0;
    static int resolution = 10;

    Orbit sun;
    Orbit end;

    void Start()
    {
        path = new List<Vector2>();
        sun = new Orbit(300,300,150,0);
        Orbit next = sun;
        for(int i = 0;i<10;i++)
        {
            next = next.addChild();
        }
        end = next;
    }
    void OnGUI()
    {
        P5JSExtension.background(51);

        Orbit next = sun;
        for (int i = 0; i < resolution;i++)
        {
            while (next != null)
            {
                next.update();
                //next.show();
                next = next.child;
            }

            path.Add(new Vector2(end.x, end.y));
        }
        

        P5JSExtension.resetShape();
        P5JSExtension.beginShape(MeshTopology.LineStrip);
        foreach (Vector2 pos in path)
        {
            P5JSExtension.vertex(pos.x, P5JSExtension.height - pos.y);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();
    }
}