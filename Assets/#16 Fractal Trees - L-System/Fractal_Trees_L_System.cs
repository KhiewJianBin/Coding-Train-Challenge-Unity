using UnityEngine;

public class Fractal_Trees_L_System : MonoBehaviour
{
    float angle;
    const string axiom = "F";
    string sentence = axiom;
    float len = 100;

    Rule[] rules = new Rule[1];
    class Rule
    {
        public string a;
        public string b; 
    }
    void Start()
    {
        rules[0] = new Rule
        {
            a = "F",
            b = "FF+[+F-F-F]-[-F+F+F]"
        };
        angle = P5JSExtension.radians(25);
        P5JSExtension.background(51);
    }
    void generate()
    {
        len *= 0.5f;
        var nextsentence = "";
        for (int i = 0; i < sentence.Length; i++)
        {
            var current = sentence[i];
            var found = false;
            for (int j = 0; j < rules.Length; j++)
            {
                if (current == rules[j].a[0])
                {
                    found = true;
                    nextsentence += rules[j].b;
                    break;
                }
            }
            if (!found)
            {
                nextsentence += current;
            }
        }
        sentence = nextsentence;
        turtle();
    }
    void turtle()
    {
        P5JSExtension.background(51);
        P5JSExtension.resetMatrix();
        P5JSExtension.translate(P5JSExtension.width / 2, P5JSExtension.height);
        P5JSExtension.stroke(255);
        for (int i = 0; i < sentence.Length; i++)
        {
            var current = sentence[i];

            if (current == 'F')
            {
                P5JSExtension.line(0, 0, 0, -len);
                P5JSExtension.translate(0, -len);
            }
            else if (current == '+')
            {
                P5JSExtension.rotate(angle);
            }
            else if (current == '-')
            {
                P5JSExtension.rotate(-angle);
            }
            else if (current == '[')
            {
                P5JSExtension.push();
            }
            else if (current == ']')
            {
                P5JSExtension.pop();
            }
        }
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(P5JSExtension.width - 100, P5JSExtension.height - 100, 100, 100), "Generate"))
        {
            generate();
        }
        GUI.TextField(new Rect(10, 10, P5JSExtension.width, 100), sentence);
        turtle();
    }
}
