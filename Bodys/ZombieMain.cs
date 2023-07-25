using System;
using System.Drawing;
using System.Windows.Forms;

public class ZombieMain : IBody
{

    Rectangle zombie;
    Rectangle mask;
    public int life = 20;
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
    bool humandamage = true;
    public bool colision = false;
    public int SideX;
    public int SideY;



    public ZombieMain()
    {
        zombie = new Rectangle(0, 0, Width, Height);
    }


    public void go(KeyEventArgs e, Wall wall)
    {
        int velX = e.KeyCode == Keys.A ? movespeed * -1 : e.KeyCode == Keys.D ? movespeed : 0;
        int velY = e.KeyCode == Keys.W ? movespeed * -1 : e.KeyCode == Keys.S ? movespeed : 0;

        SideX = velX;
        SideY = velY;

        if (e.KeyCode == Keys.D)
            goRight = true;

        if (e.KeyCode == Keys.A)
            goLeft = true;

        if (e.KeyCode == Keys.S)
            goDown = true;

        if (e.KeyCode == Keys.W)
            goTop = true;

        Update();

    }


    public void stop(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.D)
            goRight = false;

        if (e.KeyCode == Keys.A)
            goLeft = false;

        if (e.KeyCode == Keys.S)
            goDown = false;

        if (e.KeyCode == Keys.W)
            goTop = false;
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

        zombie.Location = new Point(x, y);
        
    }


    public void Draw(Graphics g, SolidBrush color)
    {
       g.FillRectangle(color, this.zombie);
    }


    public bool intersect(Human human)
    {
        Rectangle Rect = new Rectangle(human.x, human.y, human.width, human.height);
        return this.zombie.IntersectsWith(Rect);
    }


    public bool CollideWallX(Wall wall)
    {
        mask = new Rectangle(zombie.X, zombie.Y, zombie.Width, zombie.Height);
        if(wall.Colision(mask) && goLeft || wall.Colision(mask) && goRight)
            return true;
        return false;
    }

    public bool CollideWallY(Wall wall)
    {
        mask = new Rectangle(zombie.X, zombie.Y, zombie.Width, zombie.Height);
        if(wall.Colision(mask) && goTop || wall.Colision(mask) && goDown)
            return true;
        return false;
    }
}









