using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyFraming
{
    class Cube
    {
        public Point3D point1;
        public Point3D point2;
        public Point3D point3;
        public Point3D point4;
        public Point3D point5;
        public Point3D point6;
        public Point3D point7;
        public Point3D point8;
        public int size;

        public Cube() {

            size = 50;

            point1 = new Point3D(0, 0, 0);
            point2 = new Point3D(size, 0, 0);
            point3 = new Point3D(size, size, 0);
            point4 = new Point3D(0, size, 0);

            point5 = new Point3D(0, 0, -size);
            point6 = new Point3D(size, 0, -size);
            point7 = new Point3D(size, size, -size);
            point8 = new Point3D(0, size, -size);

            
        }

        public void drawCube(Graphics g)
        {
            drawLine(point1, point2, g);
            drawLine(point2, point3, g);
            drawLine(point3, point4, g);
            drawLine(point4, point1, g);

            drawLine(point5, point6, g);
            drawLine(point6, point7, g);
            drawLine(point7, point8, g);
            drawLine(point8, point1, g);

            drawLine(point1, point5, g);
            drawLine(point2, point6, g);
            drawLine(point3, point7, g);
            drawLine(point4, point8, g);

        }

        public void drawLine(Point3D a, Point3D b, Graphics g) {
            Pen p = new Pen(Color.Black);
            p.Width = 3;

            g.DrawLine(p, (float)a.X, (float)a.Y, (float)b.X, (float)b.Y);
        }

        public void setNewOrigin(int x, int y)
        {
            point1 = new Point3D(x, y, 0);
            point2 = new Point3D(x + size, y, 0);
            point3 = new Point3D(x + size, y + size, 0);
            point4 = new Point3D(x, y + size, 0);

            point5 = new Point3D(x, y, -size);
            point6 = new Point3D(x + size, y, -size);
            point7 = new Point3D(x + size, y + size, -size);
            point8 = new Point3D(x, y + size, -size);
        }

    }
}
