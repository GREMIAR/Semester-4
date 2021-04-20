using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CommisVoyageur
{
    public partial class MainForm : Form
    {
        Point unsavedPoint = new Point();
        List<Point> points = new List<Point>();

        bool FreeSpace=true;
        
        public MainForm()
        {
            InitializeComponent();
            unsavedPoint.X = AreaPaint.Width / 2;
            unsavedPoint.Y = AreaPaint.Height / 2;
            AreaPaint.Refresh();
        }

        private void buttonAddPoint_Click(object sender, EventArgs e)
        {
            if (FreeSpace)
            {
                points.Add(unsavedPoint);
                comboBox3.Items.Add(points.Count);
                AreaPaint.Refresh();
                toolStripStatusLabel1.Text = "Точка успешно поставлена";
            }
            else
            {
                toolStripStatusLabel1.Text = "Нельзя установить точку";
            }
            comboBox3.Text = string.Empty;
            textBox6.Text = string.Empty;
        }

        private void AreaPaint_Click(object sender, EventArgs e)
        {
            textBox6.Text = string.Empty;
            unsavedPoint.X = Cursor.Position.X - PointToScreen(AreaPaint.Location).X - 11;
            unsavedPoint.Y = Cursor.Position.Y - PointToScreen(AreaPaint.Location).Y - 12;
            AreaPaint.Refresh();
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void buttonCalculatePath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox3.Text))
            {
                toolStripStatusLabel1.Text = string.Empty;
                NearestNeighbor nearestNeighbor = new NearestNeighbor();
                List<Point> tempPoints = new List<Point>(points);
                textBox6.Text = (Math.Round(nearestNeighbor.Greedy(tempPoints, int.Parse(comboBox3.Text)-1),2)).ToString();
                toolStripStatusLabel1.Text = "Поиск выполнен!";
                //AreaPaint.Refresh();
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            AreaPaint.Refresh();
        }
        private void AreaPaint_Paint(object sender, PaintEventArgs e)
        {
            CommisVoyageur.Paint.DrawLine(e, Color.Black, new Point(0, 0), new Point(AreaPaint.Width, 0));
            CommisVoyageur.Paint.DrawLine(e, Color.Black, new Point(0, AreaPaint.Height), new Point(AreaPaint.Width, AreaPaint.Height));
            CommisVoyageur.Paint.DrawLine(e, Color.Black, new Point(0, 0), new Point(0, AreaPaint.Height));
            CommisVoyageur.Paint.DrawLine(e, Color.Black, new Point(AreaPaint.Width, 0), new Point(AreaPaint.Width, AreaPaint.Height));
            if (FreeSpace = FreeSpaceAvailable(unsavedPoint, points))
            {
                CommisVoyageur.Paint.DrawPoint(e, Color.Black, unsavedPoint);
            }
            else
            {
                CommisVoyageur.Paint.DrawPoint(e, Color.Red, unsavedPoint);
            }
            foreach (Point point in points)
            {
                CommisVoyageur.Paint.DrawPoint(e, Color.Blue, point);
            }
        }
        public bool FreeSpaceAvailable(Point unsavedPoint, List<Point> points)
        {
            foreach (Point point in points)
            {
                if ((Math.Pow(point.X - unsavedPoint.X, 2) + Math.Pow(point.Y - unsavedPoint.Y, 2) < 1600))
                {
                    return false;
                }
            }
            return true;
        }



    }
}