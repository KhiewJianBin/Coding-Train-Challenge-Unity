using UnityEngine;

public class PurpleRain : MonoBehaviour
{
    Drop[] drops = new Drop[500];
    void Start()
    {
        for (int i = 0; i < drops.Length; i++)
        {
            drops[i] = new Drop();
        }
    }

    void OnGUI()
    {
        P5JSExtension.background(230, 230, 250);
        for (int i = 0; i < drops.Length; i++)
        {
            drops[i].fall();
            drops[i].show();
        }
    }
}
