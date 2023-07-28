using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


public interface IGun
{
    void Draw(Graphics g, Image gunImg, SolidBrush colorBullet);
    void Shot(Point joeLocation, Form form);
    void Reload(Form form);
    void Update(int x, int y); 
}

