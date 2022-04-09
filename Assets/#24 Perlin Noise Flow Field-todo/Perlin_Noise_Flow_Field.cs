using UnityEngine;

public class Perlin_Noise_Flow_Field : MonoBehaviour
{
    float inc = 0.1f;
    float scl = 10f;
    int cols, rows;

    float zoff = 0;


    void Start()
    {  
        cols = Mathf.FloorToInt(P5JSExtension.width / scl);
        rows = Mathf.FloorToInt(P5JSExtension.height / scl);
    }
    void OnGUI()
    {
        var yoff = 0f;
        for (var y = 0; y < rows; y++)
        {
            var xoff = 0f;
            for (var x = 0; x < cols; x++)
            {
                var angle = P5JSExtension.noise(P5JSExtension.noise(xoff,yoff),zoff)* Mathf.PI * 2;
                var v = P5JSExtension.fromAngle(angle);
                xoff += inc;
                P5JSExtension.stroke(0);
                P5JSExtension.push();
                P5JSExtension.translate(x * scl, y * scl);
                P5JSExtension.rotate(Vector2.Angle(v,Vector2.right));
                P5JSExtension.line(0, 0, scl, 0);

                P5JSExtension.pop();
            }
            yoff += inc;

            zoff += 0.001f;
        }
    }
}