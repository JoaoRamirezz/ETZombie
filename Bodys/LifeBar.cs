using System;
using System.Drawing;
using System.Windows.Forms;

public class LifeBar : IBody
{
    Rectangle bar;
    Rectangle backbar;
    public int Width;
    public int Height = 5;
    public int x;
    public int y;



    public LifeBar(int Width, int X, int Y)
    {
        backbar = new Rectangle(X, Y - 10, Width, Height);
        bar = new Rectangle(X, Y - 10, Width, Height);
        this.Width = Width;
    }

    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(new SolidBrush(Color.Black), this.backbar);
        g.FillRectangle(color, this.bar);
    }

    public void Damage(int damage, int life)
    {
        try
        {
            int d = damage * 100 / life;
            
            bar.Size = new Size((Width/life)-d, Height);   
        }

        catch (System.Exception)
        {
        }

    }

    public void Go(int ObjX, int ObjY)
    {
        this.x = ObjX;
        this.y = ObjY -10;
    }

    public void Update()
    {
        backbar.Location = new Point(this.x, this.y);
        bar.Location = new Point(this.x, this.y);
    }
}