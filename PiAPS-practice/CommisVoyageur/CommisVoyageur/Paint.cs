using System;
using System.Drawing;
using System.Windows.Forms;

namespace CommisVoyageur
{
    public partial class MainForm : Form
    {
        private void AreaPaint_Paint(object sender, PaintEventArgs e)
        {

            DrawLine(e, Color.Black, new Point(0, 0), new Point(AreaPaint.Width, 0));
            DrawLine(e, Color.Black, new Point(0, AreaPaint.Height), new Point(AreaPaint.Width, AreaPaint.Height));
            DrawLine(e, Color.Black, new Point(0, 0), new Point(0, AreaPaint.Height));
            DrawLine(e, Color.Black, new Point(AreaPaint.Width, 0), new Point(AreaPaint.Width, AreaPaint.Height));
            if (FreeSpace = FreeSpaceAvailable())
            {
                DrawPoint(e, Color.Black, unsavedPoint);
            }
            else
            {
                DrawPoint(e, Color.Red, unsavedPoint);
            }
            foreach (MainPoint point in points)
            {
                foreach (Path path in point.paths)
                {
                    DrawLine(e, Color.Black, point.Point, path.PointSecond);
                }
                DrawPoint(e, Color.Blue, point.Point);
            }
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                DrawPoint(e, Color.Orange, points[int.Parse(comboBox1.Text) - 1].Point);
            }
            else if (!string.IsNullOrEmpty(comboBox3.Text))
            {
                DrawPoint(e, Color.Orange, points[int.Parse(comboBox3.Text) - 1].Point);
            }
            if (!string.IsNullOrEmpty(comboBox2.Text))
            {
                DrawPoint(e, Color.Green, points[int.Parse(comboBox2.Text) - 1].Point);
            }
            else if (!string.IsNullOrEmpty(comboBox4.Text))
            {
                DrawPoint(e, Color.Green, points[int.Parse(comboBox4.Text) - 1].Point);
            }
            if (!string.IsNullOrEmpty(comboBox5.Text))
            {
                string[] str = comboBox5.Text.Split('-');
                int indexFirstPoint = int.Parse(str[0]) - 1;
                int indexSecondPoint = int.Parse(str[1]) - 1;
                DrawLine(e, Color.Red, points[indexFirstPoint].Point, points[indexSecondPoint].Point);
                DrawPoint(e, Color.Orange, points[indexFirstPoint].Point);
                DrawPoint(e, Color.Green, points[indexSecondPoint].Point);
            }
            if (!string.IsNullOrEmpty(textBox6.Text))
            {
                foreach (int tes in test1)
                {
                    DrawPoint(e, Color.Black, points[tes].Point);
                }
            }
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            DrawPoint(e, Color.Orange, new Point(label1.Width / 2 - 30, label1.Height / 2));
        }

        private void label2_Paint(object sender, PaintEventArgs e)
        {
            DrawPoint(e, Color.Green, new Point(label2.Width / 2 - 25, label2.Height / 2));
        }

        private void label4_Paint(object sender, PaintEventArgs e)
        {
            DrawPoint(e, Color.Orange, new Point(label1.Width / 2 - 30, label1.Height / 2));
        }

        private void label5_Paint(object sender, PaintEventArgs e)
        {
            DrawPoint(e, Color.Green, new Point(label2.Width / 2 - 25, label2.Height / 2));
        }

        public void DrawPoint(PaintEventArgs e, Color color, Point point)
        {
            e.Graphics.DrawEllipse(new Pen(color, 2), point.X - 2, point.Y - 2, 4, 4);
        }

        public void DrawLine(PaintEventArgs e, Color color, Point First, Point Second)
        {
            e.Graphics.DrawLine(new Pen(color, 2), First.X, First.Y, Second.X, Second.Y);

        }

        public bool FreeSpaceAvailable()
        {
            foreach (MainPoint point in points)
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
