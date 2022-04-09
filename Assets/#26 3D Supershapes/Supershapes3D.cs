using UnityEngine;

public class Supershapes3D : MonoBehaviour
{
    Vector3[][] globe;
    int total = 75;
    float m = 0;

    float offset = 0;

    float mchange = 0;
    void Start()
    {
        globe = new Vector3[total+1][];
        for(int i = 0; i < total + 1; i++)
        {
            globe[i] = new Vector3[total + 1];
        }
    }
    float a = 1;
    float b = 1;
    float supershape(float theta, float m, float n1, float n2, float n3)
    {
        float t1 = Mathf.Abs((1 / a) * Mathf.Cos(m * theta / 4));
        t1 = Mathf.Pow(t1, n2);
        float t2 = Mathf.Abs((1 / b) * Mathf.Sin(m * theta / 4));
        t2 = Mathf.Pow(t2, n3);
        float t3 = t1 + t2;
        float r = Mathf.Pow(t3, -1 / n1);
        return r;
    }

    void OnGUI()
    {
        m = P5JSExtension.map(Mathf.Sin(mchange), -1, 1, 0, 7);
        mchange += 0.02f;

        //P5JSExtension.background(0);
        float r = 200;
        for (int i = 0;i < total + 1; i++)
        {
            float lat = P5JSExtension.map(i, 0, total, -Mathf.PI/2, Mathf.PI/2);
            float r2 = supershape(lat, m, 0.2f, 1.7f, 1.7f);
            for (int j = 0; j < total + 1; j++)
            {
                float lon = P5JSExtension.map(j, 0, total, -Mathf.PI, Mathf.PI);
                float r1 = supershape(lon, m, 0.2f, 1.7f, 1.7f);
                float x = r * r1 * Mathf.Cos(lon) * r2 * Mathf.Cos(lat);
                float y = r * r1 * Mathf.Sin(lon) * r2 * Mathf.Cos(lat);
                float z = r * r2 * Mathf.Sin(lat);
                globe[i][j] = new Vector3(x, y, z);
            }
        }

        P5JSExtension.resetShape();
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