using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


public class Game
{
    public void go()
    {
        List<IBody> bodys = new List<IBody>();
        List<Zombie> zombies = new List<Zombie>();
        List<Human> humans = new List<Human>();
        List<Police> polices = new List<Police>();
        List<Pistol> pistols = new List<Pistol>();

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
        var police = new Police(form);

        var zombie = new Zombie(human.x, human.y);
        var killed = false;

        // Create rectangle for displaying image.


        for (int i = 0; i < 100; i++)
        {
            human = new Human(form);
            humans.Add(human);
            bodys.Add(human);
        }

        for (int p = 0; p < 10; p++)
        {
            police = new Police(form);
            polices.Add(police);
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
                    humans[i].Update();
                    humans[i].TakeDamage(zombieMain.intersect(humans[i]), zombieMain.attackDamage);

                    if (humans[i].life <= 0)
                    {
                        var zombie = new Zombie(humans[i].x, humans[i].y);
                        humans.Remove(humans[i]);
                        zombies.Add(zombie);
                        bodys.Add(zombie);
                        i -= 1;
                    }
                }
                foreach (var z in zombies)
                {
                    z.Draw(g, new SolidBrush(Color.Green));
                       
                    for (int i = 0; i < humans.Count; i++)
                    {
                        humans[i].TakeDamage(z.intersect(humans[i]), z.attackdamage);

                        if (humans[i].life <= 0)
                        {
                            var zombie = new Zombie(humans[i].x, humans[i].y);
                            humans.Remove(humans[i]);
                            zombies.Add(zombie);
                            bodys.Add(zombie);
                            i -= 1;
                            killed = true;
                            break;
                        }
                    }
                    if (killed)
                        break;
                }
                killed = false;

                for (int p = 0; p < polices.Count; p++)
                {
                    polices[p].Draw(g, new SolidBrush(Color.Blue));
                    polices[p].ToSearchFor(zombieMain.x, zombieMain.y);
                    polices[p].Update();
                }

                // foreach (var p in polices)
                // {
                //     polices[p].Draw(g, new SolidBrush(Color.Blue));
                //     // polices[p].ToSearchFor(zombieMain.x, zombieMain.y);
                //     // polices[p].Update();
                // }

                // police.ToSearchFor( zombieMain.x, zombieMain.y);

                foreach (var z in zombies)
                    z.Draw(g, new SolidBrush(Color.Green));     
                        

                zombie.DrunkZombie(zombies, zombieMain.x, zombieMain.y);

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
            zombieMain.stop(e);

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
