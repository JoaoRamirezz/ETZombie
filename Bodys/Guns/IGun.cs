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
    void Draw(Graphics g, SolidBrush color, SolidBrush colorBullet);
    void Shot(int zombieLiderX, int zombieLiderY, Form form);
    void Reload(Form form);
    void Update(int x, int y); 
}

