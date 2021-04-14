using System.Collections.Generic;
using System.Drawing;

namespace CommisVoyageur
{
    public class Path
    {
        int startPointIndex;
        public int StartPointIndex
        {
            get { return startPointIndex; }
            set { startPointIndex = value; }
        }
        int endPointIndex;
        public int EndPointIndex
        {
            get { return endPointIndex; }
            set { endPointIndex = value; }
        }
        Point endPointCoords;
        public Point EndPointCoords
        {
            get { return endPointCoords; }
            set { endPointCoords = value; }
        }
        int length;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public Path() { }
        public Path(int startPointIndex, int endPointIndex, Point endPointCoords, int length)
        {
            this.startPointIndex = startPointIndex;
            this.endPointIndex = endPointIndex;
            this.endPointCoords = endPointCoords;
            this.length = length;
        }
        public bool isEqualTo(Path path)
        {
            if ((this.startPointIndex == path.startPointIndex) &&(this.EndPointIndex == path.endPointIndex))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Path GetReversedPath(List<MainPoint> points)
        {
            return new Path(this.EndPointIndex, this.StartPointIndex, points[this.startPointIndex].Point, this.Length);
        }
    }

    public class MainPoint
    {
        Point point;
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }
        public List<Path> paths = new List<Path>();
        public MainPoint(Point point)
        {
            this.point = point;
        }
    }
}
