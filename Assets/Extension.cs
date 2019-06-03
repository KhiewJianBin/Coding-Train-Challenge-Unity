using UnityEngine;

public class Extension : MonoBehaviour
{
    static public float Map
        (float value,
        float start1,float stop1,
        float start2,float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }
}