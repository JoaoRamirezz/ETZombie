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
        List<LifeBar> lifebars = new List<LifeBar>();

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
        var lifebar = new LifeBar(zombieMain.Width, zombieMain.x, zombieMain.y);

        var human = new Human(form);
        var zombie = new Zombie(human.x, human.y);

        // Create rectangle for displaying image.


        for (int i = 0; i < 100; i++)
        {
            human = new Human(form);
            humans.Add(human);

            lifebar = new LifeBar(human.width, human.x, human.y);
            lifebars.Add(lifebar);
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
                    lifebars[i].Draw(g, new SolidBrush(Color.Red));
                    // humans[i].escape(zombieMain.x, zombieMain.y);
                    lifebars[i].Go(humans[i].x, humans[i].y);
                    lifebars[i].Update();

                    

                    if (zombieMain.intersect(humans[i]))
                    {
                        lifebars[i].Damage(zombieMain.attackDamage, humans[i].life);
                        humans[i].life -= zombieMain.attackDamage;
                            

                        if (humans[i].life <= 0)
                        {
                            lifebars.Remove(lifebars[i]);
                            var zombie = new Zombie(humans[i].x, humans[i].y);
                            humans.Remove(humans[i]);
                            zombies.Add(zombie);
                            i-=1;
                        }
                    }
                }


                foreach (var z in zombies)
                {
                    z.Draw(g, new SolidBrush(Color.Green));
                    z.Spaw(human.x, human.y);            
                    // z.go(zombieMain.x,
                    //      zombieMain.y,
                    //      zombieMain.movespeed - 1);

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
