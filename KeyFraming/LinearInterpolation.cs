using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyFraming
{
    class LinearInterpolation
    {
        private Dictionary<double, Point3D> keyframes = new Dictionary<double, Point3D>();
        private List<double> orderedTimes;
        private int actualKeyframe = 0;
        // private double startingTime, nextTime;
        // private Point3D startingPosition, nextPosition;

        public enum InterpolationType { linear, nextNeighbour, bezier };

        public void AddKeyFrame(double time, Point3D position)
        {
            keyframes.Add(time, position);
            orderedTimes = keyframes.Keys.OrderBy(x => x).ToList();
        }

        public void RemoveKeyFrame(double time)
        {
            keyframes.Remove(time);
            orderedTimes.Remove(time);
        }

        public void ClearAllKeyFrames()
        {
            keyframes.Clear();
        }

        public Point3D interpolate(double time, InterpolationType interpolationType)
        {
            if (actualKeyframe > orderedTimes.Count - 1)
            {
                return keyframes[orderedTimes.Last()];
            }
            var nextPosition = keyframes[orderedTimes[actualKeyframe + 1]];
            var nextTime = orderedTimes[actualKeyframe + 1];
            var startingPosition = keyframes[orderedTimes[actualKeyframe]];
            var startingTime = orderedTimes[actualKeyframe];
            if (time > nextTime)
            {
                actualKeyframe++;
            }
            switch (interpolationType)
            {
                case InterpolationType.bezier:
                    return bezierInterpolate(time, nextPosition, nextTime, startingPosition, startingTime);
                    break;
                case InterpolationType.linear:
                    return linearInterpolate(time, nextPosition, nextTime, startingPosition, startingTime);
                    break;
                case InterpolationType.nextNeighbour:
                    return nearestInterpolate(time, nextPosition, nextTime, startingPosition, startingTime);
                    break;
            }



            return new Point3D();
        }

        private Point3D bezierInterpolate(double actualTime, Point3D nextPosition, double nextTime, Point3D startingPosition, double startingTime)
        {
            //TODO
            return new Point3D();
        }



        private Point3D linearInterpolate(double actualTime, Point3D nextPosition, double nextTime, Point3D startingPosition, double startingTime)
        {
            var delta = ((actualTime - startingTime) / (nextTime - startingTime));
            var deltaX = (nextPosition.X - startingPosition.X) * delta;
            var deltaY = (nextPosition.Y - startingPosition.Y) * delta;
            var deltaZ = (nextPosition.Z - startingPosition.Z) * delta;

            return new Point3D(startingPosition.X + deltaX, startingPosition.Y + deltaY, startingPosition.Z + deltaZ);
        }

        private Point3D nearestInterpolate(double actualTime, Point3D nextPosition, double nextTime, Point3D startingPosition, double startingTime)
        {
            return nextPosition;
        }

    }

}
