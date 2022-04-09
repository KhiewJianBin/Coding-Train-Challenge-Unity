using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Binary_Search_Tree : MonoBehaviour
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
                this.root = n;
                this.root.x = P5JSExtension.width / 2;
                this.root.y = 16;
            }
            else
            {
                root.addNode(n);
            }
        }
        public void traverse()
        {
            this.root.visit(this.root);
        }
        public Node search(int val)
        {
            var found = this.root.search(val);
            return found;
        }
    }
    class Node
    {
        public int value;
        Node left = null;
        Node right = null;
        public float x = -100;
        public float y = -100;
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
                    this.left = n;
                    this.left.x = this.x - 50;
                    this.left.y = this.y + 20;
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
                    this.right.x = this.x + 50;
                    this.right.y = this.y + 20;
                }
                else
                {
                    right.addNode(n);

                }
            }
        }
        public void visit(Node parent)
        {
            if(this.left != null)
            {
                this.left.visit(this);
            }
            P5JSExtension.fill(255);
            P5JSExtension.noStroke();
            P5JSExtension.text(this.value, this.x, this.y);
            P5JSExtension.stroke(255);
            P5JSExtension.noFill();
            P5JSExtension.ellipse(this.x, this.y, 20, 20);
            P5JSExtension.line(parent.x, parent.y, this.x, this.y);
            if (this.right != null)
            {
                this.right.visit(this);
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
        for(var i = 0;i < 100;i++)
        {
            tree.addValue(Mathf.FloorToInt(P5JSExtension.random(0,100)));
        }

        
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
    void OnGUI()
    {
        tree.traverse();
    }
}