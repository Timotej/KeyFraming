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
        Dictionary<double, SliderPoint> sliderPoints;
        public KeyFramesControl kfc;
        public double current;
        



        public Slider(int sx, int sy, int ex, int ey, Color sc)
        {
            startx = sx;
            starty = sy;
            endx = ex;
            endy = ey;
            width = 10;
            sliderColor = sc;
            sliderPoints = new Dictionary<double, SliderPoint>();
            sliderPoints.Add(0.0f, new SliderPoint(startx - 2 * width, starty - width, true));
            sliderPoints.Add(5.0f, new SliderPoint(endx, endy - width, false));

            kfc = new KeyFramesControl();
            current = 0.0f;
        }

        public void drawSlider(Graphics g, float progress)
        {
            Pen p = new Pen(sliderColor);
            p.Width = 3;
            g.DrawLine(p, startx, starty, endx, endy);

            foreach (KeyValuePair<double, SliderPoint> entry in sliderPoints) {
                entry.Value.drawSliderPoint(g, p);
            }

            g.DrawLine(p,new Point(startx+ (int)((progress) *(endx-startx)), starty+20),new Point(startx+ (int)((progress) *(endx - startx)), starty-20));
            
            
        }

        public void addSliderPoint(int x, int y)
        {
            int nx = x - width;
            int ny = starty - width;
            if (x > startx - 2*width && x < endx+2*width && y > starty - (width / 2) && y < endy + (width / 2))
            {
                deselectAll();
                foreach (KeyValuePair<double, SliderPoint> s in sliderPoints)
                {
                    double dist = Math.Sqrt((nx - s.Value.x) * (nx - s.Value.x) + (ny - s.Value.y) * (ny - s.Value.y));
                    if (dist < s.Value.width) {
                        s.Value.selected = true;
                        current = s.Key;
                        return;
                    }
                }

                
                SliderPoint sp = new SliderPoint(nx, ny, true);
                sliderPoints.Add(x / 100f, sp);
                kfc.AddKeyFrame(x / 100f, new Cube());
                current = x / 100f;
            } 
        }

        public void removeSliderPoint(int x, int y)
        {
            int nx = x - width;
            int ny = starty - width;
            double delete = -1f;

            if (x > startx && x < endx && y > starty - (width / 2) && y < endy + (width / 2))
            {
                foreach (KeyValuePair<double, SliderPoint> s in sliderPoints)
                {
                    double dist = Math.Sqrt((nx - s.Value.x) * (nx - s.Value.x) + (ny - s.Value.y) * (ny - s.Value.y));
                    if (dist < s.Value.width)
                    {
                        delete = s.Key;
                        break;
                    }
                }
            }
            SliderPoint sp;
            bool contains = sliderPoints.TryGetValue(delete, out sp);
            if (contains)
            {
                if (sp.selected == true)
                {
                    sliderPoints[0.0f].selected = true;
                    current = 0.0f;
                }
            }
            sliderPoints.Remove(delete);
        }

        public void deselectAll()
        {
            foreach (KeyValuePair<double, SliderPoint> s in sliderPoints)
            {
                s.Value.selected = false;
            }
        }
    }
}
