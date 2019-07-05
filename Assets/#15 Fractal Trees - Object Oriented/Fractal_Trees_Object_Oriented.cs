using System.Collections.Generic;
using UnityEngine;

public class Fractal_Trees_Object_Oriented : MonoBehaviour
{
    List<Branch> tree = new List<Branch>();
    List<PVector2> leaves = new List<PVector2>();

    int count = 0;

    void Start()
    {
        PVector2 a = new PVector2(P5JSExtension.width / 2, P5JSExtension.height);
        PVector2 b = new PVector2(P5JSExtension.width / 2, P5JSExtension.height - 100);
        var root = new Branch(a,b);

        tree.Add(root);
    }
    void Update()
    {
        mousePressed();
    }
    void mousePressed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (var i = tree.Count - 1; i >= 0; i--)
            {
                if (!tree[i].finished)
                {
                    tree.Add(tree[i].branchA());
                    tree.Add(tree[i].branchB());
                }
                tree[i].finished = true;
            }
            count++;

            if (count == 6)
            {
                for (var i = 0; i < tree.Count; i++)
                {
                    if (!tree[i].finished)
                    {
                        var leaf = tree[i].end.copy();
                        leaves.Add(leaf);
                    }
                }
            }
        }

    }

    void OnGUI()
    {
        P5JSExtension.resetMatrix();
        P5JSExtension.background(51);

        for (int i = 0; i < tree.Count; i++)
        {
            tree[i].show();
            //tree[i].jitter();
        }
        for (var i = 0; i < leaves.Count; i++)
        {
            P5JSExtension.fill(255, 0, 100, 100);
            P5JSExtension.noStroke(); //cause error for somereason
            P5JSExtension.ellipse(leaves[i].x, leaves[i].y, 8, 8);
            leaves[i].y += P5JSExtension.random(0, 2);
        }
    }
    class Branch
    {
        public PVector2 begin;
        public PVector2 end;
        public bool finished;
        public Branch(PVector2 begin, PVector2 end)
        {
            this.begin = begin;
            this.end = end;
            this.finished = false;
        }

        public void jitter()
        {
            this.end.x += P5JSExtension.random(-1f, 1f);
            this.end.y += P5JSExtension.random(-1f, 1f);
        }

        public void show()
        {
            P5JSExtension.stroke(255);
            P5JSExtension.line(this.begin.x, this.begin.y, this.end.x, this.end.y);
        }

        public Branch branchA()
        {
            var dir = end - begin;
            dir = dir.rotate(Mathf.PI / 4);
            dir *= 0.67f;
            var newEnd = end + dir;
            var b = new Branch(this.end,newEnd);
            return b;
        }
        public Branch branchB()
        {
            var dir = end - begin;
            dir = dir.rotate(-Mathf.PI / 4);
            dir *= 0.67f;
            var newEnd = end + dir;
            var b = new Branch(this.end,newEnd);
            return b;
        }
    }

    //new class is made, because unity Vector2 is a struct, and struct only gets pass by value and not by refrence
    public class PVector2
    {
        public float x;
        public float y;
        public PVector2(float inx,float iny)
        {
            x = inx;
            y = iny;
        }
        public static PVector2 operator- (PVector2 v1, PVector2 v2)
        {
            return new PVector2(v1.x - v2.x, v1.y - v2.y);
        }
        public static PVector2 operator+ (PVector2 v1, PVector2 v2)
        {
            return new PVector2(v1.x + v2.x, v1.y + v2.y);

        }
        public static PVector2 operator* (PVector2 v1, float v)
        {
            return new PVector2(v1.x * v, v1.y * v);
        }
        public PVector2 copy()
        {
            return new PVector2(x,y);
        }
    }
}
