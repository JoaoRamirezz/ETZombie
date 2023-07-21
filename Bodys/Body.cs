using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


public interface IBody
{
    void Draw(Graphics g, SolidBrush color);
    void Update(); 
}

