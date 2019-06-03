using UnityEngine;

public class TheSnakeGame : MonoBehaviour
{
    //-------------------------------------------------
    public static int width;
    public static int height;
    //-------------------------------------------------

    public Texture2D snakeTexture;
    Snake s;

    //float s;
    public static float scl = 20;
    public Texture2D foodTexture;
    Vector2 food;

    void Awake()
    {
        width = Screen.width;
        height = Screen.height;
    }
    void Start()
    {
        s = new Snake();
        Application.targetFrameRate = 10;
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
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color32(51, 51, 51, 255);

        s.Show(snakeTexture);

        Rect position = new Rect(food.x, food.y, scl, scl);
        GUI.DrawTexture(position, foodTexture,ScaleMode.StretchToFill,false,0,new Color32(255,0,100,255),0,2);
    }
    void pickLocation()
    {
        int cols = Mathf.FloorToInt(width / scl);
        int rows = Mathf.FloorToInt(height / scl);
        food = new Vector2(Random.Range(0, cols), Random.Range(0, rows));
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