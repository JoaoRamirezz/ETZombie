using System;
using System.Drawing;
using System.Windows.Forms;

public class Wall : IBody
{
    Rectangle wall;
    public int X;
    public int Y;
    public int Width;
    public int Height;
    public Wall(int locationX, int locationY, int width, int height)
    {
        wall = new Rectangle(locationX, locationY, width, height);
        X = locationX;
        Y = locationY;
        Width = width;
        Height = height;
    }

    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(color, this.wall);

    }

    public bool Colision(Rectangle body)
    => body.IntersectsWith(this.wall);

    public void Update()
    {
        throw new NotImplementedException();
    }
}