using System.Collections.Generic;
using UnityEngine;

public class Quicksort_Visualization : MonoBehaviour
{
    float[] values;
    float i = 0;
    float w = 10;

    void Start()
    {
        values = new float[Mathf.FloorToInt(P5JSExtension.width / 2)];
        for(int i = 0;i < values.Length; i++)
        {
            values[i] = P5JSExtension.random(P5JSExtension.height*1f);
        }
        P5JSExtension.frameRate(5);
        quickSort(values, 0, values.Length - 1);
    }
    void quickSort(float[] arr,int start,int end)
    {
        if (start >= end)
        {
            return;
        }

        var index = partition(arr, start, end);
        quickSort(arr, start, index - 1);
        quickSort(arr, index+1, end);
    }
    int partition(float[] arr, int start,int end)
    {
        var pivotIndex = start;
        var pivotValue = arr[end];
        for(int i = start; i<end;i++)
        {
            if(arr[i] < pivotValue)
            {
                swap(arr,i, pivotIndex);
                pivotIndex++;
            }
        }
        swap(arr, pivotIndex, end);
        return pivotIndex;
    }
    void OnGUI()
    {
        P5JSExtension.background(51);

        for (int i = 0; i < values.Length; i++)
        {
            P5JSExtension.stroke(0);
            P5JSExtension.fill(255);
            P5JSExtension.rect(i * w, P5JSExtension.height - values[i], w, values[i]);
        }
    }
    void swap(float[] arr, int a, int b)
    {
        float temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }
}