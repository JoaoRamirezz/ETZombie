#pragma warning disable
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Media;

public class Game
{
    Image imageZombie;
    Upgrade upgrade = new Upgrade();

    private Random number = new Random();
    private int brain = 0;

    private List<Image> zombiesImg;
    private List<Image> policesImg;
    private List<Image> humansImg;


    private List<IBody> bodys;
    private List<Zombie> zombies;
    private List<Human> humans;
    private List<Police> polices;
    private List<Pistol> pistols;
    private List<BrainPoison> brainsPoison;

    private BrainPoison brainpoison;
    private Human human;
    private Police police;
    private Zombie zombie;
    private Wall wall;
    int pct;
    bool dead = false;
    bool flag = true;
    int spawmPolices = 3;
    int spawnHumans = 80;
    int spawmPoison = 5;

    public void go(Image JoeImg, ZombieMain zombieMain, SoundPlayer music)
    {
        music.PlayLooping();
        zombiesImg = new List<Image>();
        humansImg = new List<Image>();
        policesImg = new List<Image>();

        zombiesImg.Add(Image.FromFile("imagens/zombines1.png"));
        zombiesImg.Add(Image.FromFile("imagens/zombines2.png"));
        zombiesImg.Add(Image.FromFile("imagens/zombines3.png"));

        bodys = new List<IBody>();
        zombies = new List<Zombie>();
        humans = new List<Human>();
        polices = new List<Police>();
        pistols = new List<Pistol>();
        brainsPoison = new List<BrainPoison>();

        bool running = true;

        Graphics g = null;
        Bitmap bmp = null;
        Rectangle ImgRec = new Rectangle(0, 0, 120, 120);

        var form = new Form();
        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.None;
        form.BackgroundImage = Image.FromFile("Screens/images/map.png");


        PictureBox pb = new PictureBox();
        pb.Dock = DockStyle.Fill;
        form.Controls.Add(pb);
        zombieMain.putImage(JoeImg);

        var zombieFlag = zombieMain;

        brainpoison = new BrainPoison(form);
        human = new Human(form);
        police = new Police(form, pistols);
        zombie = new Zombie(human.x, human.y);
        // wall = new Wall(50,50,30,100);

        var killed = false;

        generateBots(spawnHumans, spawmPolices, spawmPoison, form);

        var timer = new Timer();
        timer.Interval = 30;

        zombieMain.life = zombieMain.maxlife;

        Application.Idle += delegate
        {
            while (running)
            {
                if (zombieMain.life <= 0)
                    break;


                GraphicsUnit units = GraphicsUnit.Pixel;
                g.DrawString("Brains:" + brain.ToString(), new Font("arial", 15), Brushes.Black, 0, 40);
                g.DrawString("Horde:" + zombies.Count.ToString(), new Font("arial", 15), Brushes.Black, 0, 65);


                g.DrawString("Damage: " + zombieMain.attackDamage.ToString(), new Font("arial", 13), Brushes.Black, 0, 112);
                g.DrawString("Speed: " + zombieMain.movespeed.ToString(), new Font("arial", 13), Brushes.Black, 0, 132);
                g.DrawString("Chance: " + zombieMain.chance.ToString(), new Font("arial", 13), Brushes.Black, 0, 152);
                g.DrawString("Cure: " + zombieMain.cure.ToString(), new Font("arial", 13), Brushes.Black, 0, 172);
                
                g.DrawString(zombieMain.life.ToString(), new Font("arial", 10), Brushes.Black, 210, 0);
                zombieMain.draw(g);
                zombieMain.Update();

                for (int b = 0; b < brainsPoison.Count; b++)
                {
                    brainsPoison[b].Draw(g, new SolidBrush(Color.Purple));
                    if (zombieMain.takePoison(brainsPoison[b]))
                    {
                        zombieMain.life += zombieMain.cure;
                        brainsPoison.Remove(brainsPoison[b]);
                        b -= 1;
                        zombieMain.Update();
                    }
                }

                for (int i = 0; i < humans.Count; i++)
                {
                    humans[i].Draw(g, new SolidBrush(Color.Black));
                    humans[i].escape(zombieMain.x, zombieMain.y);
                    humans[i].Update();
                    humans[i].TakeDamage(zombieMain.intersect(humans[i]), zombieMain.attackDamage);

                    if (humans[i].life <= 0)
                    {
                        newZombie(humans[i], zombieMain);
                        i -= 1;
                    }
                }

                for (int p = 0; p < polices.Count; p++)
                {

                    polices[p].Draw(g, new SolidBrush(Color.Blue));
                    polices[p].ToSearchFor(zombieMain, form, zombies);
                    polices[p].Update();
                    polices[p].TakeDamage(zombieMain.intersectPolice(polices[p]), zombieMain.attackDamage);

                    if (polices[p].life <= 0)
                    {
                        polices.Remove(polices[p]);
                        p -= 1;
                    }
                }

                for (int j = 0; j < zombies.Count; j++)
                {
                    zombies[j].draw(g);

                    for (int i = 0; i < humans.Count; i++)
                    {
                        humans[i].TakeDamage(zombies[j].intersect(humans[i]), zombies[j].attackdamage);

                        if (humans[i].life <= 0)
                        {
                            newZombie(humans[i], zombieMain);
                            i -= 1;
                            killed = true;
                            break;
                        }
                    }

                    for (int p = 0; p < polices.Count; p++)
                    {
                        polices[p].TakeDamage(zombies[j].intersectPolice(polices[p]), zombies[j].attackdamage);

                        if (polices[p].life <= 0)
                        {
                            polices.Remove(polices[p]);
                            p -= 1;
                            break;
                        }
                    }

                    if (killed)
                        break;
                }

                foreach (var p in pistols)
                {
                    for (int d = 0; d < zombies.Count; d++)
                    {
                        zombies[d].TakeDamage(zombies[d].intersectShot(p.bullet), p.damage);
                        if (zombies[d].intersectShot(p.bullet))
                        {
                            p.bullet.Location = new Point(0, 0);
                        }

                        if (zombies[d].life <= 0)
                        {
                            zombies.Remove(zombies[d]);
                            d -= 1;
                            break;
                        }
                    }

                    zombieMain.TakeDamage(zombieMain.intersectShot(p.bullet), p.damage);
                    if (zombieMain.life <= 0)
                    {
                        dead = true;
                        break;
                    }
                }
                if (dead)
                    break;


                killed = false;

                if(polices.Count <= 0 || humans.Count <= 0)
                {
                    generateBots(spawnHumans, spawmPolices, spawmPoison, form);
                    spawnHumans += 20;
                    spawmPolices += 3;
                    spawmPoison += 1;
                }

                zombie.DrunkZombie(zombies, zombieMain.x, zombieMain.y);


                pb.Refresh();
                g.Clear(Color.Transparent);
                Application.DoEvents();
            }

            while (flag)
            {
                music.Stop();
                zombieFlag.maxbrains += brain;
                form.Hide();
                flag = false;
                upgrade.go(zombieFlag, music);
            }
        };

        form.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                running = false;
                Application.Exit();
            }
            zombieMain.go(e, wall);

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
        form.Show();
    }

    public void newZombie(Human human, ZombieMain zombieMain)
    {
        pct = number.Next(zombieMain.chance, 21);
        var index = number.Next(0, zombiesImg.Count);
        if (pct == 20)
        {
            var zombie = new Zombie(human.x, human.y);
            zombie.life = zombieMain.zombiesLife;
            imageZombie = zombiesImg[index];
            zombie.putImage(imageZombie);
            humans.Remove(human);
            zombies.Add(zombie);
            bodys.Add(zombie);
            brain += 1;
        }
        else
        {
            humans.Remove(human);
            brain += 1;
        }
    }

    public void generateBots(int qttHumans, int qttPolices, int qttBrains, Form form)
    {
        for (int i = 0; i < qttHumans; i++)
        {
            human = new Human(form);
            humans.Add(human);
            bodys.Add(human);
        }

        for (int p = 0; p < qttPolices; p++)
        {
            police = new Police(form, pistols);
            polices.Add(police);
        }

        for (int p = 0; p < qttBrains; p++)
        {
            brainpoison = new BrainPoison(form);
            brainsPoison.Add(brainpoison);
        }
    }

}
