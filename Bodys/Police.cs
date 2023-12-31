using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Police : IBody
{
    Rectangle police;
    Random numberRandom = Random.Shared;
    private DateTime lastFrame = DateTime.Now;
    Rectangle bar;
    Rectangle backbar;
    public int x;
    public int y;
    public int width = 20;
    public int height = 20;
    double velX = 0;
    double velY = 0;
    double FiX;
    double FiY;
    double d;
    double range;
    int pointOfView = 400;
    double direcaoX;
    double direcaoY;
    int MovieSpeed = 1;

    public Police(Form form)
    {
        police = new Rectangle(
            numberRandom.Next(0, 1200),
            numberRandom.Next(0, 1200),
            width,
            height
        );

        backbar = new Rectangle(police.Location.X, police.Location.Y - 10, width, 5);
        bar = new Rectangle(police.Location.X, police.Location.Y - 10, width, 5);
    }


    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(color, this.police);
    }

    public void Update()
    {
        police.Location = new Point(x, y);

    }

    // public void ToSearchFor(int zombieX, int zombieY)
    // {
    //     x = police.Location.X;
    //     y = police.Location.Y;

    //     d = Math.Pow((x - zombieX), 2) + Math.Pow((y - zombieY), 2);

    //     range = Math.Sqrt(d);

    //     if (range <= pointOfView)
    //     {
    //         direcaoX = -(x - zombieX);
    //         direcaoY = -(y - zombieY);

    //         double pita = Math.Sqrt(direcaoX * direcaoX + direcaoY * direcaoY);

    //         try
    //         {
    //             x += (int)direcaoX / (int)pita * MovieSpeed;
    //             y += (int)direcaoY / (int)pita * MovieSpeed;
    //         }
    //         catch (System.Exception)
    //         {

    //         }

    //     }
    // }

    public void ToSearchFor(List<Police> policeList, int zombieLiderX, int zombieLiderY)
    {
        float A = 100_000_000f;
        float B = 0.05f;

        var FrameCurrent = DateTime.Now - lastFrame;
        lastFrame = DateTime.Now;
        var time = FrameCurrent.TotalSeconds;

        for (int p = 0; p < policeList.Count; p++)
        {
            x = policeList[p].police.Location.X;
            y = policeList[p].police.Location.Y;

            d = Math.Pow((x - zombieLiderX), 2) + Math.Pow((y - zombieLiderY), 2);

            range = Math.Sqrt(d);
            if (range <= pointOfView)
            {
                var Ldx = zombieLiderX - policeList[p].police.Location.X;
                var Ldy = zombieLiderY - policeList[p].police.Location.Y;
                var dL = Ldx * Ldx + Ldy * Ldy;
                var modL = MathF.Sqrt(dL);

                var distFunc = modL * modL - 5 * modL;
                var leaderAttract = new SizeF(Ldx, Ldy) * distFunc * B / modL / 1000;

                if (modL != 0)
                {
                    policeList[p].velX = leaderAttract.Width;
                    policeList[p].velY = leaderAttract.Height;
                }

                FiX = 0;
                FiY = 0;

                policeList[p].velX += FiX * time;
                policeList[p].velY += FiY * time;


                for (int i = 0; i < policeList.Count; i++)
                {
                    var police = policeList[i];
                    double pita = Math.Sqrt(police.police.Location.X * police.police.Location.X + police.police.Location.Y * police.police.Location.Y);

                    police.police.Location = new Point(
                        (int)(police.police.Location.X + police.velX * time),
                        (int)(police.police.Location.Y + police.velY * time)
                    );
                }
            }
        }
    }
}