using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


public class Game
{
    public void go()
    {
        List<IBody> corpos = new List<IBody>();
        List<Zombie> zombies = new List<Zombie>();

        bool running = true;

        ApplicationConfiguration.Initialize();

        PictureBox pb = new PictureBox();
        pb.Dock = DockStyle.Fill;

        Graphics g = null;
        Bitmap bmp = null;

        var form = new Form();
        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.None;
        form.Controls.Add(pb);

        var zombieMain = new ZombieMain(500, 500);
        var human = new Human();
        var zombie = new Zombie(human.x, human.y);

        // Create rectangle for displaying image.

        var timer = new Timer();
        timer.Interval = 15;



        Application.Idle += delegate
        {
            while (running)
            {
                zombieMain.Draw(g,  new SolidBrush(Color.Red));
                zombieMain.Update();    
                human.Draw( g,  new SolidBrush(Color.Black));
                human.escape(zombieMain.x, zombieMain.y);
                
                foreach (var z in zombies)
                {
                    z.Draw(g,  new SolidBrush(Color.Green));  
                    z.Spaw(human.x, human.y);            
                    z.go(zombieMain.x,
                         zombieMain.y,
                         zombieMain.movespeed - 1);    
                }
                
                pb.Refresh();
                g.Clear(Color.Transparent);
                Application.DoEvents();
            }
        };


        form.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                running = false;
                Application.Exit();
            }
            zombieMain.go(e);
            if ((zombieMain.x >= human.x) && (zombieMain.y >= human.y))
            {
                human.life -= zombieMain.attackDamage;
                if (human.life < -0)
                {
                    var zombie = new Zombie(human.x, human.y);
                    human = new Human();
                    zombies.Add(zombie);
                }
            }

        };

        form.KeyUp += (s, e) =>
        {
            zombieMain.stop(e);
        };

        form.Load += delegate
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            pb.Image = bmp;
            

            timer.Start();
        };





        form.KeyPreview = true;

        Application.Run(form);
    }
}
