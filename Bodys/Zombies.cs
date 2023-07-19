using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Zombie : IBody
{
    Rectangle zombie;
    Random numberRandom = Random.Shared;
    int x;
    int y;

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

    public void Spaw(int x, int y)
    {
        zombie.Location = new Point(x, y);
    }

    public void go(int PositionPlayerX, int PositionPlayerY, int zombieSpeed)
    {
        if (x <= PositionPlayerX && y <= PositionPlayerY)
        {
            x += zombieSpeed;
            y += zombieSpeed;
        }

        if (x >= PositionPlayerX && y <= PositionPlayerY)
        {
            x -= zombieSpeed;
            y += zombieSpeed;
        }

        if (x <= PositionPlayerX && y >= PositionPlayerY)
        {
            x += zombieSpeed;
            y -= zombieSpeed;
        }

        if (x >= PositionPlayerX && y >= PositionPlayerY)
        {
            x -= zombieSpeed;
            y -= zombieSpeed;
        }
        
        zombie.Location = new Point(x, y);
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}
