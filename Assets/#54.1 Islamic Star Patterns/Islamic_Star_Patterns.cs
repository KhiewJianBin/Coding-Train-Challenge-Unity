using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Islamic_Star_Patterns : MonoBehaviour
{
    class Polygon
    {
        List<Vector2> vertices = new List<Vector2>();
        List<Edge> edges = new List<Edge>();

        public void addVertex(float x, float y)
        {
            var a = new Vector2(x, y);
            var total = vertices.Count;
            if (total > 0)
            {
                var prev = vertices[total-1];
                var edge = new Edge(prev, a);
                edges.Add(edge);
            }
            vertices.Add(a);
        }

        public void show()
        {
            for(var i  = 0; i < edges.Count;i++)
            {
                edges[i].show();
            }
        }

        public void close()
        {
            var total = vertices.Count;
            var last = vertices[total - 1];
            var first = vertices[0];
            var edge = new Edge(last, first);
            edges.Add(edge);
        }

        public void hankin()
        {
            for (var i = 0; i < edges.Count; i++)
            {
                edges[i].hankin();
            }
            for (var i = 0; i < edges.Count; i++)
            {
                for (var j = 0; j < edges.Count; j++)
                {
                    if (i != j)
                    {
                        edges[i].findEnds(edges[j]);
                    }
                }
            } 
        }
    }
    class Edge
    {
        Vector2 a;
        Vector2 b;
        public Hankin h1;
        public Hankin h2;
        
        public Edge(Vector2 a, Vector2 b)
        {
            this.a = a;
            this.b = b;
        }
        public void show()
        {
            P5JSExtension.stroke(255,50); 
            P5JSExtension.line(a.x, a.y, b.x, b.y);
            h1.show();
            h2.show();
        }
        public void hankin()
        {
            var mid = a + b;
            mid *= 0.5f;

            var v1 = a - mid;
            var v2 = b - mid;
            var offset1 = mid;
            var offset2 = mid;
            if (delta > 0)
            {
                v1 = v1.setMag(delta);
                v2 = v2.setMag(delta);
                offset1 = mid + v2;
                offset2 = mid + v1;
            }
            v1.Normalize();
            v2.Normalize();

            v1 = v1.rotate(-angle * Mathf.Deg2Rad);
            v2 = v2.rotate(angle * Mathf.Deg2Rad);

            h1 = new Hankin(offset1, v1);
            h2 = new Hankin(offset2, v2);
        }
        public void findEnds(Edge edges)
        {
            h1.findEnd(edges.h1);
            h1.findEnd(edges.h2);
            h2.findEnd(edges.h1);
            h2.findEnd(edges.h2);

            //for (int i = 0;i<edges.Count;i++)
            //{

            //}
        }
    }

    class Hankin
    {
        Vector2 a;
        Vector2 v;
        Vector2 b;
        Vector2 end;
        float prevD;

        public Hankin(Vector2 a, Vector2 v)
        {
            this.a = a;
            this.v = v;
            b = a + v;
        }
        public void show()
        {
            P5JSExtension.stroke(255,0,255);
            P5JSExtension.line(a.x, a.y, end.x, end.y);
            //P5JSExtension.fill(255);
            //P5JSExtension.ellipse(a.x, a.y, 4);
            
            //P5JSExtension.fill(255, 255, 0);
            //P5JSExtension.ellipse(end.x, end.y, 4);
        }
        public void findEnd(Hankin other)
        {
            //line line intersection
            var den = (other.v.y * v.x) - (other.v.x * v.y);
            var numa = (other.v.x * (a.y - other.a.y)) - (other.v.y * (a.x - other.a.x));
            var numb = (v.x * (a.y - other.a.y)) - (v.y * (a.x - other.a.x));
            var ua = numa / den;
            var ub = numb / den;
            var x = a.x + ua * (v.x);
            var y = a.y + ua * (v.y);

            if (ua > 0 && ub > 0)
            {
                var candidate = new Vector2(x, y);
                var d1 = Vector2.Distance(candidate, a);
                var d2 = Vector2.Distance(candidate, other.a);
                var d = d1 + d2;
                var diff = Mathf.Abs(d1 - d2);
                if(diff<0.001f)
                {
                    if (end == Vector2.zero)
                    {
                        end = candidate;
                        prevD = d;
                    }
                    else if (d < prevD)
                    {
                        prevD = d;
                        end = candidate;
                    }
                }
            }
        }
    }

    public static float angle = 75;
    public static float delta = 10;

    List<Polygon> polys = new List<Polygon>();
    public Slider deltaSlider;
    public Slider angleSlider;

    void Start()
    {
        deltaSlider.minValue = 0;
        deltaSlider.maxValue = 25;
        deltaSlider.value = 0;
        angleSlider.minValue = 0;
        angleSlider.maxValue = 90;
        angleSlider.value = 75;

        var inc = 100;
        for(var x = 0; x < P5JSExtension.width; x+= inc)
        {
            for (var y = 0; y < P5JSExtension.height; y += inc)
            {
                var poly = new Polygon();
                poly.addVertex(x, y);
                poly.addVertex(x + inc, y);
                poly.addVertex(x + inc, y + inc);
                poly.addVertex(x, y + inc);
                poly.close();
                polys.Add(poly);
            }
        }
    }
    void OnGUI()
    {
        angle = angleSlider.value;
        delta = deltaSlider.value;
        for (var i = 0; i < polys.Count; i++)
        {
            polys[i].hankin();
            polys[i].show();
        }
    }
}