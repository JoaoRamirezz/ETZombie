#pragma warning disable
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Media;

public class Game
{
    Image imageGun;
    Image imagePolice;
    Image imageZombie;
    Image imageHuman;
    Image imageBrain;

    Rectangle map;

    Upgrade upgrade = new Upgrade();

    private Random number = new Random();
    private int brain = 0;

    private List<Image> zombiesImg;
    private List<Image> policesImg;
    private List<Image> humansImg;
    private List<Image> brainsImg;
    private List<Image> gunsImg;
    private Image mapImg;

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
    // private Wall wall;
    int pct;
    bool dead = false;
    bool flag = true;
    int spawmPolices = 5;
    int spawnHumans = 80;
    int spawmPoison = 5;

    public void go(Image JoeImg, ZombieMain zombieMain, SoundPlayer music)
    {
        music.PlayLooping();
        zombiesImg = new List<Image>();
        humansImg = new List<Image>();
        policesImg = new List<Image>();
        brainsImg = new List<Image>();
        gunsImg = new List<Image>();

        mapImg = Image.FromFile("imagens/mapCity.png");

        zombiesImg.Add(Image.FromFile("imagens/zombines1.png"));
        zombiesImg.Add(Image.FromFile("imagens/zombines2.png"));
        zombiesImg.Add(Image.FromFile("imagens/zombines3.png"));

        policesImg.Add(Image.FromFile("imagens/politia1.png"));
        policesImg.Add(Image.FromFile("imagens/politia2.png"));

        humansImg.Add(Image.FromFile("imagens/npc1.png"));
        humansImg.Add(Image.FromFile("imagens/npc2.png"));
        humansImg.Add(Image.FromFile("imagens/npc3.png"));
        humansImg.Add(Image.FromFile("imagens/npc4.png"));

        gunsImg.Add(Image.FromFile("imagens/gun.png"));

        brainsImg.Add(Image.FromFile("imagens/pocao.png"));

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

        PictureBox pb = new PictureBox();
        pb.Dock = DockStyle.Fill;
        form.Controls.Add(pb);
        zombieMain.putImage(JoeImg);

        var zombieFlag = zombieMain;

        brainpoison = new BrainPoison(form);
        human = new Human(form);
        police = new Police(form, pistols);
        zombie = new Zombie(human.x, human.y);

        var killed = false;

        generateBots(spawnHumans, spawmPolices, spawmPoison, form);

        var timer = new Timer();
        timer.Interval = 30;

        zombieMain.life = zombieMain.maxlife;

        Application.Idle += delegate
        {
            while (running)
            {
                putImage(mapImg);
                draw(g, form);

                if (zombieMain.life <= 0)
                    break;

                GraphicsUnit units = GraphicsUnit.Pixel;


                for (int b = 0; b < brainsPoison.Count; b++)
                {
                    // brainsPoison[b].Draw(g, new SolidBrush(Color.Purple));
                    brainsPoison[b].Draw(g);
                    if (zombieMain.takePoison(brainsPoison[b]))
                    {
                        zombieMain.life += zombieMain.cure;
                        brainsPoison.Remove(brainsPoison[b]);
                        b -= 1;
                        zombieMain.TakeDamage(true, 0);
                    }
                }

                for (int i = 0; i < humans.Count; i++)
                {
                    humans[i].Draw(g);
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

                    // polices[p].Draw(g, new SolidBrush(Color.Blue));
                    polices[p].Draw(g);
                    polices[p].ToSearchFor(zombieMain, form, zombies);
                    polices[p].Update();
                    polices[p].TakeDamage(zombieMain.intersectPolice(polices[p]), zombieMain.attackDamage);
                    if (polices[p].life <= 0)
                        polices.Remove(polices[p]);
                }

                for (int j = 0; j < zombies.Count; j++)
                {
                    zombies[j].Draw(g);

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
                    if (zombieMain.intersectShot(p.bullet))
                    {
                        p.bullet.Location = new Point(0, 0);
                    }
                    if (zombieMain.life <= 0)
                    {
                        dead = true;
                        break;
                    }
                }
                if (dead)
                    break;


                killed = false;

                if (polices.Count <= 0 || humans.Count <= 20)
                {
                    generateBots(spawnHumans, spawmPolices, spawmPoison, form);
                    spawnHumans += 20;
                    spawmPolices += 3;
                    spawmPoison += 1;
                }

                zombie.DrunkZombie(zombies, zombieMain.x, zombieMain.y);

                zombieMain.Draw(g);
                g.DrawString(zombieMain.life.ToString(), new Font("arial", 12), Brushes.White, 11, 10);
                zombieMain.Update();

                var stats = new Rectangle(0, 35, 120, 180);
                g.FillRectangle(Brushes.Black, stats);
                // form.Controls.Add(stats);

                g.DrawString("Brains:" + brain.ToString(), new Font("arial", 15), Brushes.White, 0, 40);
                g.DrawString("Horde:" + zombies.Count.ToString(), new Font("arial", 15), Brushes.White, 0, 65);

                g.DrawString("Damage: " + zombieMain.attackDamage.ToString(), new Font("arial", 13), Brushes.White, 0, 112);
                g.DrawString("Speed: " + zombieMain.movespeed.ToString(), new Font("arial", 13), Brushes.White, 0, 132);
                g.DrawString("Chance: " + zombieMain.chance.ToString(), new Font("arial", 13), Brushes.White, 0, 152);
                g.DrawString("Cure: " + zombieMain.cure.ToString(), new Font("arial", 13), Brushes.White, 0, 172);


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
        form.Show();
    }

    public void putImage(Image img)
        => mapImg = img;

    public void draw(Graphics g, Form form)
    {
        g.DrawImage(mapImg, 0, 0, form.Width, form.Height);
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
            imageHuman = humansImg[number.Next(0, humansImg.Count)];
            human.putImage(imageHuman);
            humans.Add(human);
            bodys.Add(human);
        }

        for (int p = 0; p < qttPolices; p++)
        {
            police = new Police(form, pistols);
            polices.Add(police);
            imagePolice = policesImg[number.Next(0, policesImg.Count)];
            imageGun = gunsImg[number.Next(0, gunsImg.Count)];
            polices[p].putImage(imagePolice, imageGun);
        }

        for (int p = 0; p < qttBrains; p++)
        {
            brainpoison = new BrainPoison(form);
            brainsPoison.Add(brainpoison);
            imageBrain = brainsImg[0];
            brainsPoison[p].putImage(imageBrain);
        }
    }

}
