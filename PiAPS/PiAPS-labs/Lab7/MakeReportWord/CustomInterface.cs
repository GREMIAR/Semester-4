﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MakeReportWord
{
    public partial class CustomInterface : Form
    {
        public CustomInterface()
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

        private void checkBox1_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            MakeReport report = new MakeReport();
            string faculty = comboBox1.Text;
            string numberLab = maskedTextBox1.Text;
            string theme = textBox1.Text;
            string discipline = textBox2.Text;
            string professor = textBox3.Text;
            string year = textBox4.Text;
            await Task.Run(() => report.CreateReport(faculty, numberLab, theme, discipline, professor, year));
            if (checkBox1.Checked)
            {
                Application.Exit();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 50, 39, 62);
            label1.BackColor = Color.FromArgb(255, 253, 219, 124);
            label2.BackColor = Color.FromArgb(255, 253, 219, 124);
            label3.BackColor = Color.FromArgb(255, 208, 117, 252);
            label4.BackColor = Color.FromArgb(255, 208, 117, 252);
            label5.BackColor = Color.FromArgb(255, 84, 213, 245);
            label5.BackColor = Color.FromArgb(255, 84, 213, 245);
            button1.BackColor = Color.FromArgb(255, 238, 230, 246);
            checkBox1.BackColor = Color.FromArgb(255, 50, 39, 62);
            tableLayoutPanel1.BackColor = Color.FromArgb(255, 50, 39, 62);
            label6.BackColor = Color.FromArgb(255, 84, 213, 245);
            checkBox1.Refresh();
            label1.Focus();
        }
    }
}
