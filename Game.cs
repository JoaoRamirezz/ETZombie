#pragma warning disable

using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


public class Game
{
    
    private List<IBody> bodys;
    private List<Zombie> zombies;
    private List<Human> humans;
    private List<Police> polices;
    private List<Pistol> pistols;


    private ZombieMain zombieMain;
    private Human human;
    private Police police;
    private Zombie zombie;
    private Wall wall;


    public void go()
    {
        bodys = new List<IBody>();
        zombies = new List<Zombie>();
        humans = new List<Human>();
        polices = new List<Police>();
        pistols = new List<Pistol>();

        bool running = true;

        ApplicationConfiguration.Initialize();


        Graphics g = null;
        Bitmap bmp = null;
        Image Joe = Image.FromFile("imagens/JoeSprites.png");
        Rectangle ImgRec = new Rectangle(0,0,120,120);


        var form = new Form();
        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.None;

        PictureBox pb = new PictureBox();
        pb.Dock = DockStyle.Fill;
        form.Controls.Add(pb);


        zombieMain = new ZombieMain(Joe);
        human = new Human(form);
        police = new Police(form);
        zombie = new Zombie(human.x, human.y);
        // wall = new Wall(50,50,30,100);


        var killed = false;
        var brains = 0;


        generateBots(100,10,form);


        var timer = new Timer();
        timer.Interval = 30;



        Application.Idle += delegate
        {
            while (running)
            {

                GraphicsUnit units = GraphicsUnit.Pixel;
                g.DrawString("Brains: " + brains.ToString(), new Font("arial", 10), Brushes.Black, 0, 0);
                zombieMain.draw(g);
                zombieMain.Update();

                // if(zombieMain.CollideWallX(wall))
                // {
                //     if(zombieMain.x > wall.X)
                //         zombieMain.x = wall.X + wall.Width;
                //     else
                //         zombieMain.x = wall.X - zombieMain.Height;
                // }

                // if(zombieMain.CollideWallY(wall))
                // {
                //     if(zombieMain.y < wall.Y)
                //         zombieMain.y = wall.Y - zombieMain.Height;
                //     else
                //         zombieMain.y = wall.Y + wall.Height;
                // }


                // wall.Draw(g, new SolidBrush(Color.Orange));

                for (int i = 0; i < humans.Count; i++)
                {
                    humans[i].Draw(g, new SolidBrush(Color.Black));
                    humans[i].escape(zombieMain.x, zombieMain.y);
                    humans[i].Update();
                    humans[i].TakeDamage(zombieMain.intersect(humans[i]), zombieMain.attackDamage);

                    if (humans[i].life <= 0)
                    {
                        newZombie(humans[i]);
                        i -= 1;
                        brains += 1;
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
                            newZombie(humans[i]);
                            i -= 1;
                            brains += 1;
                            killed = true;
                            break;
                        }
                    }
                    if (killed)
                        break;
                }
                killed = false;

                foreach (var p in pistols)
                {
                    p.hit(zombies, zombieMain);    
                }


                foreach (var p in polices)
                {
                    p.Draw(g, new SolidBrush(Color.Blue));
                    p.ToSearchFor(zombieMain, form, zombies);
                    p.Update();
                }

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
            zombieMain.go(e,wall);

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




    public void newZombie(Human human)
    {
        var zombie = new Zombie(human.x, human.y);
        humans.Remove(human);
        zombies.Add(zombie);
        bodys.Add(zombie);
    }


    public void generateBots(int qttHumans, int qttPolices, Form form)
    {
        for (int i = 0; i < qttHumans; i++)
        {
            human = new Human(form);
            humans.Add(human);
            bodys.Add(human);
        }

        for (int p = 0; p < qttPolices; p++)
        {
            police = new Police(form);
            polices.Add(police);
        }

    }
}
