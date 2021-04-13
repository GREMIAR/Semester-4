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
        }

        private void buttonAddPoint_Click(object sender, EventArgs e)
        {
            if (FreeSpace)
            {
                points.Add(new MainPoint(unsavedPoint));
                comboBox1.Items.Add(points.Count);
                comboBox2.Items.Add(points.Count);
                comboBox3.Items.Add(points.Count);
                comboBox4.Items.Add(points.Count);
                AreaPaint.Refresh();
                toolStripStatusLabel1.Text = "Точка успешно поставлена";
            }
            else
            {
                toolStripStatusLabel1.Text = "Нельзя установить точку";
            }
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            textBoxDistance.Text = string.Empty;
            textBox1.Text = string.Empty;
        }

        private void AreaPaint_Click(object sender, EventArgs e)
        {
            unsavedPoint.X = Cursor.Position.X - PointToScreen(AreaPaint.Location).X - 7;
            unsavedPoint.Y = Cursor.Position.Y - PointToScreen(AreaPaint.Location).Y - 7;
            AreaPaint.Refresh();
        }

        private void buttonAddPath_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(textBoxDistance.Text) || string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text)) && (comboBox1.Text != comboBox2.Text))
            {
                int firstPoint = int.Parse(comboBox1.Text) - 1;
                int secondPoint = int.Parse(comboBox2.Text) - 1;
                if (!AlreadyExists(firstPoint, secondPoint))
                {
                    points[firstPoint].paths.Add(new Path(secondPoint, points[secondPoint].Point, int.Parse(textBoxDistance.Text)));
                    if (checkBox1.Checked)
                    {
                        points[secondPoint].paths.Add(new Path(firstPoint, points[firstPoint].Point, int.Parse(textBoxDistance.Text)));
                    }
                    comboBox5.Items.Add((firstPoint + 1) + "-" + (secondPoint + 1));
                    comboBox1.Text = string.Empty;
                    comboBox2.Text = string.Empty;
                    textBoxDistance.Text = string.Empty;
                    textBox1.Text = string.Empty;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Такой путь уже есть!";
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Не все пункты заполнены или введены не верные даные";
            }
            AreaPaint.Refresh();
        }

        public bool AlreadyExists(int firstPoint, int secondPoint)
        {
            foreach (Path path in points[firstPoint].paths)
            {
                if(path.IndexPoint == secondPoint)
                {
                    return true;
                }
            }
            return false;
        }

        private void textBoxDistance_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBoxAddPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Подготовка построения графа от: " + comboBox1.Text + " до: " + comboBox2.Text + " с размером: " + textBoxDistance.Text;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            AreaPaint.Refresh();
        }

        private void comboBoxCalculatePath_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Подготовка расчёта кратчайшего пути от: " + comboBox3.Text + " до: " + comboBox4.Text;
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox5.Text = string.Empty;
            AreaPaint.Refresh();
        }

        private void buttonCalculatePath_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Поиск выполнен!";
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            AreaPaint.Refresh();
        }

        private void comboBoxPathInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Поиск информации о пути";
            if(comboBox5.Text!=string.Empty)
            {
                string[] str = comboBox5.Text.Split('-');
                int indexFirstPoint = int.Parse(str[0]) - 1;
                int indexSecondPoint = int.Parse(str[1]) - 1;
                foreach (Path path in points[indexFirstPoint].paths)
                {
                    if (path.IndexPoint == indexSecondPoint)
                    {
                        textBox1.Text = "Растояние = " + path.Length;
                        break;
                    }
                }
                comboBox1.Text = string.Empty;
                comboBox2.Text = string.Empty;
                comboBox3.Text = string.Empty;
                comboBox4.Text = string.Empty;
                AreaPaint.Refresh();
            }
        }
    }
}
