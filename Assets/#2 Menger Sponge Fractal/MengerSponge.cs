using System.Collections.Generic;
using UnityEngine;

public class MengerSponge : MonoBehaviour
{
    GameObject group;

    public GameObject BoxPrefab;
    List<Box> sponge = new List<Box>();

    void Start()
    {
        group = new GameObject("Group");

        GameObject newbox = Instantiate(BoxPrefab);
        Box b = newbox.AddComponent<Box>();
        b.SetBoxPrefab(BoxPrefab);
        b.SetPosSize(0, 0, 0, 200);
        sponge.Add(b);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            List<Box> next = new List<Box>();
            foreach (Box b in sponge)
            {
                next.AddRange(b.Generate());
                Destroy(b.gameObject);
            }

            sponge = next;
        }
    }
}