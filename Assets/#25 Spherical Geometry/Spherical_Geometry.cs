using UnityEngine;

public class Spherical_Geometry : MonoBehaviour
{
    Vector3[][] globe;
    int total = 200;
    void Start()
    {
        globe = new Vector3[total+1][];
        for(int i = 0; i < total + 1; i++)
        {
            globe[i] = new Vector3[total + 1];
        }
    }

    void OnGUI()
    {
        //P5JSExtension.background(0);
        float r = 200;
        for (int i = 0;i < total + 1; i++)
        {
            float lat = P5JSExtension.map(i, 0, total, 0, Mathf.PI);
            for (int j = 0; j < total + 1; j++)
            {
                float lon = P5JSExtension.map(j, 0, total, 0, 2*Mathf.PI);

                float x = r * Mathf.Sin(lat) * Mathf.Cos(lon);
                float y = r * Mathf.Sin(lat) * Mathf.Sin(lon);
                float z = r * Mathf.Cos(lat);
                globe[i][j] = new Vector3(x, y, z);
            }
        }

        P5JSExtension.beginShape(MeshTopology.Triangles);
        for (int i = 0; i < total; i++)
        {
            for (int j = 0; j < total + 1; j++)
            {
                Vector3 v1 = globe[i][j];
                P5JSExtension.vertex(v1.x, v1.y, v1.z);
                Vector3 v2 = globe[i + 1][j];
                P5JSExtension.vertex(v2.x, v2.y, v2.z);
            }
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();

    }
}