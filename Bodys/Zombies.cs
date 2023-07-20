using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Zombie : IBody
{
    Rectangle zombie;
    Random numberRandom = Random.Shared;
    Random randomFX = new Random();
    Random randomFY = new Random();
    public int attackdamage = 1;
    public int x;
    public int y;
    double velX = 0;
    double velY = 0;
    double FiX;
    double FiY;
    double dj;
    double dL;
    double range;
    bool humandamage = true;

    public Zombie(int x, int y)
    {
        zombie = new Rectangle(x, y, 20, 20);
        this.x = x;
        this.y = y;
    }

    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(color, this.zombie);
    }

    private DateTime lastFrame = DateTime.Now;
    public void DrunkZombie(List<Zombie> zombieList, int zombieLiderX, int zombieLiderY)
    {
        float A = 100_000_000f;
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
        }

        for (int i = 0; i < zombieList.Count; i++)
        {
            var zombie = zombieList[i];
            zombie.zombie.Location = new Point(
                (int)(zombie.zombie.Location.X + zombie.velX * time),
                (int)(zombie.zombie.Location.Y + zombie.velY * time)
            );
        }
    }


    public void Update()
    {
        throw new NotImplementedException();
    }

    public bool intersect(Human human)
    {
        Rectangle Rect = new Rectangle(human.x, human.y, human.width, human.height);
        if (this.zombie.IntersectsWith(Rect))
            return true;
        return false;
    }
}
