using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KeyFraming
{
    class SliderPoint : IComparable
    {
        public int x;
        public int y;
        public int width;
        public bool selected { get; set; }
        
        public SliderPoint(int nx, int ny, bool select)
        {
            x = nx;
            y = ny;
            selected = select;
            width = 10;
            
        }

        public void drawSliderPoint(Graphics g, Pen p) {
            if (selected)
            {
                g.FillEllipse(Brushes.Red, x, y, 2 * width, 2 * width);
            }
            else
            {
                g.FillEllipse(Brushes.White, x, y, 2 * width, 2 * width);
            }
            g.DrawEllipse(p, x, y, 2 * width, 2 * width);
        }

        public int CompareTo(object obj)
        {
            SliderPoint sp = obj as SliderPoint; 
            return x.CompareTo(sp.x);
        }
    }
}
