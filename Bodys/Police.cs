using System;
using System.Drawing;
using System.Windows.Forms;

public class Police : IBody
{
    Rectangle police;
    Random numberRandom = Random.Shared;
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




    public Police(Form form) {
        police = new Rectangle(
            numberRandom.Next( 0, 1200 ),
            numberRandom.Next( 0, 1200 ),
            width,
            height
        );
    }


    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(color, this.police);

    }

    public void Update()
    {
        police.Location = new Point(x, y);

    }

        public void escape(int zombieX, int zombieY)
    {
        x = police.Location.X;
        y = police.Location.Y;

        d = Math.Pow((x - zombieX), 2) + Math.Pow((y - zombieY), 2);

        range = Math.Sqrt(d);

        if (range <= pointOfView)
        {
            direcaoX = -(x - zombieX);
            direcaoY = -(y - zombieY);

            double pita = Math.Sqrt(direcaoX * direcaoX + direcaoY * direcaoY);
            
            try
            {
                x -= (int)direcaoX / (int)pita * MovieSpeed;
                y -= (int)direcaoY / (int)pita * MovieSpeed;
            }
            catch (System.Exception)
            {
            
            }
             
        }
    }
}