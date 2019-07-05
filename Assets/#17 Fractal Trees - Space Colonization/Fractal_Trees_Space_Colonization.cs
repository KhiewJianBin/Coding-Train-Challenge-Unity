using System.Collections.Generic;
using UnityEngine;

public class Fractal_Trees_Space_Colonization : MonoBehaviour
{
    Tree tree;
    public static float max_dist = 100;
    public static float min_dist = 10;

    void Start()
    {
        tree = new Tree();
    }
    
    void OnGUI()
    {
        P5JSExtension.background(51);
        tree.show();
        tree.grow();
    }
    public class Tree
    {
        List<Leaf> leaves;
        List<Branch> branches;
        Vector2 pos;
        Vector2 dir;
        Branch root;
        public Tree()
        {
            leaves = new List<Leaf>();
            branches = new List<Branch>();

            for (var i = 0; i < 1500; i++)
            {
                this.leaves.Add(new Leaf());
            }

            pos = new Vector2(P5JSExtension.width / 2, P5JSExtension.height);
            dir = new Vector2(0, -1);
            root = new Branch(null,pos,dir);
            branches.Add(root);
            var current = root;
            var found = false;
            while (!found)
            {
                for (var i = 0; i < leaves.Count; i++)
                {
                    var d = P5JSExtension.dist(current.pos, leaves[i].pos);
                    if (d < Fractal_Trees_Space_Colonization.max_dist)
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    var branch = current.next();
                    current = branch;
                    branches.Add(current);
                }
            }
        }

        public void grow()
        {
            for (var i = 0; i < leaves.Count; i++)
            {
                var leaf = leaves[i];
                Branch closestBranch = null;
                var record = Fractal_Trees_Space_Colonization.max_dist;
                for (var j = 0; j < branches.Count; j++)
                {
                    var branch = branches[j];
                    var d = P5JSExtension.dist(leaf.pos, branch.pos);
                    if (d < Fractal_Trees_Space_Colonization.min_dist)
                    {
                        leaf.reached = true;
                        closestBranch = null;
                        break;
                    }
                    else if (d < record)
                    {
                        closestBranch = branch;
                        record = d;
                    }
                }
                if (closestBranch != null)
                {
                    var newDir = leaf.pos - closestBranch.pos;
                    newDir.Normalize();
                    closestBranch.dir += newDir;
                    closestBranch.count++;
                }
            }
            for (var i = leaves.Count-1; i >= 0; i--)
            {
                if(leaves[i].reached)
                {
                    leaves.RemoveAt(i);
                }
            }
            for (var i = branches.Count - 1; i >= 0; i--)
            {
                var branch = branches[i];
                if (branch.count > 0)
                {
                    branch.dir /= branch.count+1;
                    this.branches.Add(branch.next());
                    branch.reset();
                }
            }
        }
    
        public void show()
        {
            for (var i = 0; i < leaves.Count; i++)
            {
                leaves[i].show();
            }
            for (var i = 0; i < branches.Count; i++)
            {
                branches[i].show();
            }
        }
    }
    public class Leaf
    {
        public Vector2 pos;
        public bool reached;
        public Leaf()
        {
            this.pos = new Vector2(P5JSExtension.random(P5JSExtension.width), P5JSExtension.random(P5JSExtension.height-100));
            this.reached = false;
        }

        public void show()
        {
            P5JSExtension.ellipse(this.pos.x, this.pos.y, 4, 4);
        }
    }
    public class Branch
    {
        public Vector2 pos;
        Branch parent;
        public Vector2 dir;
        public Vector2 origDir; 
        public int count;
        public int len;
        public Branch(Branch parent,Vector2 pos,Vector2 dir)
        {
            this.pos = pos;
            this.parent = parent;
            this.dir = dir;
            this.origDir = dir;
            this.count = 0;
            this.len = 5;
        }


        public void reset()
        {
            this.dir = origDir;
            this.count = 0;
        }

        public Branch next()
        {
            var nextDir = this.dir * this.len;
            var nextPos = this.pos + nextDir;
            var nextBranch = new Branch(this, nextPos, this.dir);
            return nextBranch;
        }

        public void show()
        {
            if(parent != null)
            {
                P5JSExtension.stroke(255);
                P5JSExtension.line(this.pos.x, this.pos.y, this.parent.pos.x, this.parent.pos.y);
            }
        }
    }
}
