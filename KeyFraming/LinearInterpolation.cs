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
        private Dictionary<double, Point> keyframes = new Dictionary<double, Point>();
        private List<double> orderedTimes;
        private int actualKeyframe = 0;

        public enum InterpolationType { linear, nextNeighbour, bezier};

        public void AddKeyFrame(double time, Point position)
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

        public Point interpolate(double time, InterpolationType interpolationType)
        {
            if(actualKeyframe > orderedTimes.Count-1)
            {
                return keyframes[orderedTimes.Last()];
            }
            var nextPosition = keyframes[orderedTimes[actualKeyframe+1]];
            var nextTime = orderedTimes[actualKeyframe + 1];
            var startingPosition = keyframes[orderedTimes[actualKeyframe]];
            var startingTime = orderedTimes[actualKeyframe];
            if(time > nextTime)
            {
                actualKeyframe++;
            }
            switch (interpolationType)
            {
                case InterpolationType.bezier:
                    return bezierInterpolate();
                    break;
                case InterpolationType.linear:
                    return linearInterpolate();
                    break;
                case InterpolationType.nextNeighbour:
                    break;                    
            }



            return null;
        }

        private Point bezierInterpolate()
        {
            
        }

    }

    private Point linearInterpolate()
        {
           
        }

        private Point nearestInterpolate()
        {
          
        }
        
    
    
}
