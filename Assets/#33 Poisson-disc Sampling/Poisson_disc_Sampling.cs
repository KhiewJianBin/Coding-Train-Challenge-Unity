using System.Collections.Generic;
using UnityEngine;

public class Poisson_disc_Sampling : MonoBehaviour
{
    float r = 4;
    int k = 30;
    Vector2[] grid;
    float w;
    List<Vector2> active;
    int cols, rows;
    List<Vector2> ordered;

    void Start()
    {
        //createCanvas(400,400);
        P5JSExtension.background(0);
        P5JSExtension.strokeWeight(4);
        P5JSExtension.colorMode(P5JSExtension.HSB);

        //step 0
        w = r / Mathf.Sqrt(2);
        cols = Mathf.FloorToInt(P5JSExtension.width / 2);
        rows = Mathf.FloorToInt(P5JSExtension.height / 2);
        grid = new Vector2[cols * rows];
        active = new List<Vector2>();
        ordered = new List<Vector2>();
        for (var ii = 0; ii < cols*rows; ii++)
        {
            grid[ii] = new Vector2(-1,-1);
        }

        //step 1
        var x = P5JSExtension.width/2;
        var y = P5JSExtension.height/2;
        var i = Mathf.FloorToInt(x / w);
        var j = Mathf.FloorToInt(y / w);
        var pos = new Vector2(x, y);
        grid[i + j * cols] = pos;
        active.Add(pos);


    }
    void OnGUI()
    {
        P5JSExtension.background(0);

        for (int total = 0; total < 25; total++)
        {
            if (active.Count > 0)
            {
                var randIndex = Mathf.FloorToInt(P5JSExtension.random(active.Count));
                var pos = active[randIndex];
                var found = false;
                for (var n = 0; n < k; n++)
                {
                    var sample = P5JSExtension.random2D();
                    var m = P5JSExtension.random(r, 2 * r);
                    sample = sample.setMag(m);
                    sample += pos;

                    int col = Mathf.FloorToInt(sample.x / w);
                    int row = Mathf.FloorToInt(sample.y / w);
                    if (col > -1 && row > -1 && col < cols && row < rows && grid[col + row * cols] == new Vector2(-1, -1))
                    {
                        var ok = true;
                        for (var i = -1; i <= 1; i++)
                        {
                            for (var j = -1; j <= 1; j++)
                            {
                                try
                                {
                                    var index = (cols + i) + (rows + j) * cols;
                                    var neighbor = grid[index];
                                    if (neighbor != new Vector2(-1, -1))
                                    {
                                        var d = P5JSExtension.dist(sample, neighbor);
                                        if (d < r)
                                        {
                                            ok = false;
                                        }
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                        if (ok)
                        {
                            found = true;
                            grid[col + row * cols] = sample;
                            active.Add(sample);
                            ordered.Add(sample);
                            // Should we break?
                            break;
                        }
                    }
                }
                if (!found)
                {
                    //active.RemoveAt(randIndex);
                }
            }
        }
        for (int i = 0; i < ordered.Count; i++)
        {
            P5JSExtension.stroke((byte)(i % 360), 255, 255);
            P5JSExtension.strokeWeight(r * 0.5f);
            P5JSExtension.point(ordered[i].x, ordered[i].y);
        }

        //for (var i = 0; i < active.Count; i++)
        //{
        //    P5JSExtension.stroke(255,0,255);
        //    P5JSExtension.strokeWeight(4);
        //    P5JSExtension.point(active[i].x, active[i].y);
        //}
    }
}