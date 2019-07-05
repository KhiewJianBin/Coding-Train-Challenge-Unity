using System.Collections.Generic;
using UnityEngine;

//Not exact, but results similar
public class The_Lorenz_Attractor : MonoBehaviour
{
    float x = 0.01f;
    float y;
    float z;

    float a = 10;
    float b = 28;
    float c = 8f / 3f;

    List<Vector3> points = new List<Vector3>();

    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",Color.white);
    }

    void Update()
    {
        //Note not using background clear
        float dt = 0.01f;
        float dx = (a * (y - x)) * dt;
        float dy = (x * (b - z) - y) * dt;
        float dz = (x * y - c * z) * dt;
        x = x + dx;
        y = y + dy;
        z = z + dz;

        points.Add(new Vector3(x, y, z));

        //Note Could not add Hue change overtime,
        P5JSExtension.beginShape(MeshTopology.LineStrip);
        foreach(Vector3 v in points)
        {
            Vector3 offset = P5JSExtension.random3D();
            offset*=0.1f;
            Vector3 a = v + offset;
            P5JSExtension.vertex(a.x,a.y,a.z);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();
    }
}