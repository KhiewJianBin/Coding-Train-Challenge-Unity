using System.Collections.Generic;
using UnityEngine;

public class Lissajous_Curve_Table : MonoBehaviour
{
    public class Curve
    {
        List<Vector2> path;
        Vector2 current;
        public Curve()
        {
            path = new List<Vector2>();
            current = new Vector2();
        }
        public void setX(float x)
        {
            current.x = x;
        }
        public void setY(float y)
        {
            current.y = y;
        }
        public void addPoint()
        {
            path.Add(current);
        }
        public void reset()
        {
            path.Clear();
        }
        public void show(GameObject gameObject)
        {
            P5JSExtension.stroke(255);
            P5JSExtension.strokeWeight(1);
            P5JSExtension.noFill();
            P5JSExtension.beginShape(MeshTopology.LineStrip);
            foreach (Vector2 v in path)
            {
                P5JSExtension.vertex(v.x, v.y);
            }
            gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();

            P5JSExtension.strokeWeight(8);
            P5JSExtension.point(current.x, P5JSExtension.height-current.y);
            current = new Vector2();
        }
    }
    float angle = 0;
    int w = 80;
    int cols,rows;
    Curve[][] curves;

    public void Start()
    {
        cols = P5JSExtension.width / w - 1;
        rows = P5JSExtension.height / w - 1;
        curves = new Curve[rows][];

        for (int j = 0; j < rows; j ++)
        {
            curves[j] = new Curve[cols];
            for (int i = 0; i < cols; i++)
            {
                curves[j][i] = new Curve();
            }
        }
    }
    void OnGUI()
    {
        float d = w - 0.2f*w;
        float r = d / 2;

        P5JSExtension.noFill();
        P5JSExtension.stroke(255);
        for (int i = 0; i < cols; i++)
        {
            float cx = w + i * w + w/2;
            float cy = w / 2;
            P5JSExtension.strokeWeight(1);
            P5JSExtension.stroke(255);
            P5JSExtension.ellipse(cx, cy, d, d);
            float x = r * Mathf.Cos(angle * (i + 1) - Mathf.PI / 2);
            float y = r * Mathf.Sin(angle * (i + 1) - Mathf.PI / 2);
            P5JSExtension.strokeWeight(8);
            P5JSExtension.stroke(255);
            P5JSExtension.point(cx + x, cy + y);

            P5JSExtension.stroke(255, 150);
            P5JSExtension.strokeWeight(1);
            P5JSExtension.line(cx + x, 0,cx + x,P5JSExtension.height);

            for (int j = 0; j < rows; j++)
            {
                curves[j][i].setX(cx + x);
            }
        }

        P5JSExtension.noFill();
        P5JSExtension.stroke(255);
        for (int j = 0; j < rows; j++)
        {
            float cx = w / 2;
            float cy = w + j * w + w / 2;
            P5JSExtension.strokeWeight(1);
            P5JSExtension.stroke(255);
            P5JSExtension.ellipse(cx, cy, d, d);
            float x = r * Mathf.Cos(angle * (j + 1) - Mathf.PI / 2);
            float y = r * Mathf.Sin(angle * (j + 1) - Mathf.PI / 2);
            P5JSExtension.strokeWeight(8);
            P5JSExtension.stroke(255);
            P5JSExtension.point(cx + x, cy + y);

            P5JSExtension.stroke(255, 100);
            P5JSExtension.strokeWeight(1);
            P5JSExtension.line(0, cy+y,P5JSExtension.width, cy + y);

            for (int i = 0; i < cols; i++)
            {
                curves[j][i].setY(P5JSExtension.width-(cy + y));
            }
        }
        P5JSExtension.resetShape();
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < cols; i++)
            {
                curves[j][i].addPoint();
                curves[j][i].show(gameObject);
            }
        }

        angle -= 0.05f * (80/w) * (80/w);

        if(angle < -2*Mathf.PI)
        {
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    curves[j][i].reset();
                }
            }
            angle = 0;
        }
    }
}