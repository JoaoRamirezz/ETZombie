using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


public interface IGun
{
    void Draw(Graphics g, SolidBrush color);
    void Shot(int zombieLiderX, int zombieLiderY);
    void Reload();
    void Update(int x, int y); 
}

