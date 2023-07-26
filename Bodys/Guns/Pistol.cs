using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

public class Pistol : IGun
{
    Rectangle zombieRec;
    private Police police;
    private ZombieMain zombieMain;
    Rectangle pistol;
    public Rectangle bullet;
    double VeloBullet = 5;
    int width = 10;
    int height = 10;
    int x => police.police.Location.X;
    int y => police.police.Location.Y;
    public int damage = 15;

    public Pistol(Form form, Police police)
    {
        this.police = police;
        pistol = new Rectangle(x, y, width, height);
        bullet = new Rectangle(x, y, 7, 7);
    }

    public void Draw(Graphics g, SolidBrush color, SolidBrush colorBullet)
    {
        g.FillRectangle(color, this.pistol);
        g.FillRectangle(colorBullet, this.bullet);
    }


    public void Shot(int zombieLiderX, int zombieLiderY, Form form)
    {
        var BulletX = bullet.Location.X;
        var BulletY = bullet.Location.Y;

        var direcaoX = (x - zombieLiderX);
        var direcaoY = (y - zombieLiderY);

        var pita = Math.Sqrt(direcaoX * direcaoX + direcaoY * direcaoY);

        BulletX -= (int)((direcaoX) / pita * VeloBullet);
        BulletY -= (int)((direcaoY) / pita * VeloBullet);

        bullet.Location = new Point(BulletX, BulletY);

    }



    public bool hitJoe(ZombieMain Joe)
    {
        zombieRec = new Rectangle(Joe.x, Joe.y, Joe.Width, Joe.Height);
        if (!bullet.IntersectsWith(zombieRec))
            return false;
        
        Joe.life -= damage;
        return true;
    }


    public void Reload(Form form)
    {
        bullet.Location = new Point(x, y);
    }

    public void Update(int x, int y)
    {
        police = this.police;
        pistol.Location = new Point(x, y);
    }
}