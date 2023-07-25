using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Start
{
    public void go()
    {
        {
            var form = new Form();
            form.WindowState = FormWindowState.Maximized;
            form.FormBorderStyle = FormBorderStyle.Fixed3D;

            // Bitmap bmp = null;
            // Graphics g = null;

            // PictureBox pb = new PictureBox();
            // pb.Dock = DockStyle.Fill;
            // form.Controls.Add(pb);

            // Image newImage = Image.FromFile("images/gosma.jpg");

            Button play = new Button();
            play.Text = "PLAY";
            play.Width = 320;
            play.Height = 130;
            play.Location = new Point(800, 400);
            form.Controls.Add(play);

            Button settings = new Button();
            settings.Text = "SETTINGS";
            settings.Width = 320;
            settings.Height = 130;
            settings.Location = new Point(800, 550);
            form.Controls.Add(settings);

            // var tm = new Timer();
            // tm.Interval = 20;
            // tm.Tick += delegate
            // {
            //     g.Clear(Color.White);
            //     pb.Refresh();

            //     GraphicsUnit units = GraphicsUnit.Pixel;

            //     g.DrawImage(newImage, units);
            //     g.DrawImage(newImage, units);
            //     pb.Refresh();
            // };

            // form.Load += delegate
            // {
            //     bmp = new Bitmap(pb.Width, pb.Height);
            //     g = Graphics.FromImage(bmp);

            //     pb.Image = bmp;
            //     tm.Start();
            // };

            Application.Run(form);
        }
    }
}
