using System.Collections.Generic;
using UnityEngine;

public class Diffusion_Limited_Aggregation : MonoBehaviour
{
    class Walker
    {
        public Vector2 pos;
        public bool stuck;
        public Walker()
        {
            pos = new Vector2(P5JSExtension.random(P5JSExtension.width), P5JSExtension.random(P5JSExtension.height));
            stuck = false;
        }
        public void walk()
        {
            var vel = P5JSExtension.random2D();
            pos += vel;
            pos.x = P5JSExtension.constrain(pos.x, 0, P5JSExtension.width);
            pos.y = P5JSExtension.constrain(pos.y, 0, P5JSExtension.height);
        }
        public void checkStuck()
        {
            for (int i = 0; i < tree.Count; i++)
            {
                var d = P5JSExtension.dist(pos, tree[i]);
                if (d < r * 2)
                {
                    stuck = true;
                    break;
                }
            }
        }
    }

    static List<Vector2> tree = new List<Vector2>();
    static Vector2 walker;
    static float r = 4;
    void Start()
    {
        //400x400
        tree.Add(new Vector2(P5JSExtension.width / 2, P5JSExtension.height / 2));
    }
    void OnGUI()
    {
        P5JSExtension.background(0);

        List<Vector2> walkers = new List<Vector2>();
        for (var i = 0; i < 10; i ++)
        {
            walkers[i] = new Vector2(P5JSExtension.random(P5JSExtension.width), P5JSExtension.random(P5JSExtension.height));
        }

        

        tree.Add(walker);

        for (int i = 0; i < tree.Count; i++)
        {
            P5JSExtension.strokeWeight(r*2);
            P5JSExtension.stroke(255,100);
            P5JSExtension.point(tree[i].x, tree[i].y);
        }
    }
}