using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Start
{
    public void go()
    {
        Upgrade upgrade = new Upgrade();
        var form = new Form();

        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.Fixed3D;

        form.BackgroundImage = Image.FromFile("Screens/images/bg.png");

        int size = 25; 


        Button play = new Button();
        play.Text = "PLAY";
        play.Name = "play";
        play.Width = 320;
        play.Height = 130;
        play.Location = new Point(800, 450);
        form.Controls.Add(play);

        // Button settings = new Button();
        // settings.Text = "SETTINGS";
        // settings.Width = 320;
        // settings.Height = 130;
        // settings.Location = new Point(800, 550);
        // form.Controls.Add(settings);

        play.MouseDown += (s, e) =>
        {
            upgrade.go();
            form.Hide();
        };

        Application.Run(form);
    }
}


