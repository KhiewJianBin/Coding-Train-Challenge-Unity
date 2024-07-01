using System.Collections.Generic;
using UnityEngine;
using static P5JSExtension;

public class MitosisSimulation : P5JSBehaviour
{
    List<Cell> cells = new();

    protected override void setup()
    {
        createCanvas(700, 700);
        cells.push(new Cell(null));
        cells.push(new Cell(new Vector2(100, 100)));
    }

    protected override void Update()
    {
        base.Update();

        for (var i = 0; i < cells.Count; i++)
        {
            cells[i].move();
        }
    }

    protected override void draw()
    {
        background(200);
        for (var i = 0; i < cells.Count; i++)
        {
            cells[i].show();
        }
    }

    protected override void mousePressed()
    {
        for (var i = cells.Count - 1; i >= 0; i--)
        {
            if (cells[i].clicked(mouseX,mouseY))
            {
                cells.Add(cells[i].mitosis());
                cells.Add(cells[i].mitosis());
                cells.splice(i, 1);
            }
        }
    }

    public class Cell
    {
        Vector2 pos;
        float r;
        Color32 c;

        public Cell(Vector2? pos = null, float? r = null, Color32? c = null)
        {
            if (pos.HasValue)
            {
                this.pos = pos.Value;
            }
            else
            {

                this.pos = createVector(random(width), random(height));
            }

            this.r = r ?? 60;
            this.c = c ?? new Color32((byte)random(100, 255), 0, (byte)random(100, 255), 100);
        }

        public bool clicked(float x, float y)
        {
            var d = dist(this.pos.x, this.pos.y, x, y);
            if (d < this.r)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Cell mitosis()
        {
            //this.pos.x += random(-this.r, this.r);
            var cell = new Cell(this.pos, this.r * 0.8f, this.c);
            return cell;
        }

        public void move()
        {
            Vector2 vel = random2D();
            this.pos += vel;
        }
        public void show()
        {
            noStroke();
            fill(this.c);
            ellipse(this.pos.x, this.pos.y, this.r, this.r);
        }
    }
}