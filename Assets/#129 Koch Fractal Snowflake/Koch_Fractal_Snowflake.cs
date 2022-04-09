using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Koch_Fractal_Snowflake : MonoBehaviour
{
    class Segment
    {
        PVector a;
        PVector b;
        public Segment(PVector a_,PVector b_)
        {
            a = a_.copy();
            b = b_.copy();
        }
        public Segment[] generate()
        {
            Segment[] children = new Segment[4];

            PVector v = b - a;
            v /= 3f;

            //Segment 0
            PVector b1 = a + v;
            children[0] = new Segment(a, b1);

            //Segment 3
            PVector a1 = b - v;
            children[3] = new Segment(a1, b);

            v = v.rotate(-Mathf.PI / 3);
            PVector c = b1 + v;
            //Segment 1
            children[1] = new Segment(b1, c);
            //Segment 2
            children[2] = new Segment(c, a1);
            return children;
        }
        public void show()
        {
            P5JSExtension.line(a.x, a.y, b.x, b.y);
        }
    }
    List<Segment> segments;
    void Start()
    {
        segments = new List<Segment>();
        PVector a = new PVector(0, 100);
        PVector b = new PVector(600, 100);
        Segment s1 = new Segment(a, b);

        float len = P5JSExtension.dist(a, b);
        float h = len * Mathf.Sqrt(3)/2;
        PVector c = new PVector(300, 100+h);

        Segment s2 = new Segment(b, c);
        Segment s3 = new Segment(c, a);
        segments.Add(s1);
        segments.Add(s2);
        segments.Add(s3);
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            List<Segment> nextGeneration = new List<Segment>();
            foreach (Segment s in segments)
            {
                Segment[] children = s.generate();
                nextGeneration.AddRange(children);
            }
            segments = nextGeneration;
        }
    }
    void OnGUI()
    {
        P5JSExtension.resetMatrix();
        P5JSExtension.background(0);
        P5JSExtension.translate(0, 100);

        P5JSExtension.stroke(255);
        foreach (Segment s in segments)
        {
            s.show();
        }
    }
}