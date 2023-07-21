using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

public class Bullet : IBullet
{
    private Police police;
    Rectangle bullet;
    double VeloBullet = 15;
    int width = 10;
    int height = 10;
    int x => police.police.Location.X;
    int y => police.police.Location.Y;
    public int bulletX;
    public int bulletY;

    public Bullet(Form form, Police police)
    {
        this.police = police;
        bullet = new Rectangle(x, y, width, height);
        bulletX = bullet.Location.X;
        bulletY = bullet.Location.Y; 
    }

    public void Draw(Graphics g)
    {
        g.FillRectangle(new SolidBrush(Color.Black), bullet);
    }

    public void Update(int xUpdate, int yUpdate)
    {
        bullet.Location = new Point(xUpdate, yUpdate);
    }
}