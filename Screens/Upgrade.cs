using System;
using System.Media;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Upgrade
{

    int actualPrice = 0;
    string name = "";
    int x = 100;
    Random random = new Random();
    List<object> methods = new List<object>();
    public void go(ZombieMain zombie, SoundPlayer gameMusic)
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
        newzombie.movespeed = zombie.movespeed;
        newzombie.attackDamage = zombie.attackDamage;
        newzombie.maxbrains = zombie.maxbrains;
        newzombie.LifePrice = zombie.LifePrice;
        newzombie.DamagePrice = zombie.DamagePrice;
        newzombie.SpeedPrice = zombie.SpeedPrice;

        var methodInt1 = random.Next(0, 3);
        var methodInt2 = random.Next(0, 3);
        var methodInt3 = random.Next(0, 3);


        selectName(methodInt1,upgrade,newzombie);
        Button upgrade_card1 = new Button();
        upgrade_card1.Text = name + "-------" + actualPrice;
        upgrade_card1.Width = 300;
        upgrade_card1.Height = 130;
        upgrade_card1.Location = new Point(100, 400);
        form.Controls.Add(upgrade_card1);


        selectName(methodInt2,upgrade,newzombie);
        Button upgrade_card2 = new Button();
        upgrade_card2.Text = name + "-------" + actualPrice;
        upgrade_card2.Width = 300;
        upgrade_card2.Height = 130;
        upgrade_card2.Location = new Point(500, 400);
        form.Controls.Add(upgrade_card2);


        selectName(methodInt3,upgrade,newzombie);
        Button upgrade_card3 = new Button();
        upgrade_card3.Text = name + "-------" + actualPrice;
        upgrade_card3.Width = 300;
        upgrade_card3.Height = 130;
        upgrade_card3.Location = new Point(900, 400);
        form.Controls.Add(upgrade_card3);


        Button Exit = new Button();
        Exit.Text = "Go Eat Humans!!!";
        Exit.Width = 300;
        Exit.Height = 100;
        Exit.Location = new Point(500, 700);
        form.Controls.Add(Exit);

        var textbox = new TextBox();
        textbox.PointToScreen(new Point(10, 50));
        textbox.Text = newzombie.maxbrains.ToString();
        textbox.ReadOnly = true;
        textbox.Multiline = true;
        textbox.AutoSize = false;
        textbox.Size = new System.Drawing.Size(100, 50);
        textbox.Font = new Font(textbox.Font.FontFamily, 20);

        form.Controls.Add(textbox);


        upgrade_card1.MouseDown += (s, e) =>
        {
            selectUpgrade(methodInt1, upgrade, newzombie, form, game, Joe, gameMusic);
        };
        upgrade_card2.MouseDown += (s, e) =>
        {
            selectUpgrade(methodInt2, upgrade, newzombie, form, game, Joe, gameMusic);
        };
        upgrade_card3.MouseDown += (s, e) =>
        {
            selectUpgrade(methodInt3, upgrade, newzombie, form, game, Joe, gameMusic);
        };
        Exit.MouseDown += (s, e) =>
        {
            game.go(Joe, newzombie, gameMusic);
        };


        form.Show();
    }

    public void selectUpgrade(int number, Upgrades upgrade, ZombieMain newzombie, Form form, Game game, Image Joe, SoundPlayer gameMusic)
    {
        switch (number)
        {
            case 0:
                if (newzombie.maxbrains >= newzombie.LifePrice)
                {
                    newzombie.maxbrains -= newzombie.LifePrice;
                    upgrade.MoreLife(newzombie);
                    upgrade.MoreLifePrice(newzombie.LifePrice);
                    form.Hide();
                    game.go(Joe, newzombie, gameMusic);
                    newzombie.LifePrice += 10;
                }
                break;
            case 1:
                if (newzombie.maxbrains >= newzombie.DamagePrice)
                {
                    newzombie.maxbrains -= newzombie.DamagePrice;
                    upgrade.MoreDamage(newzombie);
                    upgrade.MoreDamagePrice(newzombie.DamagePrice);
                    form.Hide();
                    game.go(Joe, newzombie, gameMusic);
                    newzombie.DamagePrice += 10;
                }
                break;
            case 2:
                if (newzombie.maxbrains >= newzombie.SpeedPrice)
                {
                    newzombie.maxbrains -= newzombie.SpeedPrice;
                    upgrade.MoreSpeed(newzombie);
                    upgrade.MoreSpeedPrice(newzombie.SpeedPrice);
                    form.Hide();
                    game.go(Joe, newzombie, gameMusic);
                    newzombie.SpeedPrice += 10;
                }
                break;
        }
    }

    public string selectName(int number, Upgrades upgrade,ZombieMain newzombie)
    {
        switch (number)
        {
            case 0:
                name = upgrade.MoreLifeName();
                actualPrice = newzombie.LifePrice;
                break;
            case 1:
                name = upgrade.MoreDamageName();
                actualPrice = newzombie.DamagePrice;
                break;
            case 2:
                name = upgrade.MoreSpeedName();
                actualPrice = newzombie.SpeedPrice;
                break;
        }
        
        return "";
    }
}


