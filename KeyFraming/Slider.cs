using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KeyFraming
{

    class Slider
    {

        int startx;
        int starty;
        int endx;
        int endy;
        int width;
        Color sliderColor;
        List<SliderPoint> sliderPoints;

        public Slider(int sx, int sy, int ex, int ey, Color sc)
        {
            startx = sx;
            starty = sy;
            endx = ex;
            endy = ey;
            width = 10;
            sliderColor = sc;
            sliderPoints = new List<SliderPoint>();
            sliderPoints.Add(new SliderPoint(startx - 2 * width, starty - width, true));
            sliderPoints.Add(new SliderPoint(endx, endy - width, false));
        }

        public void drawSlider(Graphics g)
        {
            Pen p = new Pen(sliderColor);
            p.Width = 3;
            g.DrawLine(p, startx, starty, endx, endy);

            foreach (SliderPoint sp in sliderPoints) {
                sp.drawSliderPoint(g, p);
            }
            
        }

        public void addSliderPoint(int x, int y)
        {
            int nx = x - width;
            int ny = starty - width;
            if (x > startx - 2*width && x < endx+2*width && y > starty - (width / 2) && y < endy + (width / 2))
            {
                deselectAll();
                foreach (SliderPoint s in sliderPoints)
                {
                    double dist = Math.Sqrt((nx - s.x) * (nx - s.x) + (ny - s.y) * (ny - s.y));
                    if (dist < s.width) {
                        s.selected = true;
                        return;
                    }
                }

                
                SliderPoint sp = new SliderPoint(nx, ny, true);
                sliderPoints.Add(sp);
                sliderPoints.Sort();
            } 
        }

        public void removeSliderPoint(int x, int y)
        {
            int nx = x - width;
            int ny = starty - width;
            SliderPoint delete = null;

            if (x > startx && x < endx && y > starty - (width / 2) && y < endy + (width / 2))
            {
                foreach (SliderPoint s in sliderPoints)
                {
                    double dist = Math.Sqrt((nx - s.x) * (nx - s.x) + (ny - s.y) * (ny - s.y));
                    if (dist < s.width)
                    {
                        delete = s;
                        break;
                    }
                }
            }
            sliderPoints.Remove(delete);
        }

        public void deselectAll()
        {
            foreach (SliderPoint sp in sliderPoints)
            {
                sp.selected = false;
            }
        }
    }
}
