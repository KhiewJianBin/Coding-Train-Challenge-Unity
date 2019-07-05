using System.Collections.Generic;
using UnityEngine;

// cant follow exactly 
public class _3D_Fractal_Trees: MonoBehaviour
{
    Tree tree;
    public static float max_dist = 100;
    public static float min_dist = 10;

    public static MeshFilter meshfilter;
    void Start()
    {
        tree = new Tree();

        meshfilter = gameObject.GetComponent<MeshFilter>();
        //P5JSExtension.frameRate(5);
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
        Vector3 pos;
        Vector3 dir;
        Branch root;
        public Tree()
        {
            leaves = new List<Leaf>();
            branches = new List<Branch>();

            for (var i = 0; i < 100; i++)
            {
                this.leaves.Add(new Leaf());
            }

            pos = new Vector3(0, 0, 0);
            dir = new Vector2(0, -1);
            root = new Branch(null, pos, dir);
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
            for (var i = leaves.Count - 1; i >= 0; i--)
            {
                if (leaves[i].reached)
                {
                    leaves.RemoveAt(i);
                }
            }
            for (var i = branches.Count - 1; i >= 0; i--)
            {
                var branch = branches[i];
                if (branch.count > 0)
                {
                    branch.dir /= branch.count + 1;
                    Vector3 rand = P5JSExtension.random3D();
                    rand.setMag(0.3f);
                    branch.dir += rand;
                    this.branches.Add(branch.next());
                    branch.reset();
                }
            }
        }

        public void show()
        {
            for (var i = 0; i < leaves.Count; i++)
            {
                //leaves[i].show();
            }
            P5JSExtension.beginShape(MeshTopology.Lines);
            for (var i = 0; i < branches.Count; i++)
            {
                Branch b = branches[i];
                if (b.parent != null)
                {
                    P5JSExtension.vertex(b.pos.x, b.pos.y, b.pos.z);
                    P5JSExtension.vertex(b.parent.pos.x, b.parent.pos.y, b.parent.pos.z);
                }
            }
            meshfilter.mesh = P5JSExtension.endShape();
        }
    }
    public class Leaf
    {
        public Vector3 pos;
        public bool reached;
        public Leaf()
        {
            pos = P5JSExtension.random3D();
            pos *= P5JSExtension.random(P5JSExtension.width / 2);
            this.reached = false;
        }

        public void show()
        {
            P5JSExtension.ellipse(this.pos.x, this.pos.y, 4, 4);
        }
    }
    public class Branch
    {
        public Vector3 pos;
        public Branch parent;
        public Vector3 dir;
        public Vector3 origDir;
        public int count;
        public int len;
        public Branch(Branch parent, Vector3 pos, Vector3 dir)
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
            if (parent != null)
            {
                P5JSExtension.stroke(255);
                P5JSExtension.line(this.pos.x, this.pos.y, this.parent.pos.x, this.parent.pos.y);
            }
        }
    }
}