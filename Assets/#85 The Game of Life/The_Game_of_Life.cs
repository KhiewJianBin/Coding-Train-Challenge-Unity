using System.Collections.Generic;
using UnityEngine;

public class The_Game_of_Life : MonoBehaviour
{
    int[][] make2DArray(int cols,int rows)
    {
        int[][] arr = new int[cols][];
        for(int i = 0; i < arr.Length;i++)
        {
            arr[i] = new int[rows];
        }
        return arr;
    }

    int[][] grid;
    int cols;
    int rows;
    int resolution = 10;

    void Start()
    {
        //600x400
        cols = P5JSExtension.width / resolution;
        rows = P5JSExtension.height / resolution;

        grid = make2DArray(cols, rows);
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                grid[i][j] = Mathf.FloorToInt(P5JSExtension.random(2f));
            }
        }
    }
    void OnGUI()
    {
        P5JSExtension.background(0);

        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                int x = i * resolution;
                int y = j * resolution;
                if(grid[i][j] == 1)
                {
                    P5JSExtension.fill(255);
                    P5JSExtension.stroke(0);
                    P5JSExtension.rect(x, y, resolution-1, resolution-1);
                }
            }
        }



        var next = make2DArray(cols, rows);

        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var state = grid[i][j];
               
                var sum = 0;
                var neighbors = countNeighbors(grid, i, j);

                if (state == 0 && neighbors == 3)
                {
                    next[i][j] = 1;
                }
                else if (state == 1 && (neighbors < 2 || neighbors > 3))
                {
                    next[i][j] = 0;
                }
                else
                {
                    next[i][j] = state;
                }
            }
        }
        grid = next;
    }
    int countNeighbors(int [][] grid,int x, int y)
    {
        var sum = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                var col = (x + i + cols) % cols;
                var row = (y + j + rows) % rows;

                sum += grid[col][row];
            }
        }
        sum -= grid[x][y];
        return sum;
    }
}