using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CommisVoyageur
{
    public partial class Form1 : Form
    {

        List<Point> points = new List<Point>();
        Point unsavedPoint = new Point();
        List<Path> paths = new List<Path>();

        bool FreeSpace=true;
        
        public Form1()
        {
            InitializeComponent();
            unsavedPoint.X = pictureBox1.Width / 2;
            unsavedPoint.Y = pictureBox1.Height / 2;
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
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            unsavedPoint.X = Cursor.Position.X - PointToScreen(pictureBox1.Location).X - 7;
            unsavedPoint.Y = Cursor.Position.Y - PointToScreen(pictureBox1.Location).Y - 7;
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(FreeSpace = FreeSpaceAvailable())
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 2), unsavedPoint.X - 2, unsavedPoint.Y - 2, 4, 4);
            }
            else
            {
                e.Graphics.DrawEllipse(new Pen(Color.Red, 2), unsavedPoint.X - 2, unsavedPoint.Y - 2, 4, 4);
            }
            foreach (Path path in paths)
            {
                e.Graphics.DrawLine(new Pen(Color.Red, 2), points[path.PointFirst].X, points[path.PointFirst].Y, points[path.PointSecond].X, points[path.PointSecond].Y);
            }
            foreach (Point point in points)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Blue, 2), point.X-2, point.Y-2, 4, 4);
            }
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                e.Graphics.DrawEllipse(new Pen(Color.Yellow, 2), points[int.Parse(comboBox1.Text) - 1].X - 2, points[int.Parse(comboBox1.Text) - 1].Y - 2, 4, 4);
            }
            if(!string.IsNullOrEmpty(comboBox2.Text))
            {
                e.Graphics.DrawEllipse(new Pen(Color.Green, 2), points[int.Parse(comboBox2.Text) - 1].X - 2, points[int.Parse(comboBox2.Text) - 1].Y - 2, 4, 4);
            }
            if (!string.IsNullOrEmpty(comboBox3.Text))
            {
                e.Graphics.DrawEllipse(new Pen(Color.Yellow, 2), points[int.Parse(comboBox3.Text) - 1].X - 2, points[int.Parse(comboBox3.Text) - 1].Y - 2, 4, 4);
            }
            if (!string.IsNullOrEmpty(comboBox4.Text))
            {
                e.Graphics.DrawEllipse(new Pen(Color.Green, 2), points[int.Parse(comboBox4.Text) - 1].X - 2, points[int.Parse(comboBox4.Text) - 1].Y - 2, 4, 4);
            }
            if (!string.IsNullOrEmpty(comboBox5.Text))
            {
                e.Graphics.DrawLine(new Pen(Color.White, 2), points[paths[int.Parse(comboBox5.Text) - 1].PointFirst].X , points[paths[int.Parse(comboBox5.Text) - 1].PointFirst].Y, points[paths[int.Parse(comboBox5.Text) - 1].PointSecond].X, points[paths[int.Parse(comboBox5.Text) - 1].PointSecond].Y);
                e.Graphics.DrawEllipse(new Pen(Color.Yellow, 2), points[paths[int.Parse(comboBox5.Text) - 1].PointFirst].X - 2, points[paths[int.Parse(comboBox5.Text) - 1].PointFirst].Y - 2, 4, 4);
                e.Graphics.DrawEllipse(new Pen(Color.Green, 2), points[paths[int.Parse(comboBox5.Text) - 1].PointSecond].X - 2, points[paths[int.Parse(comboBox5.Text) - 1].PointSecond].Y - 2, 4, 4);
            }
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
            pictureBox1.Refresh();
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
            pictureBox1.Refresh();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox5.Text = string.Empty;
            pictureBox1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            pictureBox1.Refresh();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            pictureBox1.Refresh();
            textBox1.Text = paths[int.Parse(comboBox5.Text) - 1].Length.ToString();
        }
    }
}
