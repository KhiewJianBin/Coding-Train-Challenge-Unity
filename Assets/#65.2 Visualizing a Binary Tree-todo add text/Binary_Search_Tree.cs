using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visualizing_a_Binary_Tree : MonoBehaviour
{
    class Tree
    {
        Node root = null;
        public Tree()
        {
        }
        public void addValue(int val)
        {
            var n = new Node(val);
            if(root == null)
            {
                root = n;
                root.x = P5JSExtension.width/2;
                root.y = 16;
            }
            else
            {
                root.addNode(n);
            }
        }
        public void traverse()
        {
            this.root.visit();
        }
        public Node search(int val)
        {
            var found = this.root.search(val);
            return found;
        }
    }
    class Node
    {
        int value;
        Node left = null;
        Node right = null;
        public float x;
        public float y;
        public Node(int val)
        {
            value = val;
        }
        public void addNode(Node n)
        {
            if(n.value<this.value)
            {
                if(this.left == null)
                {
                    left = n;
                }
                else
                {
                    left.addNode(n);
                }
            }
            else if(n.value > this.value)
            {
                if (this.right == null)
                {
                    right = n;
                }
                else
                {
                    right.addNode(n);

                }
            }
        }
        public void visit()
        {
            if(this.left != null)
            {
                this.left.visit();
            }
            if (this.right != null)
            {
                this.right.visit();
            }
        }
        public Node search(int val)
        {
            if(this.value == val)
            {
                return this;
            }
            else if (val < this.value && this.left != null)
            {
                return this.left.search(val);
            }
            else if (val < this.value && this.left != null)
            {
                return this.right.search(val);
            }
            return null;
        }
    }
    Tree tree;
    void Start()
    {
        tree = new Tree();
        for(var i = 0;i < 10;i++)
        {
            tree.addValue(Mathf.FloorToInt(P5JSExtension.random(0,100)));
        }

        tree.traverse();
        var result = tree.search(20);
        if(result == null)
        {
            print("not found");
        }
        else
        {
            print(result);
        }
    }
}