using System.Collections.Generic;
using UnityEngine;

public class Recamans_Sequence_Part_1 : MonoBehaviour
{

    Dictionary<int, bool> numbers = new Dictionary<int, bool>();
    int count = 1;
    List<int> sequence = new List<int>();
    int index = 0;

    void Start()
    {
        //createCanvas(600,400);
        P5JSExtension.background(0);
        P5JSExtension.dontclear();
        numbers[index] = true;
        sequence.Add(index);
    }
    void step()
    {
        int next = index - count;
        if (numbers.ContainsKey(next) == false)
        {
            numbers[next] = false;
        }
        if(next < 0 || numbers[next])
        {
            next = index + count;
        }
        numbers[next] = true;
        sequence.Add(next);

        var diameter = next - index;
        var x = (next + index) / 2;
        P5JSExtension.stroke(255);
        P5JSExtension.noFill();
        P5JSExtension.arc(x, P5JSExtension.height/2, diameter, diameter, 0,2*Mathf.PI);

        index = next;
        count++;
    }

    void OnGUI()
    {
        step();
        
        print(index);
    }
}