using System.Collections.Generic;
using UnityEngine;

public class Bubble_Sort_Visualization : MonoBehaviour
{
    float[] values;
    int i = 0;
    int j = 0;

    void Start()
    {
        values = new float[P5JSExtension.width];
        for (int i = 0;i < values.Length;i++)
        {
            values[i] = P5JSExtension.noise(i/100.0f)*P5JSExtension.height;
        }
    }
    void swap(float [] arr,int a,int b)
    {
        float temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }
    void OnGUI()
    {
        P5JSExtension.background(0);

        if(i <values.Length)
        {
            for (int j = 0; j < values.Length-i-1; j++)
            {
                float a = values[j];
                float b = values[j + 1];
                if (a > b)
                {
                    swap(values, j, j + 1);
                }
            }
        }
        else
        {
            print("finished");
        }
        i++;

        for (int i = 0; i < values.Length; i++)
        {
            P5JSExtension.stroke(255);
            P5JSExtension.line(i, P5JSExtension.height, i, P5JSExtension.height - values[i]);
        }
    }
}