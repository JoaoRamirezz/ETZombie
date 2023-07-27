using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

public class Pistol : IGun
{
    private Police police;
    Rectangle pistol;
    Rectangle bullet;
    Graphics g;
    double VeloBullet = 15;
    int width = 10;
    int height = 10;
    int x => police.police.Location.X;
    int y => police.police.Location.Y;

    public Pistol(Form form, Police police)
    {
        this.police = police;
        pistol = new Rectangle(x, y, width, height);
        bullet = new Rectangle(x, y, width, height);
    }

    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(color, this.pistol);
    }

    public void Shot(int zombieLiderX, int zombieLiderY)
    { 
        // bullet.Draw(g, new SolidBrush(Color.Black));

        var BulletX = bullet.Location.X;
        var BulletY = bullet.Location.Y;

        var direcaoX = (x - zombieLiderX);
        var direcaoY = (y - zombieLiderY);

        var pita = Math.Sqrt(direcaoX * direcaoX + direcaoY * direcaoY);

        BulletX -= (int)((direcaoX) / pita);
        BulletY -= (int)((direcaoY) / pita);

        bullet.Location = new Point(BulletX, BulletY);
    }

    public void Inactive()
    {
        var BulletX = pistol.Location.X;
        var BulletY = pistol.Location.Y;

        // bullet.Update(BulletX, BulletY);
    }

    public void Reload()
    {

    }

    // public void Update(Police police)
    // {
    //     police = this.police;
    //     pistol.Location = new Point(x, y);
    // }

    public void Update(int x, int y)
    {
        police = this.police;
        pistol.Location = new Point(x, y);
    }
}