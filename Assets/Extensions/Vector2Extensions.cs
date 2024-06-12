using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 mult(this Vector2 vector, float scale)
    {
        return vector * new Vector2(scale,scale);
    }
}
