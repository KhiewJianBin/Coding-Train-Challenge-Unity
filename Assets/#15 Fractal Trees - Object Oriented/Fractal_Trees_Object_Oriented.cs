using System.Collections.Generic;
using UnityEngine;

public class Fractal_Trees_Object_Oriented : MonoBehaviour
{
    List<Branch> tree = new List<Branch>();
    List<PVector> leaves = new List<PVector>();

    int count = 0;

    void Start()
    {
        PVector a = new PVector(P5JSExtension.width / 2, P5JSExtension.height);
        PVector b = new PVector(P5JSExtension.width / 2, P5JSExtension.height - 100);
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
        public PVector begin;
        public PVector end;
        public bool finished;
        public Branch(PVector begin, PVector end)
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

    
}
