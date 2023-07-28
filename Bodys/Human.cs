using System;
using System.Windows.Forms;
using System.Drawing;



public class Human : IBody
{
    Image humanImg;
    int distanceImg = 3;

    Rectangle human;
    Random numberRandom = Random.Shared;
    Rectangle bar;
    Rectangle backbar;

    public int maxlife = 10;
    public int life = 10;
    public int x;
    public int y;
    public int width = 35;
    public int height = 30;

    int maxSpeed = 1;
    double direcaoX;
    double direcaoY;
    int pointOfView = 300;
    double range;
    double d;

    public Human(Form form)
    {
        human = new Rectangle(
        numberRandom.Next(0, 1200),
        numberRandom.Next(0, 1200),
        width,
        height);

        backbar = new Rectangle(human.Location.X, human.Location.Y - 10, width, 5);
        bar = new Rectangle(human.Location.X, human.Location.Y - 10, width, 5);

        this.x = human.Location.X;
        this.y = human.Location.Y;
    }

    public void putImage(Image img)
        => humanImg = img;

    public void Draw(Graphics g)
    {
        GraphicsUnit units = GraphicsUnit.Pixel;
        g.DrawImage(humanImg, human, distanceImg, 0, 35, 40, units);
        g.FillRectangle(new SolidBrush(Color.Black), backbar);
        g.FillRectangle(new SolidBrush(Color.Red), bar);
    }

    public void TakeDamage(bool damage, int attack)
    {
        if (damage)
            Damage(attack);
    }

    public void Damage(int attack)
    {
        life -= attack;
        try
        {
            int d = life * width / maxlife;
            if (d < 0)
                d = 0;
            bar.Size = new Size(d, 5);
        }

        catch (System.Exception) { }
    }

    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(color, this.human);
        g.FillRectangle(new SolidBrush(Color.Black), backbar);
        g.FillRectangle(new SolidBrush(Color.Red), bar);

    }

    public void Update()
    {
        human.Location = new Point(x, y);

        backbar.Location = new Point(x, y - 10);
        bar.Location = new Point(x, y - 10);

        distanceImg += 40;
        if (distanceImg >= 140)
            distanceImg = 2;
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
    }
}