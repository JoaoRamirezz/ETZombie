using System;
using System.Drawing;
using System.Windows.Forms;

public class Police : IBody
{
    Rectangle police;
    Random numberRandom = Random.Shared;
    Rectangle bar;
    Rectangle backbar;
    public int x;
    public int y;
    public int width = 20;
    public int height = 20;
    double d;
    double range;
    int pointOfView = 300;
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

    public void ToSearchFor(int zombieMainX, int zombieMainY)
    {
        x = police.Location.X;
        y = police.Location.Y;

        d = Math.Pow((x - zombieMainX), 2) + Math.Pow((y - zombieMainY), 2);

        range = Math.Sqrt(d);

        if (range <= pointOfView)
        {
            direcaoX = x - zombieMainX;
            direcaoY = y - zombieMainY;

            double pita = Math.Sqrt(direcaoX * direcaoX + direcaoY * direcaoY);

            try
            {
                x -= (int)(direcaoX / pita * MovieSpeed);
                y -= (int)(direcaoY / pita * MovieSpeed);
            }
            catch (System.Exception)
            {

            }

        }
    }
}