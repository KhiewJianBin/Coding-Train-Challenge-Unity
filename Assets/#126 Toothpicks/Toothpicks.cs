using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toothpicks : MonoBehaviour
{
    
    class Toothpick
    {
        int len = 63;

        public int ax, ay, bx, by;
        public int dir;
        public bool newPick = true;

        public Toothpick(int x,int y,int d)
        {
            dir = d;
            if(dir == 1)
            {
                ax = x - len / 2;
                bx = x + len / 2;
                ay = y;
                by = y;
            }
            else
            {
                ax = x;
                bx = x;
                ay = y - len / 2;
                by = y + len / 2;
            }
        }
        public void show()
        {
            P5JSExtension.stroke(0);
            if(newPick)
            {
                P5JSExtension.stroke(0,0,255);
            }
            P5JSExtension.strokeWeight(2);
            P5JSExtension.line(ax, ay, bx, by);
        }

        bool intersects(int x,int y)
        {
            if(ax == x && ay == y)
            {
                return true;
            }
            else if (bx == x && by == y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Toothpick createA(List<Toothpick> others)
        {
            bool available = true;
            foreach (Toothpick other in others)
            {
                if(other != this && other.intersects(ax,ay))
                {
                    available = false;
                }
            }
            if(available)
            {
                return new Toothpick(ax, ay, dir * -1);
            }

            return null;
        }
        public Toothpick createB(List<Toothpick> others)
        {
            bool available = true;
            foreach (Toothpick other in others)
            {
                if (other != this && other.intersects(bx, by))
                {
                    available = false;
                }
            }
            if (available)
            {
                return new Toothpick(bx, by, dir * -1);
            }

            return null;
        }
    }

    int minX;
    int maxX;
    List<Toothpick> picks;
    void Start()
    {
        minX = -P5JSExtension.width / 2;
        maxX = P5JSExtension.width / 2;

        picks = new List<Toothpick>();
        picks.Add(new Toothpick(0, 0, 1));
    }
    void OnGUI()
    {
        P5JSExtension.resetMatrix();
        P5JSExtension.background(255);
        P5JSExtension.translate(P5JSExtension.width / 2, P5JSExtension.height / 2);
        float factor = (float)P5JSExtension.width / (maxX - minX);
        P5JSExtension.scale(factor);
        foreach (Toothpick t in picks)
        {
            t.show();
            minX = Mathf.Min(t.ax, minX);
            maxX = Mathf.Max(t.ax, maxX);
        }

        if (Input.GetMouseButtonDown(0))
        {
            List<Toothpick> next = new List<Toothpick>();
            foreach (Toothpick t in picks)
            {
                if(t.newPick)
                {
                    Toothpick nextA = t.createA(picks);
                    Toothpick nextB = t.createB(picks);
                    if (nextA != null)
                    {
                        next.Add(nextA);
                    }
                    if (nextB != null)
                    {
                        next.Add(nextB);
                    }
                    t.newPick = false;
                }
            }
            picks.AddRange(next);
        }
    }
}