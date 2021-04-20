using System;
using System.Collections.Generic;
using System.Drawing;

namespace CommisVoyageur
{
    public class OurPoint
    {
        Point point;
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }
        public List<Path> paths = new List<Path>();
        public OurPoint(Point point)
        {
            this.point = point;
        }
        public static bool FreeSpaceAvailable(Point unsavedPoint, List<OurPoint> points)
        {
            foreach (OurPoint point in points)
            {
                if ((Math.Pow(point.Point.X - unsavedPoint.X, 2) + Math.Pow(point.Point.Y - unsavedPoint.Y, 2) < 1600))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
