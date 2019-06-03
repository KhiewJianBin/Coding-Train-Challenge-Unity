using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    GameObject BoxPrefab;
    Vector3 pos;
    float r;

    public void SetPosSize(float x, float y, float z, float inR)
    {
        pos = new Vector3(x, y, z);
        transform.position = pos;
        r = inR;
        transform.localScale = new Vector3(inR, inR, inR);
    }
    public void SetBoxPrefab(GameObject prefab)
    {
        BoxPrefab = prefab;
    }
    public List<Box> Generate()
    {
        List<Box> boxes = new List<Box>();
        for (var x = -1; x < 2; x++)
        {
            for (var y = -1; y < 2; y++)
            {
                for (var z = -1; z < 2; z++)
                {
                    int sum = Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z);
                    float newR = r / 3;

                    if (sum > 1)
                    {
                        GameObject newbox = GameObject.Instantiate(BoxPrefab);
                        Box b = newbox.AddComponent<Box>();
                        b.SetBoxPrefab(BoxPrefab);
                        b.SetPosSize(pos.x + x * newR, pos.y + y * newR, pos.z + z * newR, newR);
                        boxes.Add(b);
                    }
                }
            }
        }
        return boxes;
    }
}