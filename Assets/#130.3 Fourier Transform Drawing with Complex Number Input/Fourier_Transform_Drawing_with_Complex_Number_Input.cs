using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fourier_Transform_Drawing_with_Complex_Number_Input : MonoBehaviour
{
    class Complex
    {
        public float re, im;
        public Complex(float re,float im)
        {
            this.re = re;
            this.im = im;
        }
        public Complex mult(Complex c)
        {
            var re = this.re * c.re - this.im * c.im;
            var im = this.re * c.im + this.im * c.re;
            return new Complex(re, im);
        }
        public Complex add(Complex c)
        {
            this.re += c.re;
            this.im += c.im;
            return new Complex(re, im);
        }
    }

    class Epicycles
    {
        public float re;
        public float im;
        public float freq;
        public float amp;
        public float phase;

        public Epicycles(float re, float im, float freq, float amp, float phase)
        {
            this.re = re;
            this.im = im;
            this.freq = freq;
            this.amp = amp;
            this.phase = phase;
        }
    }
    Epicycles[] dft(List<Complex> x)
    {
        Epicycles[] X = new Epicycles[x.Count];
        int N = x.Count;
        for (int k = 0; k < N; k++)
        {
            var sum = new Complex(0, 0);
            for (int n = 0; n < N; n++)
            {
                var phi = (2 * Mathf.PI * k * n) / N;
                var c = new Complex(Mathf.Cos(phi), -Mathf.Sin(phi));
                sum = sum.add(x[n].mult(c));
            }
            sum.re = sum.re / N;
            sum.im = sum.im / N;

            float freq = k;
            float amp = Mathf.Sqrt(sum.re * sum.re + sum.im * sum.im);
            float phase = Mathf.Atan2(sum.im, sum.re);

            X[k] = new Epicycles(sum.re, sum.im, freq, amp, phase);
        }
        return X;
    }
    Vector2 epicycles(float x, float y, float rotation, Epicycles[] fourier)
    {
        for (int i = 0; i < fourier.Length; i++)
        {
            var prevx = x;
            var prevy = y;

            var freq = fourier[i].freq;
            var radius = fourier[i].amp;
            var phase = fourier[i].phase;
            x += radius * Mathf.Cos(freq * time + phase + rotation);
            y += radius * Mathf.Sin(freq * time + phase + rotation);

            P5JSExtension.stroke(255, 100);
            P5JSExtension.noFill();
            P5JSExtension.ellipse(prevx, prevy, radius * 2);
            P5JSExtension.stroke(255);
            P5JSExtension.line(prevx, prevy, x, y);
        }
        return new Vector2(x, y);
    }

    List<Complex> x = new List<Complex>();
    Epicycles[] fourierX;
    float time = 0f;
    List<Vector2> path = new List<Vector2>();

    void Start()
    {
        int skip = 10;
        for (int i = 0; i < codingtrain.drawing.Length; i += skip)
        {
            var c = new Complex(codingtrain.drawing[i].x, codingtrain.drawing[i].y);
            x.Add(c);
        }
        fourierX = dft(x);
    }
    void OnGUI()
    {
        P5JSExtension.resetMatrix();

        var v = epicycles(P5JSExtension.width / 2, P5JSExtension.height / 2, 0, fourierX);
        path.Add(v);

        P5JSExtension.resetShape();
        P5JSExtension.beginShape(MeshTopology.LineStrip);
        for (int i = 0; i < path.Count; i++)
        {
            P5JSExtension.vertex(path[i].x, P5JSExtension.height - path[i].y);
        }
        gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();

        float dt = (2f * Mathf.PI) / fourierX.Length;
        time += dt;

        if (time > 2 * Mathf.PI)
        {
            time = 0;
            path.Clear();
        }

        //if (wave.Count > 500)
        //{
        //    wave.RemoveAt(0);
        //}
    }
}