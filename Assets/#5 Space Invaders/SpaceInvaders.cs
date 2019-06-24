using UnityEngine;
using System.Collections.Generic;


public class SpaceInvaders : MonoBehaviour
{
    Ship ship;
    List<Flower> flowers = new List<Flower>();
    List<Drop2> drops = new List<Drop2>();

    void Start()
    {
        ship = new Ship();
        for (var i = 0; i < 6; i++)
        {
            flowers.Add(new Flower(i * 80 + 80, 60));
        }
    }

    void Update()
    {
        keyPressed();
        keyReleased();
    }
    void keyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Drop2 drop = new Drop2(ship.x, P5JSExtension.height);
            drops.Add(drop);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ship.setDir(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ship.setDir(-1);
        }
        
    }
    void keyReleased()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ship.setDir(0);
        }
    }

    void OnGUI()
    {
        P5JSExtension.background(51);

        ship.show();
        ship.move();

        for (var i = 0; i < drops.Count; i++)
        {
            drops[i].show();
            drops[i].move();
            for (var j = 0; j < flowers.Count; j++)
            {
                if (drops[i].hits(flowers[j]))
                {
                    flowers[j].grow();
                    drops[i].evaporate();
                }
            }
        }

        var edge = false;

        for (var i = 0; i < flowers.Count; i++)
        {
            flowers[i].show();
            flowers[i].move();
            if (flowers[i].x > P5JSExtension.width || flowers[i].x < 0)
            {
                edge = true;
            }
        }

        if (edge)
        {
            for (var i = 0; i < flowers.Count; i++)
            {
                flowers[i].shiftDown();
            }
        }

        for (var i = drops.Count - 1; i >= 0; i--)
        {
            if (drops[i].toDelete)
            {
                drops.RemoveAt(i);
            }
        }
    }
}
