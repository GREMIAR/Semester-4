using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MakeReportWord
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0 || e.Row == 1)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 253, 219, 124)))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
            else if (e.Row == 3 || e.Row == 4)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 208, 117, 252)))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
          else if (e.Row == 6 || e.Row == 7)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 84, 213, 245)))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            label1.BackColor = Color.FromArgb(255, 253, 219, 124);
        }

        private void label2_Paint(object sender, PaintEventArgs e)
        {
            label2.BackColor=Color.FromArgb(255, 253, 219, 124);
        }

        private void label3_Paint(object sender, PaintEventArgs e)
        {
            label3.BackColor = Color.FromArgb(255, 208, 117, 252);
        }

        private void label4_Paint(object sender, PaintEventArgs e)
        {
            label4.BackColor = Color.FromArgb(255, 208, 117, 252);
        }

        private void label5_Paint(object sender, PaintEventArgs e)
        {
            label5.BackColor = Color.FromArgb(255, 84, 213, 245);
        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            label6.BackColor = Color.FromArgb(255, 84, 213, 245);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            tableLayoutPanel1.BackColor = Color.FromArgb(255, 50, 39, 62);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 50, 39, 62);
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            button1.BackColor = Color.FromArgb(255, 238, 230, 246);
            checkBox1.Refresh();
        }

        private void checkBox1_Paint(object sender, PaintEventArgs e)
        {
            checkBox1.BackColor = Color.FromArgb(255, 50, 39, 62);
        }

        private void checkBox1_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }
    }
}
