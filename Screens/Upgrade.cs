using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;

public class Upgrade
{
    List<object> methods = new List<object>();
    public void go()
    {
        var form = new Form();

        Game game = new Game();
        Random random = new Random();
        Button upgrade_card = new Button();

        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.Fixed3D;

        form.BackgroundImage = Image.FromFile("Screens/images/bg.png");
        
        // methods.Add(game.MoreLife());


        // string name = "";
        // var x = 300;

        // for (int i = 0; i < 1; i++)
        // {
        //     upgrade_card = new Button();
        //     upgrade_card.Text = game.name;
        //     upgrade_card.Width = 320;
        //     upgrade_card.Height = 130;
        //     upgrade_card.Location = new Point(x, 400);
        //     form.Controls.Add(upgrade_card);
        //     x += 500;
        // }

        Button upgrade_card2 = new Button();
        upgrade_card2.Text = "UPGRADE";
        upgrade_card2.Width = 320;
        upgrade_card2.Height = 130;
        upgrade_card2.Location = new Point(800, 400);
        form.Controls.Add(upgrade_card2);

        // // Button upgrade_card3 = new Button();
        // // upgrade_card3.Text = "UPGRADE";
        // // upgrade_card3.Width = 320;
        // // upgrade_card3.Height = 130;
        // // upgrade_card3.Location = new Point(1300, 400);
        // // form.Controls.Add(upgrade_card3);

        upgrade_card2.MouseDown += (s, e) =>
        {
            game.go();
            // game.MoreMovespeed();
            form.Hide();
        };

        // upgrade_card2.MouseDown += (s, e) =>
        // {
        //     game.go();
        //     game.MoreDamage();
        //     form.Hide();
        // };

        // upgrade_card3.MouseDown += (s, e) =>
        // {
        //     game.go();
        //     game.MoreMovespeed();
        //     form.Hide();
        // };

        form.Show();
    }
}


