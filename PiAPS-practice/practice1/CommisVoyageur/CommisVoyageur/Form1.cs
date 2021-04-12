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
        Path unsavedPath = new Path();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            unsavedPoint.X = Cursor.Position.X - PointToScreen(pictureBox1.Location).X - 7;
            unsavedPoint.Y = Cursor.Position.Y - PointToScreen(pictureBox1.Location).Y - 7;
            textBox6.Text = "(" + unsavedPoint.X.ToString() + ";" + unsavedPoint.Y.ToString() + ")";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Red, 2), unsavedPoint.X, unsavedPoint.Y, unsavedPoint.X+1, unsavedPoint.Y);
            foreach(Point point in points)
            {
                e.Graphics.DrawLine(new Pen(Color.Red, 2), point.X, point.Y, point.X+1, point.Y+1);
            }
            e.Graphics.DrawLine(new Pen(Color.Red, 2), points[unsavedPath.GetPoint1()].X, points[unsavedPath.GetPoint1()].Y, points[unsavedPath.GetPoint1()].X, points[unsavedPath.GetPoint1()].Y);
            foreach(Path path in paths)
            {
                e.Graphics.DrawLine(new Pen(Color.Red, 2), points[path.GetPoint1()].X, points[path.GetPoint1()].Y, points[path.GetPoint2()].X, points[path.GetPoint2()].Y);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            points.Add(unsavedPoint);
            paths.Add(unsavedPath);
            comboBox1.Items.Add(points.Count);
            comboBox2.Items.Add(points.Count);
            comboBox3.Items.Add(points.Count);
            comboBox4.Items.Add(points.Count);
            pictureBox1.Refresh();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int length = 0;
            Int32.TryParse(textBox3.Text, out length);
            if (length > 0)
            {
                paths.Add(new Path(int.Parse(comboBox1.Text) - 1, int.Parse(comboBox2.Text) - 1, length));
            }
        }
    }
}
