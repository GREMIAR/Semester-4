using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;



namespace CommisVoyageur
{
    public partial class Form1 : Form
    {

        List<Point> points = new List<Point>();
        Point unsavedPoint = new Point();
        List<Path> paths = new List<Path>();
        
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            points.Add(unsavedPoint);
            comboBox1.Items.Add(points.Count);
            comboBox2.Items.Add(points.Count);
            comboBox3.Items.Add(points.Count);
            comboBox4.Items.Add(points.Count);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            unsavedPoint.X = Cursor.Position.X - PointToScreen(pictureBox1.Location).X - 7;
            unsavedPoint.Y = Cursor.Position.Y - PointToScreen(pictureBox1.Location).Y - 7;
            textBox6.Text = "(" + unsavedPoint.X.ToString() + ";" + unsavedPoint.Y.ToString() + ")";
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(new Pen(Color.Black, 2), unsavedPoint.X-2, unsavedPoint.Y-2, 4, 4);
            foreach (Path path in paths)
            {
                e.Graphics.DrawLine(new Pen(Color.Red, 2), points[path.PointFirst].X, points[path.PointFirst].Y, points[path.PointSecond].X, points[path.PointSecond].Y);
            }
            foreach (Point point in points)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Blue, 2), point.X-2, point.Y-2, 4, 4);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox3.Text)&& !string.IsNullOrEmpty(comboBox1.Text)&& !string.IsNullOrEmpty(comboBox2.Text))
            {
                paths.Add(new Path(int.Parse(comboBox1.Text) - 1, int.Parse(comboBox2.Text) - 1, int.Parse(textBox3.Text)));
                pictureBox1.Refresh();
            }
        }
        public bool AlreadyExists()
        {
            foreach (Path path in paths)
            {

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
    }
}
