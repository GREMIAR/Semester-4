using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CommisVoyageur
{
    public partial class MainForm : Form
    {
        Point unsavedPoint = new Point();
        List<MainPoint> points = new List<MainPoint>();

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
                points.Add(new MainPoint(unsavedPoint));
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
                //toolStripStatusLabel1.Text = "Поиск выполнен!"; toolStripStatusLabel1.Text = "Поиск выполнен!";
                comboBox3.Text = string.Empty;
                AreaPaint.Refresh();
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
            if (FreeSpace = MainPoint.FreeSpaceAvailable(unsavedPoint, points))
            {
                CommisVoyageur.Paint.DrawPoint(e, Color.Black, unsavedPoint);
            }
            else
            {
                CommisVoyageur.Paint.DrawPoint(e, Color.Red, unsavedPoint);
            }
            foreach (MainPoint point in points)
            {
                foreach (Path path in point.paths)
                {
                    CommisVoyageur.Paint.DrawLine(e, Color.Black, point.Point, path.EndPointCoords);
                }
                CommisVoyageur.Paint.DrawPoint(e, Color.Blue, point.Point);
            }
        }
        

        
    }
}