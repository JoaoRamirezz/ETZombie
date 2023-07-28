using System;
using System.Windows.Forms;
using System.Drawing;

public class BrainPoison : IBody
{
    Image brainImg;

    Random numberRandom = new Random();
    Rectangle brainpoison;
    public int width = 10;
    public int height = 10;
    public int x = 0;
    public int y = 0;


    public BrainPoison(Form form)
    {
        brainpoison = new Rectangle(
        numberRandom.Next(0, 1200),
        numberRandom.Next(0, 1200),
        width,
        height);

        this.x = brainpoison.Location.X;
        this.y = brainpoison.Location.Y;
    }

    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(color, this.brainpoison);
    }

    public void draw(Graphics g)
    {
        GraphicsUnit units = GraphicsUnit.Pixel;
        g.DrawImage(brainImg, brainpoison, 3, 0, 35, 40, units);
    }

    public void putImage(Image img)
        => brainImg = img;

    public void Update()
    {
        throw new NotImplementedException();
    }
}