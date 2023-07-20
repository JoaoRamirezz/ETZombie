using System;
using System.Drawing;
using System.Windows.Forms;

public class ZombieMain : IBody
{

    Rectangle zombie;
    int life = 20;
    public int movespeed = 2;
    public int attackDamage = 5;
    public int x;
    public int y;
    bool goLeft = false;
    bool goRight = false;
    bool goTop = false;
    bool goDown = false;



    public ZombieMain()
    {
        zombie = new Rectangle(0, 0, 20, 20);
    }

    public void go(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.D)
            goRight = true;

        if (e.KeyCode == Keys.A)
            goLeft = true;

        if (e.KeyCode == Keys.S)
            goDown = true;

        if (e.KeyCode == Keys.W)
            goTop = true;
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
        Rectangle Rect = new Rectangle(human.x, human.y, human.weight, human.height);
        if (this.zombie.IntersectsWith(Rect))
            return true;
        return false;
    }
}









