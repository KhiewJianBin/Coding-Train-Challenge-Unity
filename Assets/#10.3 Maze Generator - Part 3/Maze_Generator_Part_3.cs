using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Generator_Part_3 : MonoBehaviour
{ 
    public class Cell
    {
        public int i;
        public int j;
        public bool[] walls;
        public bool visited;

        public Cell(int i,int j)
        {
            this.i = i;
            this.j = j;
            walls = new bool[] { true, true, true, true };
            this.visited = false;
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

            if(this.visited)
            {
                P5JSExtension.noStroke();
                P5JSExtension.fill(255, 0, 255, 100);
                P5JSExtension.rect(x, y, w, w);
            }
        }
        public int index(int i,int j)
        {
            if(i<0 || j < 0 || i > cols-1 || j > rows - 1)
            {
                return -1;
            }
            return i + j * cols;
        }
        public Cell checkNeighbors()
        {
            List<Cell> neightbors = new List<Cell>();

            Cell top = null;
            Cell right = null;
            Cell bottom = null;
            Cell left = null;
            try
            {
                top = grid[index(i, j - 1)];
            }
            catch{}
            try
            {
                right = grid[index(i + 1, j)];
            }
            catch { }
            try
            {
                bottom = grid[index(i, j + 1)];
            }
            catch { }
            try
            {
                left = grid[index(i - 1, j)];
            }
            catch { }


            if (top != null && !top.visited)
            {
                neightbors.Add(top);
            }
            if (right != null && !right.visited)
            {
                neightbors.Add(right);
            }
            if (bottom != null && !bottom.visited)
            {
                neightbors.Add(bottom);
            }
            if (left != null && !left.visited)
            {
                neightbors.Add(left);
            }

            if(neightbors.Count > 0)
            {
                var r = Mathf.FloorToInt(P5JSExtension.random(0, neightbors.Count));
                return neightbors[r];
            }
            else
            {
                return null;
            }
        }
        public void highlight()
        {
            var x = this.i * w;
            var y = this.j * w;
            P5JSExtension.noStroke();
            P5JSExtension.fill(0, 0, 255, 100);
            P5JSExtension.rect(x, y, w, w);
        }
    }
    static int cols, rows;
    static int w = 40;
    static List<Cell> grid = new List<Cell>();

    Cell current;

    Stack<Cell> stack = new Stack<Cell>();

    void Start()
    {
        //400x400
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
        current = grid[0];
    }

    void OnGUI()
    {
        P5JSExtension.background(51);
        for(int i = 0; i < grid.Count;i++)
        {
            grid[i].show();
        }

        current.visited = true;
        current.highlight();

        //step 1
        var next = current.checkNeighbors();
        if(next != null)
        {
            next.visited = true;
        
            //step2
            stack.Push(current);

            //step 3
            removeWalls(current,next);

            //step 4
            current = next;
        }
        else if(stack.Count > 0)
        {
            var cell = stack.Pop();
            current = cell;
        }
    }
    void removeWalls(Cell a,Cell b)
    {
        var x = a.i - b.i;
        if(x == 1)
        {
            a.walls[3] = false;
            b.walls[1] = false;
        }
        else if(x == -1)
        {
            a.walls[1] = false;
            b.walls[3] = false;
        }

        var y = a.j - b.j;
        if (y == 1)
        {
            a.walls[0] = false;
            b.walls[2] = false;
        }
        else if (y == -1)
        {
            a.walls[2] = false;
            b.walls[0] = false;
        }
    }
}
