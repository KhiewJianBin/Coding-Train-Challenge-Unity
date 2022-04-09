using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Generator_Part_1 : MonoBehaviour
{ 
    public class Cell
    {
        int i;
        int j;
        bool[] walls;
        public Cell(int i,int j)
        {
            this.i = i;
            this.j = j;
            walls = new bool[] { true, true, true, true };
        }
        public void show()
        {
            var x = this.i * w;
            var y = this.j * w;
            P5JSExtension.stroke(255);
            if(walls[0])
            {
                P5JSExtension.line(x, y, x + w, y);
            }
            if (walls[1])
            {
                P5JSExtension.line(x + w, y, x + w, y + w);
            }
            if (walls[2])
            {
                P5JSExtension.line(x + w, y + w, x, y + w);
            }
            if (walls[3])
            {
                P5JSExtension.line(x, y + w, x, y);
            }
        }
    }
    int cols, rows;
    static int w = 40;
    List<Cell> grid = new List<Cell>();

    void Start()
    {
        //600x600
        cols = Mathf.FloorToInt(P5JSExtension.width / w);
        rows = Mathf.FloorToInt(P5JSExtension.height / w);

        for(int j = 0;j<rows;j++)
        {
            for (int i = 0; i < cols; i++)
            {
                var cell = new Cell(i, j);
                grid.Add(cell);
            }
        }
    }

    void OnGUI()
    {
        P5JSExtension.background(51);
        for(int i = 0; i < grid.Count;i++)
        {
            grid[i].show();
        }
    }

}
