using UnityEngine;
using static P5JSExtension;

public class P5JSBehaviour : MonoBehaviour
{
    void Start()
    {
        frameRate(60);

        setup();
    }

    void OnGUI()
    {
        resetMatrix();

        draw();
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressed();
        }

        keyReleased();
        keyPressed();
    }

    protected virtual void setup() { }
    protected virtual void draw() { }
    protected virtual void mousePressed() { }
    protected virtual void keyPressed() { }
    protected virtual void keyReleased() { }

}