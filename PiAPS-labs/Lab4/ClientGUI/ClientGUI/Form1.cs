using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGUI
{
    public partial class Form1 : Form
    {
        const int matrixSizeLimit = 10;
        List<List<Label>> elements = new List<List<Label>>();

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < matrixSizeLimit; i++)
            {
                elements.Add(new List<Label>());
                for (int j = 0; j < matrixSizeLimit; j++)
                {
                    elements[i].Add(new Label());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            await RefreshTable();
        }
        async void RefreshTable()
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                tableLayoutPanel1.ColumnCount = 0;
                tableLayoutPanel1.RowCount = 0;
                tableLayoutPanel1.Controls.Clear();
                for (int i = 0; i<int.Parse(comboBox1.Text); i++)
                {
                    tableLayoutPanel1.ColumnCount++;
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    tableLayoutPanel1.RowCount++;
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                }
                for (int i = 0; i<tableLayoutPanel1.RowCount; i++)
                {
                    for (int j = 0; j<tableLayoutPanel1.ColumnCount; j++)
                    {
                        elements[i][j].Text = (i + 1).ToString() + "." + (j + 1).ToString();
                        tableLayoutPanel1.Controls.Add(elements[i][j], j, i);
                    }
                }
            }
        }
    }
}
