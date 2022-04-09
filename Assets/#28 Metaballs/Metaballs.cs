using UnityEngine;

public class Metaballs : MonoBehaviour
{
    Blob[] blobs = new Blob[2];
    Texture2D texture;
    void Start()
    {
        texture = new Texture2D(640, 360);
        for (int i = 0; i < blobs.Length; i++)
        {
            blobs[i] = new Blob(P5JSExtension.random(P5JSExtension.width), P5JSExtension.random(P5JSExtension.height));
        }
    }
    void OnGUI()
    {
        for (int x = 0; x < P5JSExtension.width; x++)
        {
            for (int y = 0; y < P5JSExtension.height; y++)
            {
                float sum = 0;
                foreach (Blob b in blobs)
                {
                    float d = P5JSExtension.dist(x, y, b.pos.x, b.pos.y);
                    sum += 300 * blobs[0].r / d;
                }
                
                texture.SetPixel(x, P5JSExtension.height - y, new Color32((byte)sum, (byte)sum, (byte)sum, 255));
            }
        }
        texture.Apply();

        if (Event.current.type.Equals(EventType.Repaint))
        {
            Graphics.DrawTexture(new Rect(0, 0, texture.width, texture.height), texture);
        }

        foreach (Blob b in blobs)
        {
            //b.show();
            b.update();
        }
    }


    class Blob
    {
        public Vector2 pos;
        public float r;
        public Vector2 vel;

        public Blob(float x,float y)
        {
            pos = new Vector2(x, y);
            vel = P5JSExtension.random2D();
            vel *= P5JSExtension.random(2f, 5f);
            r = 40;
        }
        public void update()
        {
            pos += (vel);

            if(pos.x > P5JSExtension.width ||pos.x < 0)
            {
                vel.x *= -1;
            }
            if (pos.y > P5JSExtension.height || pos.y < 0)
            {
                vel.y *= -1;
            }
        }
        public void show()
        {
            P5JSExtension.ellipse(pos.x, pos.y, r * 2, r * 2);
        }
    }
}