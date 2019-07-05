using System.Collections.Generic;
using UnityEngine;

public class MitosisSimulation : MonoBehaviour
{
    Cell cell;
    List<Cell> cells = new List<Cell>();
    void Start()
    {
        cells.Add(new Cell(null));
        cells.Add(new Cell(new Vector2(100,100)));
    }

    void OnGUI()
    {
        P5JSExtension.background(200);
        for (var i = 0; i < cells.Count; i++)
        {
            cells[i].move();
            cells[i].show();
        }
    }
    void Update()
    {
        mousePressed();
    }
    void mousePressed()
    {
        if(Input.GetMouseButtonDown(0))
        {
            for (var i = cells.Count - 1; i >= 0; i--)
            {
                if (cells[i].clicked(Input.mousePosition.x, P5JSExtension.height - Input.mousePosition.y))
                {
                    cells.Add(cells[i].mitosis());
                    cells.Add(cells[i].mitosis());
                    cells.RemoveAt(i);
                }
            }
        }
    }
}