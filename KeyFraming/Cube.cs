using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL4NET;

namespace KeyFraming
{
    class Cube
    {
        public double X { get { return point1.X; } set { point1.X = value;  updateCubeByPoint1(); } }
        public double Y { get { return point1.Y; } set { point1.Y = value;  updateCubeByPoint1(); } }
        public double Z { get { return point1.Z; } set { point1.Z = value;  updateCubeByPoint1(); } }

        public Point3D origin { get { return point1; } set { point1 = value; updateCubeByPoint1(); } }

        private Point3D point1;        
        private Point3D point2;
        private Point3D point3;
        private Point3D point4;
        private Point3D point5;
        private Point3D point6;
        private Point3D point7;
        private Point3D point8;
        public int size;
        public int pivotSize = 15;
       

        public Point3D pivot1;
        public Point3D pivot2;

        public Cube() {

            size = 50;

            point1 = new Point3D(0, 0, 0);
            updateCubeByPoint1();

            pivot1 = new Point3D(200,200,0);
            pivot2 = new Point3D(300,300,0);

        }
        
        public void updateCubeByPoint1()
        {
            point2 = new Point3D(point1.X+size, point1.Y, point1.Z);
            point3 = new Point3D(point1.X+size, point1.Y+size, point1.Z);
            point4 = new Point3D(point1.X, point1.Y+size, point1.Z);

            point5 = new Point3D(point1.X, point1.Y, point1.Z - size);
            point6 = new Point3D(point1.X+size, point1.Y, point1.Z - size);
            point7 = new Point3D(point1.X+size, point1.Y+size, point1.Z - size);
            point8 = new Point3D(point1.X, point1.Y+size, point1.Z - size);

            int skewx = 10;
            int skewy = 10;
            point5.X += skewx;
            point5.Y += -skewy;

            point6.X += skewx;
            point6.Y += -skewy;

            point7.X += skewx;
            point7.Y += -skewy;

            point8.X += skewx;
            point8.Y += -skewy;
        }

        public void drawCube(Graphics g, bool drawBezierPivots)
        {
            



            drawLine(point1, point2, g);
            drawLine(point2, point3, g);
            drawLine(point3, point4, g);
            drawLine(point4, point1, g);

            drawLine(point5, point6, g);
            drawLine(point6, point7, g);
            drawLine(point7, point8, g);
            drawLine(point8, point5, g);

            drawLine(point1, point5, g);
            drawLine(point2, point6, g);
            drawLine(point3, point7, g);
            drawLine(point4, point8, g);
            
           

            if (drawBezierPivots)
            {                
                drawPivots(g);
            }


        }

        public void CalculatePivotsByNextCube(Cube nextCube)
        {

            this.pivot1 = new Point3D((this.point1.X - nextCube.point1.X) / 2, (this.point1.Y - nextCube.point1.Y) / 2 - 20, (this.point1.Z - nextCube.point1.Z) / 2);
            this.pivot2 = new Point3D((this.point1.X - nextCube.point1.X) / 2, (this.point1.Y - nextCube.point1.Y) / 2 + 20, (this.point1.Z - nextCube.point1.Z) / 2);

        }        

        public void drawLine(Point3D a, Point3D b, Graphics g) {
            Pen p = new Pen(Color.Black);
            p.Width = 3;

            g.DrawLine(p, (float)a.X, (float)a.Y, (float)b.X, (float)b.Y);
        }

        public void drawPivots(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            p.Width = 3;
            if(pivot1!=null) g.DrawEllipse(p, (float)pivot1.X, (float)pivot1.Y, pivotSize, pivotSize);
            if (pivot2 != null) g.DrawEllipse(p, (float)pivot2.X, (float)pivot2.Y, pivotSize, pivotSize);
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

            int skewx = 14;
            int skewy = 12;
            point5.X += skewx;
            point5.Y += -skewy;

            point6.X += skewx;
            point6.Y += -skewy;

            point7.X += skewx;
            point7.Y += -skewy;

            point8.X += skewx;
            point8.Y += -skewy;
        }


        public bool isClickingInsideCube(double x, double y)
        {
            return (point1.X < x && point1.X + size > x && point1.Y < y && point1.Y + size > y);
        }

        public Point3D getSomePivotIfBeingClicked(double x, double y)
        {            
            if (pivot1 != null && pivot1.X < x && pivot1.X + pivotSize > x && pivot1.Y < y && pivot1.Y + pivotSize > y) return pivot1;
            if (pivot2 != null && pivot2.X < x && pivot2.X + pivotSize > x && pivot2.Y < y && pivot2.Y + pivotSize > y) return pivot2;
            return null;
        }

      

       

    }
}
