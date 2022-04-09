using raminrahimzada;
using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Calculating_Digits_of_Pi_with_Mandelbrot_Set : MonoBehaviour
{
    int digits = 11;
    decimal c = new Decimal(0.25);
    decimal hundred = new Decimal(100);
    decimal e;
    decimal z = Decimal.Zero;
    BigInteger iterations = 0;
    decimal two = new Decimal(2);

    void Start()
    {
        e = Decimal.One / DecimalMath.Power(hundred, digits - 1);
        c = c + e;

        P5JSExtension.background(0);
        P5JSExtension.fill(255);
        //P5JSExtension.textSize(32);
        //P5JSExtension.textAlign(CENTER);
    }
    void OnGUI()
    {
        for (int i = 0; i < 10000;i++)
        {
            if (z.CompareTo(new Decimal(2)) == -1)
            {
                z = z * z;
                z = z + c;
                iterations = iterations + BigInteger.One;
            }
            else
            {
                break;
            }
        }

        string s = iterations.ToString();
        int diff = digits - s.Length;
        for(int i = 0;i<diff;i++)
        {
            s = '0' + s;
        }
        s = s.Substring(0, 1) + '.' + s.Substring(1, s.Length-1);
        P5JSExtension.text(s, P5JSExtension.width / 2, P5JSExtension.height / 2);
    }
}