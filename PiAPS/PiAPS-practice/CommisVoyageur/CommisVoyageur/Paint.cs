﻿using System.Drawing;
using System.Windows.Forms;

namespace CommisVoyageur
{
    class Paint
    {
        public static void DrawPoint(PaintEventArgs e, Color color, Point point)
        {
            e.Graphics.DrawEllipse(new Pen(color, 2), point.X - 2, point.Y - 2, 4, 4);
        }

        public static void DrawLine(PaintEventArgs e, Color color, Point First, Point Second)
        {
            e.Graphics.DrawLine(new Pen(color, 2), First.X, First.Y, Second.X, Second.Y);
        }
        public static void DrawArrow(PaintEventArgs e, Color color, Point First, Point Second)
        {
            e.Graphics.DrawLine(new Pen(color, 2), First.X, First.Y, Second.X, Second.Y);
            Point tmpPoint = new Point((First.X + Second.X) / 2, (First.Y + Second.Y) / 2);
            tmpPoint = new Point((tmpPoint.X + Second.X) / 2, (tmpPoint.Y + Second.Y) / 2);
            e.Graphics.DrawEllipse(new Pen(Color.Green, 4), tmpPoint.X - 3, tmpPoint.Y - 3, 6, 6);
        }
        public static void BigRedPoint(PaintEventArgs e, Color color, Point point)
        {
            e.Graphics.DrawEllipse(new Pen(color, 5), point.X - 5, point.Y - 5, 10, 10);
        }
    }
}
