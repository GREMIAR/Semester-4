using System.Collections.Generic;
using System.Drawing;

namespace CommisVoyageur
{
    class Path
    { 
        int indexPoint;
        public int IndexPoint
        {
            get { return indexPoint; }
            set { indexPoint = value; }
        }
        Point pointSecond;
        public Point PointSecond
        {
            get { return pointSecond; }
            set { pointSecond = value; }
        }
        int length;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public Path() { }
        public Path(int indexPoint, Point pointSecond, int length)
        {
            this.indexPoint = indexPoint;
            this.pointSecond = pointSecond;
            this.length = length;
        }
    }

    class MainPoint
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
