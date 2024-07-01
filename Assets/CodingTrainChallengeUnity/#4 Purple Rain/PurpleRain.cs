using static P5JSExtension;

public class PurpleRain : P5JSBehaviour
{
    Drop[] drops = new Drop[500];

    protected override void setup()
    {
        createCanvas(640, 360);
        for (int i = 0; i < drops.Length; i++)
        {
            drops[i] = new Drop();
        }
    }

    protected override void draw()
    {
        background(230, 230, 250);

        for (var i = 0; i < drops.Length; i++)
        {
            drops[i].fall();
            drops[i].show();
        }
    }

    public class Drop
    {
        float x;
        float y;
        float z;
        float len;
        float yspeed;

        public Drop()
        {
            x = random(width);
            y = random(-500, -50);
            z = random(0, 20);
            len = map(this.z, 0, 20, 10, 20);
            yspeed = map(this.z, 0, 20, 1, 20);
        }
        public void fall()
        {
            this.y = this.y + this.yspeed;
            var grav = map(z, 0, 20, 0, 0.2f);
            this.yspeed = this.yspeed + grav;

            if (this.y > height)
            {
                this.y = random(-200, -100);
                this.yspeed = map(this.z, 0, 20, 4, 10);
            }
        }

        public void show()
        {
            float thick = map(z, 0, 20, 1, 3);
            strokeWeight(thick);
            stroke(138, 43, 226);
            line(x, y, x, y + len);
        }
    }
}