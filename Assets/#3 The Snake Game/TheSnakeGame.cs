using UnityEngine;

public class TheSnakeGame : MonoBehaviour
{
    Snake s;
    public static float scl = 20;
    Vector2 food;

    void Start()
    {
        s = new Snake();
        P5JSExtension.frameRate(10);
        pickLocation();
    }
    void Update()
    {
        KeyPressed();
        s.death();
        s.Update();

        if (s.eat(food))
        {
            pickLocation();
        };

        if(Input.GetMouseButtonDown(0))
        {
            s.total++;
        }
    }
    void OnGUI()
    {
        P5JSExtension.background(51);

        s.Show();

        P5JSExtension.fill(255, 0, 100);
        P5JSExtension.rect(food.x, food.y, scl, scl);
    }
    void pickLocation()
    {
        int cols = P5JSExtension.floor(P5JSExtension.width / scl);
        int rows = P5JSExtension.floor(P5JSExtension.height / scl);
        food = new Vector2(P5JSExtension.random(0, cols), P5JSExtension.random(0, rows));
        food.Scale(new Vector2(scl, scl));
    }
    void KeyPressed()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            s.dir(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            s.dir(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            s.dir(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            s.dir(-1, 0);
        }
    }
}