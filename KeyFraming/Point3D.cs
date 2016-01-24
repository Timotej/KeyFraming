namespace KeyFraming
{
    internal class Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }


        public Point3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point3D operator *(Point3D point, double number)
        {
            return new Point3D(point.X * number, point.Y * number, point.Z * number);
        }

        public static Point3D operator *(double number, Point3D point)
        {
            return new Point3D(point.X * number, point.Y * number, point.Z * number);
        }

        public static Point3D operator +(Point3D p0, Point3D p1)
        {
            return new Point3D(p0.X + p1.X, p0.Y + p1.Y, p0.Z + p1.Z);
        }


        public override string ToString()
        {
            return ""+X+" "+Y+" "+Z;
        }

    }
}