using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fourier_Transform_User_Drawing : MonoBehaviour
{
    const int USER = 0;
    const int FOURIER = 1;

    List<float> x = new List<float>();
    List<float> y = new List<float>();
    Epicycles[] fourierX;
    Epicycles[] fourierY;
    float time = 0f;
    List<Vector2> path = new List<Vector2>();
    List<Vector2> drawing = new List<Vector2>();
    int state = -1;

    void Start()
    {
        //800x600
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            state = USER;
            drawing.Clear();
            x.Clear();
            y.Clear();
            time = 0;
            path.Clear();
        }
        if (Input.GetMouseButtonUp(0))
        {
            state = FOURIER;
            int skip = 2;
            for (int i = 0; i < drawing.Count; i+=skip)
            {
                float angle = P5JSExtension.map(i, 0, 100, 0, 2 * Mathf.PI);
                x.Add(drawing[i].x);
                y.Add(drawing[i].y);
            }
            fourierX = dft(x);
            fourierY = dft(y);
        }
        if (state == USER)
        {
            var point = new Vector2(Input.mousePosition.x-P5JSExtension.width/2, Input.mousePosition.y - P5JSExtension.height / 2);
            drawing.Add(point);
        }
    }

    class Epicycles
    {
        public float re;
        public float im;
        public float freq;
        public float amp;
        public float phase;

        public Epicycles(float re,float im,float freq,float amp,float phase)
        {
            this.re = re;
            this.im = im;
            this.freq = freq;
            this.amp = amp;
            this.phase = phase;
        }
    }
    Epicycles[] dft(List<float> x)
    {
        Epicycles[] X = new Epicycles[x.Count];
        int N = x.Count;
        for (int k = 0; k < N; k++)
        {
            float re = 0;
            float im = 0;
            for (int n = 0; n < N; n++)
            {
                var phi = (2 * Mathf.PI * k * n) / N;
                re += x[n] * Mathf.Cos(phi);
                im -= x[n] * Mathf.Sin(phi);
            }
            re = re / N;
            im = im / N;

            float freq = k;
            float amp = Mathf.Sqrt(re * re + im * im);
            float phase = Mathf.Atan2(im, re);

            X[k] = new Epicycles(re, im, freq, amp, phase);
        }
        return X;
    }

    Vector2 epiCycles(float x,float y,float rotation,Epicycles[] fourier)
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
            P5JSExtension.ellipse(prevx, P5JSExtension.height - prevy, radius * 2);
            P5JSExtension.stroke(255);
            P5JSExtension.line(prevx, P5JSExtension.height - prevy, x, P5JSExtension.height - y);
        }
        return new Vector2(x, y);
    }

    void OnGUI()
    {
        P5JSExtension.resetMatrix();

        if (state == FOURIER)
        {
            var vx = epiCycles(P5JSExtension.width/2, P5JSExtension.height - 100, 0,fourierX);
            var vy = epiCycles(100, P5JSExtension.height - P5JSExtension.height/2 , Mathf.PI / 2,fourierY);
            var v = new Vector2(vx.x, vy.y);
            path.Add(v);    
            P5JSExtension.line(vx.x, P5JSExtension.height - vx.y, v.x, P5JSExtension.height - v.y);
            P5JSExtension.line(vy.x, P5JSExtension.height - vy.y, v.x, P5JSExtension.height - v.y);
            P5JSExtension.resetShape();
            P5JSExtension.beginShape(MeshTopology.LineStrip);
            for (int i = 0; i < path.Count; i++)
            {
                P5JSExtension.vertex(path[i].x, path[i].y);
            }
            gameObject.GetComponent<MeshFilter>().mesh = P5JSExtension.endShape();

            float dt = (2f * Mathf.PI) / fourierY.Length;
            time += dt;

            if(time > 2*Mathf.PI)
            {
                time = 0;
                path.Clear();
            }
        }

        //if (wave.Count > 500)
        //{
        //    wave.RemoveAt(0);
        //}
    }
}