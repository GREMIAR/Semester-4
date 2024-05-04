using System;
using System.Drawing;
using System.Windows.Forms;

namespace CommisVoyageur
{
    public partial class MainForm : Form
    {
        readonly NearestNeighbor nearestNeighbor = new NearestNeighbor();
        bool FreeSpace = true;
        Point unsavedPoint;

        public MainForm()
        {
            InitializeComponent();
            unsavedPoint.X = AreaPaint.Width / 2;
            unsavedPoint.Y = AreaPaint.Height / 2;
            AreaPaint.Refresh();
        }

        void buttonAddPoint_Click(object sender, EventArgs e)
        {
            SetBluePoint();
        }

        void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            AreaPaint.Refresh();
        }

        void AreaPaint_Paint(object sender, PaintEventArgs e)
        {
            CommisVoyageur.Paint.DrawLine(e, Color.Black, new Point(0, 0), new Point(AreaPaint.Width, 0));
            CommisVoyageur.Paint.DrawLine(e, Color.Black, new Point(0, AreaPaint.Height),
                new Point(AreaPaint.Width, AreaPaint.Height));
            CommisVoyageur.Paint.DrawLine(e, Color.Black, new Point(0, 0), new Point(0, AreaPaint.Height));
            CommisVoyageur.Paint.DrawLine(e, Color.Black, new Point(AreaPaint.Width, 0),
                new Point(AreaPaint.Width, AreaPaint.Height));
            if (FreeSpace = nearestNeighbor.FreeSpaceAvailable(unsavedPoint))
            {
                CommisVoyageur.Paint.DrawPoint(e, Color.Black, unsavedPoint);
            }
            else
            {
                CommisVoyageur.Paint.DrawPoint(e, Color.Red, unsavedPoint);
            }

            foreach (Point point in nearestNeighbor.GetPoints())
            {
                CommisVoyageur.Paint.DrawPoint(e, Color.Blue, point);
            }

            for (int i = 0; i < nearestNeighbor.GetpointsSorted().Count - 1; i++)
            {
                CommisVoyageur.Paint.DrawArrow(e, Color.Black, nearestNeighbor.GetpointsSorted()[i],
                    nearestNeighbor.GetpointsSorted()[i + 1]);
            }

            if (nearestNeighbor.GetpointsSorted().Count > 2)
            {
                CommisVoyageur.Paint.DrawArrow(e, Color.Black,
                    nearestNeighbor.GetpointsSorted()[nearestNeighbor.GetpointsSorted().Count - 1],
                    nearestNeighbor.GetpointsSorted()[0]);
                CommisVoyageur.Paint.BigRedPoint(e, Color.Red, nearestNeighbor.GetpointsSorted()[0]);
            }
        }

        void AreaPaint_MouseClick(object sender, MouseEventArgs e)
        {
            unsavedPoint.X = Cursor.Position.X - PointToScreen(AreaPaint.Location).X - 11;
            unsavedPoint.Y = Cursor.Position.Y - PointToScreen(AreaPaint.Location).Y - 12;
            AreaPaint.Refresh();
            if (e.Button == MouseButtons.Right)
            {
                SetBluePoint();
            }
        }

        void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        void SetBluePoint()
        {
            if (FreeSpace)
            {
                nearestNeighbor.AddPoints(unsavedPoint);
                comboBox3.Items.Add(nearestNeighbor.GetPoints().Count);
                AreaPaint.Refresh();
                toolStripStatusLabel1.Text = "Точка успешно поставлена";
                Search();
            }
            else
            {
                toolStripStatusLabel1.Text = "Нельзя установить точку";
            }
        }

        void Search()
        {
            if (!string.IsNullOrEmpty(comboBox3.Text) && nearestNeighbor.GetPoints().Count > 2)
            {
                toolStripStatusLabel1.Text = string.Empty;
                textBox6.Text = Math.Round(nearestNeighbor.Greedy(int.Parse(comboBox3.Text) - 1), 2).ToString();
                toolStripStatusLabel1.Text = "Поиск выполнен!";
                AreaPaint.Refresh();
            }
        }
    }
}