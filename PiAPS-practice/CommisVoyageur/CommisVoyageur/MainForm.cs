using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CommisVoyageur
{
    public partial class MainForm : Form
    {
        List<Point> points = new List<Point>();
        Point unsavedPoint = new Point();
        List<Path> paths = new List<Path>();

        bool FreeSpace=true;
        
        public MainForm()
        {
            InitializeComponent();
            unsavedPoint.X = AreaPaint.Width / 2;
            unsavedPoint.Y = AreaPaint.Height / 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(FreeSpace)
            {
                points.Add(unsavedPoint);
                comboBox1.Items.Add(points.Count);
                comboBox2.Items.Add(points.Count);
                comboBox3.Items.Add(points.Count);
                comboBox4.Items.Add(points.Count);
                AreaPaint.Refresh();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            unsavedPoint.X = Cursor.Position.X - PointToScreen(AreaPaint.Location).X - 7;
            unsavedPoint.Y = Cursor.Position.Y - PointToScreen(AreaPaint.Location).Y - 7;
            AreaPaint.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(FreeSpace = FreeSpaceAvailable())
            {
                DrawPoint(e, Color.Black, unsavedPoint);
            }
            else
            {
                DrawPoint(e, Color.Red, unsavedPoint);
            }
            foreach (Path path in paths)
            {
                DrawLine(e, Color.Red, path.PointFirst, path.PointSecond);
            }
            foreach (Point point in points)
            {
                DrawPoint(e, Color.Blue, point);
            }
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                DrawPoint(e, Color.Yellow, int.Parse(comboBox1.Text) - 1);
            }
            else if(!string.IsNullOrEmpty(comboBox3.Text))
            {
                DrawPoint(e, Color.Yellow, int.Parse(comboBox3.Text) - 1);
            }
            if (!string.IsNullOrEmpty(comboBox2.Text))
            {
                DrawPoint(e, Color.Green, int.Parse(comboBox2.Text) - 1);
            }
            else if (!string.IsNullOrEmpty(comboBox4.Text))
            {
                DrawPoint(e, Color.Green, int.Parse(comboBox4.Text) - 1);
            }
            if (!string.IsNullOrEmpty(comboBox5.Text))
            {
                DrawLine(e, Color.White, paths[int.Parse(comboBox5.Text) - 1].PointFirst, paths[int.Parse(comboBox5.Text) - 1].PointSecond);
                DrawPoint(e, Color.Yellow, paths[int.Parse(comboBox5.Text) - 1].PointFirst);
                DrawPoint(e, Color.Green, paths[int.Parse(comboBox5.Text) - 1].PointSecond);
            }
        }

        public void DrawPoint(PaintEventArgs e,Color color,int index)
        {
            e.Graphics.DrawEllipse(new Pen(color, 2), points[index].X - 2, points[index].Y - 2, 4, 4);
        }

        public void DrawPoint(PaintEventArgs e, Color color, Point point)
        {
            e.Graphics.DrawEllipse(new Pen(color, 2), point.X - 2, point.Y - 2, 4, 4);
        }
        public void DrawLine(PaintEventArgs e, Color color, int indexFirst, int indexSecond)
        {
            e.Graphics.DrawLine(new Pen(color, 2), points[indexFirst].X, points[indexFirst].Y, points[indexSecond].X, points[indexSecond].Y);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(textBox3.Text)|| string.IsNullOrEmpty(comboBox1.Text)|| string.IsNullOrEmpty(comboBox2.Text))&&(comboBox1.Text != comboBox2.Text))
            { 
                int firstPoint = int.Parse(comboBox1.Text) - 1;
                int secondPoint = int.Parse(comboBox2.Text) - 1;
                if(!AlreadyExists(firstPoint, secondPoint))
                {
                    paths.Add(new Path(firstPoint, secondPoint, int.Parse(textBox3.Text),checkBox1.Checked));
                    comboBox5.Items.Add(paths.Count);
                }
            }
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            AreaPaint.Refresh();
        }

        public bool AlreadyExists(int firstPoint, int secondPoint)
        {
            foreach (Path path in paths)
            {
                if((path.PointFirst==firstPoint&&path.PointSecond==secondPoint)||(path.PointFirst == secondPoint && path.PointSecond == firstPoint))
                {
                    return true;
                }
            }
            return false;
        }

        public bool FreeSpaceAvailable()
        {
            foreach (Point point in points)
            {
                if ((Math.Pow(point.X- unsavedPoint.X, 2) + Math.Pow(point.Y- unsavedPoint.Y, 2) < 1600))
                {
                    return false;
                }
            }
            return true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58)&&e.KeyChar!=8)
            {
                e.Handled = true;
            }
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            AreaPaint.Refresh();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox5.Text = string.Empty;
            AreaPaint.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            AreaPaint.Refresh();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            AreaPaint.Refresh();
            textBox1.Text = "Растояние = " + paths[int.Parse(comboBox5.Text) - 1].Length.ToString()+"; От точки: "+ paths[int.Parse(comboBox5.Text) - 1].PointFirst + ";До точки: "+ paths[int.Parse(comboBox5.Text) - 1].PointSecond;
        }
    }
}
