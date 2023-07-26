using System;
using System.Drawing;
using System.Windows.Forms;

public class ZombieMain : IBody
{
    Rectangle bar;
    Rectangle backbar;
    Rectangle zombie;
    Rectangle mask;

    Image zombieImg;
    int distanceImg = 3;
    public int life = 200;
    public int movespeed = 2;
    public int attackDamage = 1;
    public int x;
    public int y;
    bool goLeft = false;
    bool goRight = false;
    bool goTop = false;
    bool goDown = false;
    public int Width = 20;
    public int Height = 20;
    public int SideX;
    public int SideY;
    bool run = false;
    public int chance = 1;
    int maxlife = 0;
    int barSize = 200;

    public ZombieMain(Image img)
    {
        zombie = new Rectangle(50, 50, 25, 25);
        zombieImg = img;
        maxlife = life;
        backbar = new Rectangle(0, 10, barSize, 20);
        bar = new Rectangle(0, 10, barSize, 20);
    }

    public void go(KeyEventArgs e, Wall wall)
    {
        int velX = e.KeyCode == Keys.A ? movespeed * -1 : e.KeyCode == Keys.D ? movespeed : 0;
        int velY = e.KeyCode == Keys.W ? movespeed * -1 : e.KeyCode == Keys.S ? movespeed : 0;

        SideX = velX;
        SideY = velY;

        if (e.KeyCode == Keys.D)
        {
            run = true;
            goRight = true;
        }

        if (e.KeyCode == Keys.A)
        {
            run = true;
            goLeft = true;

        }

        if (e.KeyCode == Keys.S)
        {
            goDown = true;
            run = true;
        }

        if (e.KeyCode == Keys.W)
        {
            goTop = true;
            run = true;
        }
        Update();

    }

    public void stop(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.D)
        {
            run = false;
            goRight = false;
        }

        if (e.KeyCode == Keys.A)
        {
            run = false;
            goLeft = false;
        }

        if (e.KeyCode == Keys.S)
        {
            run = false;
            goDown = false;
        }

        if (e.KeyCode == Keys.W)
        {
            run = false;
            goTop = false;
        }
    }

    public void Update()
    {
        if (goLeft)
            x -= movespeed;
        if (goRight)
            x += movespeed;
        if (goTop)
            y -= movespeed;
        if (goDown)
            y += movespeed;
        if (run)
            distanceImg += 40;
        if (distanceImg >= 140)
            distanceImg = 2;

        zombie.Location = new Point(x, y);    
    }

    public void Draw(Graphics g, SolidBrush color)
    {
        g.FillRectangle(color, this.zombie);
    }

    public void draw(Graphics g)
    {
        GraphicsUnit units = GraphicsUnit.Pixel;
        g.DrawImage(zombieImg, zombie, distanceImg, 0, 35, 55, units);
        g.FillRectangle(new SolidBrush(Color.Black), backbar);
        g.FillRectangle(new SolidBrush(Color.Red), bar);
    }


    public bool intersectShot(Rectangle bullet)
    => this.zombie.IntersectsWith(bullet);

    public bool intersectPolice(Police police)
    {
        Rectangle Rect = new Rectangle(police.x, police.y, police.width, police.height);
        if (this.zombie.IntersectsWith(Rect))
            return true;
        return false;
    }
    
    public bool intersect(Human human)
    {
        Rectangle Rect = new Rectangle(human.x, human.y, human.width, human.height);
        return this.zombie.IntersectsWith(Rect);
    }

    public bool CollideWallX(Wall wall)
    {
        mask = new Rectangle(zombie.X, zombie.Y, zombie.Width, zombie.Height);
        if (wall.Colision(mask) && goLeft || wall.Colision(mask) && goRight)
            return true;
        return false;
    }

    public bool CollideWallY(Wall wall)
    {
        mask = new Rectangle(zombie.X, zombie.Y, zombie.Width, zombie.Height);
        if (wall.Colision(mask) && goTop || wall.Colision(mask) && goDown)
            return true;
        return false;
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
            int d = life * barSize / maxlife;
            if (d < 0)
                d = 0;
            bar.Size = new Size(d, 20);
        }

        catch (System.Exception) { }
    }



}









