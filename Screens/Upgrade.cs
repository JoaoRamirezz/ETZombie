using System;
using System.Media;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Upgrade
{
    string name;
    int x = 100;
    Random random = new Random();
    List<object> methods = new List<object>();
    public void go(ZombieMain zombie)
    {
        SoundPlayer soundElevator = new SoundPlayer("Sounds/shop.wav");
        soundElevator.Play();

        var form = new Form();
        zombie.life = zombie.maxlife;

        Game game = new Game();
        Upgrades upgrade = new Upgrades();
        ZombieMain newzombie = new ZombieMain();

        Image Joe = Image.FromFile("imagens/JoeSprites.png");

        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.Fixed3D;
        form.BackgroundImage = Image.FromFile("Screens/images/bgUpgrade-fotor.png");
        form.BackgroundImageLayout = ImageLayout.Stretch;
        newzombie.maxlife = zombie.maxlife;
        var methodInt1 = random.Next(0, 3);
        var methodInt2 = random.Next(0, 3);
        var methodInt3 = random.Next(0, 3);

        Button upgrade_card1 = new Button();
        upgrade_card1.Text = selectName(methodInt1, upgrade);
        upgrade_card1.Width = 300;
        upgrade_card1.Height = 130;
        upgrade_card1.Location = new Point(100, 400);
        form.Controls.Add(upgrade_card1);

        Button upgrade_card2 = new Button();
        upgrade_card2.Text = selectName(methodInt2, upgrade);
        upgrade_card2.Width = 300;
        upgrade_card2.Height = 130;
        upgrade_card2.Location = new Point(500, 400);
        form.Controls.Add(upgrade_card2);

        Button upgrade_card3 = new Button();
        upgrade_card3.Text = selectName(methodInt3, upgrade);
        upgrade_card3.Width = 300;
        upgrade_card3.Height = 130;
        upgrade_card3.Location = new Point(900, 400);
        form.Controls.Add(upgrade_card3);

        upgrade_card1.MouseDown += (s, e) =>
        {
            selectUpgrade(methodInt1, upgrade, newzombie);

            form.Hide();
            game.go(Joe, newzombie);
        };
        upgrade_card2.MouseDown += (s, e) =>
        {
            selectUpgrade(methodInt2, upgrade, newzombie);

            form.Hide();
            game.go(Joe, newzombie);
        };
        upgrade_card3.MouseDown += (s, e) =>
        {
            selectUpgrade(methodInt3, upgrade, newzombie);

            form.Hide();
            game.go(Joe, newzombie);
        };

        form.Show();
    }

    public void selectUpgrade(int number, Upgrades upgrade, ZombieMain newzombie)
    {
        switch (number)
        {
            case 0:
                upgrade.MoreLife(newzombie);
                break;
            case 1:
                upgrade.MoreDamage(newzombie);
                break;
            case 2:
                upgrade.MoreSpeed(newzombie);
                break;
        }
    }

    public string selectName(int number, Upgrades upgrade)
    {
        switch (number)
        {
            case 0:
                return upgrade.MoreLifeName();
            case 1:
                return upgrade.MoreDamageName();
            case 2:
                return upgrade.MoreSpeedName();
        }
        return "";
    }
}


