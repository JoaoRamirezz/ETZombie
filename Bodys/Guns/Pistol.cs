using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

public class Pistol : IGun
{
    Image PistolImg;
    Rectangle zombieRec;
    private Police police;
    private ZombieMain zombieMain;
    Rectangle pistol;
    public Rectangle bullet;
    double VeloBullet = 5;
    int width = 20;
    int height = 15;
    int x => police.police.Location.X - 8;
    int y => police.police.Location.Y + 10;
    public int damage = 50;

    public Point Target { get; set;} = new Point(-1000, -1000);

    public Pistol(Form form, Police police)
    {
        this.police = police;
        pistol = new Rectangle(x, y, width, height);
        bullet = new Rectangle(x, y, 7, 7);
    }

    public void Draw(Graphics g, Image gunImg, SolidBrush colorBullet)
    {
        PistolImg = gunImg;
        GraphicsUnit units = GraphicsUnit.Pixel;
        if(gunImg.Width > 100)
        {
            
            pistol.Width = 120;
            g.DrawImage(gunImg, pistol, 0, 0, 88, 40,units);
        }

        g.DrawImage(gunImg, pistol, 3, 0, 61, 50,units);
        g.FillRectangle(colorBullet, this.bullet);
    }

    public void Shot(Point joeLocation, Form form)
    {
        if(Target == new Point(-1000, -1000))
        {
            Target = joeLocation;
        }

        var BulletX = bullet.Location.X;
        var BulletY = bullet.Location.Y;

        var direcaoX = (x - Target.X);
        var direcaoY = (y - Target.Y);

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
        
        new Point(-1000, -1000);
        
        Joe.life -= damage;
        return true;
    }


    public void Reload(Form form)
    {
        Target = new Point(-1000, -1000);
        bullet.Location = new Point(x, y);
    }

    public void Update(int x, int y)
    {
        police = this.police;
        pistol.Location = new Point(x - 8, y + 10);
    }
}