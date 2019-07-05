using UnityEngine;

public class Terrain_Generation_with_Perlin_Noise : MonoBehaviour
{
    int cols, rows;
    int scl = 20;
    int w = 1400;
    int h = 1000;

    float flying = 0;

    float[][] terrain;

    void Start()
    {
        cols = w / scl;
        rows = h / scl;

        terrain = new float[cols][];
        for (int x = 0; x < cols; x++)
        {
            terrain[x] = new float[rows];
            for (int y = 0; y < rows; y++)
            {
                terrain[x][y] = 0; //specify a default value for now
            }
        }
    }

    void Update()
    {
        flying -= 0.1f;
        float yoff = flying;
        for (int y = 0; y < rows; y++)
        {
            float xoff = 0;
            for (int x = 0; x < cols; x++)
            {
                terrain[x][y] = P5JSExtension.map(P5JSExtension.noise(xoff, yoff), 0, 1, -100, 100);
                xoff += 0.2f;
            }
            yoff += 0.2f;
        }

        gameObject.transform.position = new Vector3(-w / 2, -h / 2 + 50, 0);
        gameObject.transform.rotation = Quaternion.Euler(180 / 3, 0, 0);


        for (int y = 0; y < rows - 1; y++)
        {
            P5JSExtension.beginShape(MeshTopology.LineStrip);
            for (int x = 0; x < cols; x++)
            {
                P5JSExtension.vertex(x * scl, y * scl, terrain[x][y]);
                P5JSExtension.vertex(x * scl, (y + 1) * scl, terrain[x][y + 1]);
            }
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();
    }
}