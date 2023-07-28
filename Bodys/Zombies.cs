using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Zombie : IBody
{

    Image zombieImg;

    Rectangle bar;
    Rectangle backbar;
    Rectangle zombie;
    public int attackdamage = 1;
    public int x;
    public int y;
    public int Width = 30;
    public int Height = 25;
    double velX = 0;
    double velY = 0;
    double FiX;
    double FiY;
    public int life = 10;
    public int maxlife = 0;
    int speed = 10000;

    int distanceImg = 3;


    public Zombie(int x, int y)
    {
        zombie = new Rectangle(x, y, Width, Height);
        backbar = new Rectangle(zombie.Location.X, zombie.Location.Y - 10, Width, 5);
        bar = new Rectangle(zombie.Location.X, zombie.Location.Y - 10, Height, 5);

        maxlife = life;


        this.x = x;
        this.y = y;
    }

    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(color, this.zombie);
        g.FillRectangle(new SolidBrush(Color.Black), backbar);
        g.FillRectangle(new SolidBrush(Color.Red), bar);
    }

    public void putImage(Image img)
        => zombieImg = img;

    public void draw(Graphics g)
    {
        GraphicsUnit units = GraphicsUnit.Pixel;
        g.DrawImage(zombieImg, zombie, distanceImg, 0, 35, 40,units);
        g.FillRectangle(new SolidBrush(Color.Black), backbar);
        g.FillRectangle(new SolidBrush(Color.Red), bar);
    }

    public void Spaw(int x, int y)
    {
        zombie.Location = new Point(x, y);
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
            int d = life * Width / maxlife;
            if (d < 0)
                d = 0;
            bar.Size = new Size(d, 5);
        }

        catch (System.Exception) { }
    }


    private DateTime lastFrame = DateTime.Now;

    public void DrunkZombie(List<Zombie> zombieList, int zombieLiderX, int zombieLiderY)
    {
        float A = 300_000_000f;
        float B = 0.05f;

        var FrameCurrent = DateTime.Now - lastFrame;
        lastFrame = DateTime.Now;
        var time = FrameCurrent.TotalSeconds;

        for (int i = 0; i < zombieList.Count; i++)
        {
            var Ldx = zombieLiderX - zombieList[i].zombie.Location.X;
            var Ldy = zombieLiderY - zombieList[i].zombie.Location.Y;
            var dL = Ldx * Ldx + Ldy * Ldy;
            var modL = MathF.Sqrt(dL);

            var distFunc = modL * modL - 5 * modL;
            var leaderAttract = new SizeF(Ldx, Ldy) * distFunc * B / modL;

            if (modL != 0)
            {
                zombieList[i].velX = leaderAttract.Width;
                zombieList[i].velY = leaderAttract.Height;
            }

            FiX = 0;
            FiY = 0;

            for (int j = 0; j < zombieList.Count; j++)
            {
                if (i == j)
                    continue;

                var dx = zombieList[i].zombie.X - zombieList[j].zombie.X;
                var dy = zombieList[i].zombie.Y - zombieList[j].zombie.Y;

                var dj = dx * dx + dy * dy;
                var modj = MathF.Sqrt(dj);
                if (modj == 0)
                {
                    modj = 1;
                    dj = 1;
                    dx = 1;
                    dy = 1;
                }

                var dist = modj * modj * modj * modj;
                FiX += dx * A / dist;
                FiY += dy * A / dist;
            }

            zombieList[i].velX += FiX * time;
            zombieList[i].velY += FiY * time;

            if (zombieList[i].velX > speed)
                zombieList[i].velX = speed;
            else if (zombieList[i].velX < -speed)
                zombieList[i].velX = -speed;

            if (zombieList[i].velY > speed)
                zombieList[i].velY = speed;
            else if (zombieList[i].velY < -speed)
                zombieList[i].velY = -speed;
        }

        for (int i = 0; i < zombieList.Count; i++)
        {
            var zombie = zombieList[i];

            x = (int)(zombie.zombie.Location.X + zombie.velX * time);
            y = (int)(zombie.zombie.Location.Y + zombie.velY * time);

            zombie.distanceImg += 40;
            if(zombie.distanceImg >= 140)
                zombie.distanceImg = 2;

            zombie.zombie.Location = new Point(x, y);
            zombie.backbar.Location = new Point(x, y - 10);
            zombie.bar.Location = new Point(x, y - 10);
        }
    }

    public void Update()
    {

    }

    public bool intersect(Human human)
    {
        Rectangle Rect = new Rectangle(human.x, human.y, human.width, human.height);
        if (this.zombie.IntersectsWith(Rect))
            return true;
        return false;
    }
    public bool intersectPolice(Police police)
    {
        Rectangle Rect = new Rectangle(police.x, police.y, police.width, police.height);
        if (this.zombie.IntersectsWith(Rect))
            return true;
        return false;
    }

    public bool intersectShot(Rectangle bullet)
    => this.zombie.IntersectsWith(bullet);
}
