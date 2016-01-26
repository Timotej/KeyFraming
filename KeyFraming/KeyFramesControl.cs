using System.Linq;
using System.Collections.Generic;
using System;

namespace KeyFraming
{
    class KeyFramesControl
    {

        public Dictionary<double, Cube> keyframes;
        public List<double> orderedTimes;
        public enum InterpolationType { Linear, NextNeighbour, Bezier };


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
            Cube lastCube = new Cube();
            for (int i = 0; i < orderedTimes.Count - 1; i++)  {
                if (orderedTimes[i] < time) {
                    keyframes.TryGetValue(orderedTimes[i], out lastCube);
                }
            }
            Cube cc = new Cube();
            cc.origin = lastCube.origin;

            keyframes.Add(time, cc);
            
            orderedTimes = keyframes.Keys.ToList();
            orderedTimes.Sort();
            var prevCube = getPreviousKeyframe(time);
            if (prevCube != null)
            {
                prevCube.CalculatePivotsByNextCube(c);
            }


        }

        public void RemoveKeyFrame(double time) {
            keyframes.Remove(time);
            orderedTimes = keyframes.Keys.ToList();
            orderedTimes.Sort();
            
        }

        public Cube getPreviousKeyframe(double currentTime)
        {
            var prevIndex = orderedTimes.FindIndex(a => a == currentTime) - 1;
            if (prevIndex >= 0)
            {
                return keyframes[orderedTimes[prevIndex]];
            }
            return null;                
        }

        public Cube getNextKeyframe(double currentTime)
        {
            var nextIndex = orderedTimes.FindIndex(a => a == currentTime) + 1;
            if (nextIndex < orderedTimes.Count)
            {
                return keyframes[orderedTimes[nextIndex]];
            }
            return null;
        }

