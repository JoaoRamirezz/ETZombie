using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


public class Game
{
    public void go()
    {
        List<IBody> corpos = new List<IBody>();
        List<Zombie> zombies = new List<Zombie>();
        List<Human> humans = new List<Human>();

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



        var zombieMain = new ZombieMain();
        var human = new Human(form);
        var zombie = new Zombie(human.x, human.y);

        // Create rectangle for displaying image.


        for (int i = 0; i < 10; i++)
        {
            human = new Human(form);
            humans.Add(human);
        }
        var timer = new Timer();
        timer.Interval = 15;



        Application.Idle += delegate
        {
            while (running)
            {

                zombieMain.Draw(g, new SolidBrush(Color.Red));
                zombieMain.Update();

                for (int i = 0; i < humans.Count; i++)
                {
                    humans[i].Draw(g, new SolidBrush(Color.Black));
                    humans[i].escape(zombieMain.x, zombieMain.y);

                    if (zombieMain.intersect(humans[i]))
                    {
                        humans[i].life -= zombieMain.attackDamage;
                        if (humans[i].life < -0)
                        {
                            var zombie = new Zombie(humans[i].x, humans[i].y);
                            // human = new Human(form);
                            // humans.Add(h);
                            var removed = humans.Remove(humans[i]);
                            zombies.Add(zombie);
                            i-=1;
                        }
                    }
                }


                foreach (var z in zombies)
                {
                    z.Draw(g, new SolidBrush(Color.Green));
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
