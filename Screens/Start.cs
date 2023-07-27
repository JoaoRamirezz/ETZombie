using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Start
{
    ZombieMain Joe;
    public void go()
    {
        Game game = new Game();
        Upgrade upgrade = new Upgrade();
        var form = new Form();

        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.Fixed3D;
        form.BackgroundImage = Image.FromFile("Screens/images/bg.png");

        Image JoeImg = Image.FromFile("imagens/JoeSprites.png");
        Joe = new ZombieMain();

        form.TransparencyKey = Color.AliceBlue;

        Button play = new Button();
        play.BackgroundImage = Image.FromFile("Screens/images/ZombiePlay.png");
        play.BackgroundImageLayout = ImageLayout.Center;
        play.FlatStyle = FlatStyle.Flat;
        play.BackColor = Color.Transparent;
        play.FlatAppearance.MouseDownBackColor = Color.Transparent;
        play.FlatAppearance.MouseOverBackColor = Color.Transparent;
        play.FlatAppearance.BorderSize = 0;
        play.Width = 390;
        play.Height = 390;
        play.Location = new Point(740, 350);
        form.Controls.Add(play);

        // Button settings = new Button();
        // settings.Text = "SETTINGS";
        // settings.Width = 320;
        // settings.Height = 130;
        // settings.Location = new Point(800, 550);
        // form.Controls.Add(settings);

        play.MouseDown += (s, e) =>
        {
            game.go(JoeImg, Joe);
            form.Hide();
        };

        Application.Run(form);
    }
}


