using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CookComputing.XmlRpc;

namespace ClientGUI
{
    public partial class Form1 : Form
    {
        XmlRpcRequest client;
        const int matrixSizeLimit = 10;
        List<List<TextBox>> elements = new List<List<TextBox>>();
        List<List<TextBox>> elementsResult = new List<List<TextBox>>();

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            for (int i = 0; i < matrixSizeLimit; i++)
            {
                elements.Add(new List<TextBox>());
                elementsResult.Add(new List<TextBox>());
                for (int j = 0; j < matrixSizeLimit; j++)
                {
                    elements[i].Add(new TextBox());
                    elementsResult[i].Add(new TextBox());

                    MatrixCellText(elements[i][j]);
                    MatrixCellText(elementsResult[i][j]);
                }
                comboBox1.Items.Add((i+1).ToString());
            }
        }
        void MatrixCellHandler(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        void RefreshMatrix(TableLayoutPanel matrix)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                matrix.Visible = false;
                int count = int.Parse(comboBox1.Text);
                matrix.ColumnCount = 0;
                matrix.RowCount = 0;
                matrix.Controls.Clear();
                for (int i = 0; i < count; i++)
                {
                    matrix.ColumnCount++;
                    matrix.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    matrix.RowCount++;
                    matrix.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                }
                for (int i = 0; i < matrix.RowCount; i++)
                {
                    for (int j = 0; j < matrix.ColumnCount; j++)
                    {
                        //elements[i][j].Text = (i + 1).ToString() + "." + (j + 1).ToString();
                        Action action = () => matrix.Controls.Add(elements[i][j]);
                        if (this.InvokeRequired)
                            Invoke(action);
                        else
                            action();
                    }
                }
                matrix.Visible = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMatrix(tableLayoutPanel1);
        }

        void MatrixCellText(TextBox txtBx)
        {
            txtBx.KeyPress += MatrixCellHandler;
            txtBx.Multiline = false;
            txtBx.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBx.Margin = new Padding(0, 0, 1, 1);
            txtBx.TextAlign = HorizontalAlignment.Center;
            txtBx.Font = new Font("Times New Roman", 14.0f, FontStyle.Bold);
        }
    }
}
