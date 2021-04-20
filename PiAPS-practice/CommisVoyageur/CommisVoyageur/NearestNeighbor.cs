using System;
using System.Collections.Generic;
using System.Drawing;

namespace CommisVoyageur
{
    class NearestNeighbor
    {
        public double Greedy(List<Point> points,int indexStartPoint)
        {
            double minPath=0;
            double minDistance;
            Point testPoint = points[indexStartPoint];
            while (points.Count>0)
            {
                int delIndex=0;
                minDistance = double.MaxValue;
                for (int i=0; points.Count>i;i++)
                {
                    double distance = DistancePoint(testPoint, points[i]);
                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        delIndex = i;
                    }
                }
                Paint.DrawLine()
                testPoint = points[delIndex];
                points.RemoveAt(delIndex);
                minPath += minDistance;
            }
            return minPath;

        }
        double DistancePoint(Point first, Point second)
        {
            return Math.Sqrt(Math.Pow(second.X - first.X, 2) + Math.Pow(second.Y - first.Y,2));
        }
    }
}