        public List<Cube> generateAllFrames(InterpolationType interpolationType)
        {

            List<Cube> result = new List<Cube>();
            double first, second;
            for (int i = 0; i < orderedTimes.Count - 1; i++) {
                first = orderedTimes[i];
                second = orderedTimes[i + 1];
                for (double d = first; d <= second; d += 1.0f / fps) {
                    Cube c = null;
                    switch (interpolationType) {
                        case InterpolationType.Bezier:
                            c = bezierInterpolateCube(d, keyframes[second], second, keyframes[first], first);
                        break;
                        case InterpolationType.Linear:                            
                            c = linearInterpolateCube(d, keyframes[second], second, keyframes[first], first);
                            break;
                        default:
                            c = nearestInterpolateCube(d, keyframes[second], second, keyframes[first], first);
                            
                            break;

                    }

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

            c.origin = linearInterpolatePoint(actualTime, nextPosition.origin, nextTime, startingPosition.origin, startingTime);
            //c.point2 = linearInterpolatePoint(actualTime, nextPosition.point2, nextTime, startingPosition.point2, startingTime);
            //c.point3 = linearInterpolatePoint(actualTime, nextPosition.point3, nextTime, startingPosition.point3, startingTime);
            //c.point4 = linearInterpolatePoint(actualTime, nextPosition.point4, nextTime, startingPosition.point4, startingTime);

            //c.point5 = linearInterpolatePoint(actualTime, nextPosition.point5, nextTime, startingPosition.point5, startingTime);
            //c.point6 = linearInterpolatePoint(actualTime, nextPosition.point6, nextTime, startingPosition.point6, startingTime);
            //c.point7 = linearInterpolatePoint(actualTime, nextPosition.point7, nextTime, startingPosition.point7, startingTime);
            //c.point8 = linearInterpolatePoint(actualTime, nextPosition.point8, nextTime, startingPosition.point8, startingTime);
            return c;
        }



        public Cube bezierInterpolateCube(double actualTime, Cube nextPosition, double nextTime, Cube startingPosition, double startingTime)
        {
            Cube c = new Cube();
            
            c.origin = bezierInterpolate(actualTime, nextPosition.origin, nextTime, startingPosition.origin, startingTime,startingPosition.pivot1, startingPosition.pivot2);
            //c.point2 = bezierInterpolate(actualTime, nextPosition.point2, nextTime, startingPosition.point2, startingTime, startingPosition.pivot1, startingPosition.pivot2);
            //c.point3 = bezierInterpolate(actualTime, nextPosition.point3, nextTime, startingPosition.point3, startingTime, startingPosition.pivot1, startingPosition.pivot2);
            //c.point4 = bezierInterpolate(actualTime, nextPosition.point4, nextTime, startingPosition.point4, startingTime, startingPosition.pivot1, startingPosition.pivot2);

            //c.point5 = bezierInterpolate(actualTime, nextPosition.point5, nextTime, startingPosition.point5, startingTime, startingPosition.pivot1, startingPosition.pivot2);
            //c.point6 = bezierInterpolate(actualTime, nextPosition.point6, nextTime, startingPosition.point6, startingTime, startingPosition.pivot1, startingPosition.pivot2);
            //c.point7 = bezierInterpolate(actualTime, nextPosition.point7, nextTime, startingPosition.point7, startingTime, startingPosition.pivot1, startingPosition.pivot2);
            //c.point8 = bezierInterpolate(actualTime, nextPosition.point8, nextTime, startingPosition.point8, startingTime, startingPosition.pivot1, startingPosition.pivot2);
            return c;
        }

        private Point3D bezierInterpolate(double actualTime, Point3D nextPosition, double nextTime, Point3D startingPosition, double startingTime, Point3D pivot1, Point3D pivot2)
        {
            var bezierT = (actualTime - startingTime) / (nextTime - startingTime);
            
            return bezierCalc(bezierT, startingPosition, pivot1, pivot2, nextPosition);
            //var deltaR = (to['recr'] - from['recr']) * bezierT;        
        }


        private Point3D bezierCalc(double actualTime, Point3D p0, Point3D p1, Point3D p2, Point3D p3)
        {
            return Math.Pow((1 - actualTime), 3) * p0 + 3 * Math.Pow((1 - actualTime), 2) * actualTime * p1 + 3 * (1 - actualTime) * Math.Pow(actualTime, 2) * p2 + Math.Pow(actualTime, 3) * p3;
        }

        public Cube nearestInterpolateCube(double actualTime, Cube nextPosition, double nextTime, Cube startingPosition, double startingTime)
        {
            Cube c = new Cube();
            
            c.origin = nearestInterpolate(actualTime, nextPosition.origin, nextTime, startingPosition.origin, startingTime);
            //c.point2 = nearestInterpolate(actualTime, nextPosition.point2, nextTime, startingPosition.point2, startingTime);
            //c.point3 = nearestInterpolate(actualTime, nextPosition.point3, nextTime, startingPosition.point3, startingTime);
            //c.point4 = nearestInterpolate(actualTime, nextPosition.point4, nextTime, startingPosition.point4, startingTime);

            //c.point5 = nearestInterpolate(actualTime, nextPosition.point5, nextTime, startingPosition.point5, startingTime);
            //c.point6 = nearestInterpolate(actualTime, nextPosition.point6, nextTime, startingPosition.point6, startingTime);
            //c.point7 = nearestInterpolate(actualTime, nextPosition.point7, nextTime, startingPosition.point7, startingTime);
            //c.point8 = nearestInterpolate(actualTime, nextPosition.point8, nextTime, startingPosition.point8, startingTime);
            return c;
        }

        private Point3D nearestInterpolate(double actualTime, Point3D nextPosition, double nextTime, Point3D startingPosition, double startingTime)
        {
            Console.WriteLine(startingTime+" " +actualTime + " " + nextTime);
            return (actualTime == nextTime) ? nextPosition: startingPosition;            
        }


    }
}
