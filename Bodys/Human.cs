using System;
using System.Windows.Forms;
using System.Drawing;



public class Human : IBody
{
    Rectangle human;
    Random numberRandom = Random.Shared;

    public int life = 100;
    public int x;
    public int y;
    public int weight = 20;
    public int height = 20;

    int maxSpeed = 1;
    double direcaoX;
    double direcaoY;
    int pointOfView = 300;
    double range;
    double d;

    public Human(Form form)
    {
        
        human = new Rectangle(numberRandom.Next(0,1200), numberRandom.Next(0,1200), weight, height);
        this.x = human.Location.X;
        this.y = human.Location.Y;

    }

    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(color, this.human);
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public void escape(int zombieX, int zombieY)
    {
        x = human.Location.X;
        y = human.Location.Y;

        d = Math.Pow((x - zombieX), 2) + Math.Pow((y - zombieY), 2);

        range = Math.Sqrt(d);

        if (range <= pointOfView)
        {
            direcaoX = -(x - zombieX);
            direcaoY = -(y - zombieY);

            double pita = Math.Sqrt(direcaoX * direcaoX + direcaoY * direcaoY);
            
            try
            {
                x -= (int)direcaoX / (int)pita * maxSpeed;
                y -= (int)direcaoY / (int)pita * maxSpeed;
            }
            catch (System.Exception)
            {
            
            }
             
        }

        human.Location = new Point(x, y);
    }


    // public void death(Form form){
    //     form.Controls.Remove(human);
    // }
}