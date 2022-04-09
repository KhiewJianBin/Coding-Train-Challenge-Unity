using System.Collections.Generic;
using UnityEngine;

public class _3D_Knots : MonoBehaviour
{
    float beta = 0;

    float angle = 0;

    List<Vector3> vectors = new List<Vector3>();
    void OnGUI()
    {
        P5JSExtension.background(0);
        P5JSExtension.resetMatrix();
        //P5JSExtension.translate(P5JSExtension.width / 2, P5JSExtension.height / 2);
        //P5JSExtension.rotateY(angle);
        Camera.main.transform.eulerAngles = Vector3.up * angle;
        angle += 0.03f;

        
        float r = 100*(0.8f + 1.6f * Mathf.Sin(6 * beta));
        float theta = 2 * beta;
        float phi = 0.6f * Mathf.PI * Mathf.Sin(12 * beta);

        float x = r * Mathf.Cos(phi) * Mathf.Cos(theta);
        float y = r * Mathf.Cos(phi) * Mathf.Sin(theta);
        float z = r * Mathf.Sin(phi);


        vectors.Add(new Vector3(x, y, z));


        beta += 0.01f;


        P5JSExtension.noFill();
        P5JSExtension.stroke(255);
        P5JSExtension.strokeWeight(8);
        P5JSExtension.resetShape();
        P5JSExtension.beginShape();
        foreach (Vector3 v in vectors)
        {
            float d = v.magnitude;
            P5JSExtension.stroke(255, (byte)d, 255);
            P5JSExtension.vertex(v.x, v.y, v.z);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();
    }
}