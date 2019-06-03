using UnityEngine;

public class PurpleRain : MonoBehaviour
{
    //-------------------------------------------------
    public static int width;
    public static int height;
    //-------------------------------------------------

    Drop[] drops = new Drop[500];
    void Awake()
    {
        width = Screen.width;
        height = Screen.height;
    }
    void Start()
    {
        for (int i = 0; i < drops.Length; i++)
        {
            drops[i] = new Drop();
        }
    }

    void OnGUI()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color32(230, 230, 250,255);
        for (int i = 0; i < drops.Length; i++)
        {
            drops[i].fall();
            drops[i].show();
        }
    }
}
