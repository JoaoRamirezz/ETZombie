using System;
using System.Windows.Forms;
using System.Drawing;



public class Human : IBody
{
    Rectangle human;
    Random numberRandom = Random.Shared;

    public int life = 20;
    public int x;
    public int y;
    public int right;
    public int left;
    public int bottom;
    public int top;
    int maxSpeed = 5;
    double direcaoX;
    double direcaoY;
    int pointOfView = 300;
    double range;
    double d;

    public Human()
    {
        human = new Rectangle(numberRandom.Next(0,500), numberRandom.Next(0,500), 20, 20);
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

        d = Math.Pow((x - zombieX), 2) + Math.Pow((y - zombieY),2);

        range = Math.Sqrt(d);

        if (range <= pointOfView)
        {
            direcaoX = (x - zombieX)*-1;
            direcaoY = (y - zombieY)*-1;

            double pita = Math.Sqrt(direcaoX * direcaoX + direcaoY * direcaoY);
            
            x -= (int)direcaoX / (int)pita * maxSpeed;
            y -= (int)direcaoY / (int)pita * maxSpeed;
             
        }

        human.Location = new Point(x, y);
    }


    // public void death(Form form){
    //     form.Controls.Remove(human);
    // }
}