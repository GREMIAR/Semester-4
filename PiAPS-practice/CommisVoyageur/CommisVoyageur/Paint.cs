using System.Drawing;
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
    }
}
