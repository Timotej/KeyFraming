using System.Linq;
using System.Collections.Generic;

namespace KeyFraming
{
    class KeyFramesControl
    {

        public Dictionary<double, Cube> keyframes;
        public List<double> orderedTimes;
        
        public int fps = 30;

        public KeyFramesControl()
        {
            keyframes = new Dictionary<double, Cube>();
            keyframes.Add(0.0f, new Cube());
            keyframes.Add(5.0f, new Cube());
            orderedTimes = keyframes.Keys.ToList();
            orderedTimes.Sort();
        }

        public void AddKeyFrame(double time, Cube c)
        {
            keyframes.Add(time, c);
            orderedTimes = keyframes.Keys.ToList();
            orderedTimes.Sort();
            
        }

        public List<Cube> generateAllFrames()
        {
            List<Cube> result = new List<Cube>();
            double first, second;
            for (int i = 0; i < orderedTimes.Count - 1; i++) {
                first = orderedTimes[i];
                second = orderedTimes[i + 1];
                for (double d = first; d < second; d += 1.0f / fps) {
                    Cube c = linearInterpolateCube(d, keyframes[second], second, keyframes[first], first);
                    result.Add(c);
                }
            }
            return result;
        }

        public Point3D linearInterpolatePoint(double actualTime, Point3D nextPosition, double nextTime, Point3D startingPosition, double startingTime)
        {
            var delta = ((actualTime - startingTime) / (nextTime - startingTime));
            var deltaX = (nextPosition.X - startingPosition.X) * delta;
            var deltaY = (nextPosition.Y - startingPosition.Y) * delta;
            var deltaZ = (nextPosition.Z - startingPosition.Z) * delta;

            return new Point3D(startingPosition.X + deltaX, startingPosition.Y + deltaY, startingPosition.Z + deltaZ);
        }

        public Cube linearInterpolateCube(double actualTime, Cube nextPosition, double nextTime, Cube startingPosition, double startingTime)
        {
            Cube c = new Cube();

            c.point1 = linearInterpolatePoint(actualTime, nextPosition.point1, nextTime, startingPosition.point1, startingTime);
            c.point2 = linearInterpolatePoint(actualTime, nextPosition.point2, nextTime, startingPosition.point2, startingTime);
            c.point3 = linearInterpolatePoint(actualTime, nextPosition.point3, nextTime, startingPosition.point3, startingTime);
            c.point4 = linearInterpolatePoint(actualTime, nextPosition.point4, nextTime, startingPosition.point4, startingTime);

            c.point5 = linearInterpolatePoint(actualTime, nextPosition.point5, nextTime, startingPosition.point5, startingTime);
            c.point6 = linearInterpolatePoint(actualTime, nextPosition.point6, nextTime, startingPosition.point6, startingTime);
            c.point7 = linearInterpolatePoint(actualTime, nextPosition.point7, nextTime, startingPosition.point7, startingTime);
            c.point8 = linearInterpolatePoint(actualTime, nextPosition.point8, nextTime, startingPosition.point8, startingTime);
            return c;
        }

        
    }
}
