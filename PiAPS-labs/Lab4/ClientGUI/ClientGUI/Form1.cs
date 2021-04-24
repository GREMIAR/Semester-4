using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Nwc.XmlRpc;
namespace ClientGUI
{
    public partial class Form1 : Form
    {
        XmlRpcRequest client;
        String host = "http://127.0.0.1:8888";
        const int matrixSizeLimit = 10;
        List<List<TextBox>> elements = new List<List<TextBox>>();
        List<List<TextBox>> elementsResult = new List<List<TextBox>>();

        public Form1()
        {
            client = new XmlRpcRequest();
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
            if ((e.KeyChar <= 47 || e.KeyChar >= 59) && e.KeyChar != 8 && e.KeyChar != 45)
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                for (int i = 0; i < int.Parse(comboBox1.Text); i++)
                {
                    for (int j = 0; j < int.Parse(comboBox1.Text); j++)
                    {
                        if (!string.IsNullOrEmpty(elements[i][j].Text))
                        {
                            SendToServer("SetCell", i, j, int.Parse(elements[i][j].Text));
                        }
                        else
                        {
                            elements[i][j].Text = "0";
                            SendToServer("SetCell", i, j, int.Parse(elements[i][j].Text));
                        }
                    }
                }
                SendToServer("Reset");
                for (int i = 0; i < int.Parse(comboBox1.Text); i++)
                {
                    for (int j = 0; j < int.Parse(comboBox1.Text); j++)
                    {
                        elementsResult[i][j].Text = ((int)SendToServer("Cell", i, j)).ToString();
                    }
                }
                RefreshMatrix(tableLayoutPanel2, elementsResult);
            }
        }
        


        public dynamic SendToServer(string methodName, params object[] value)
        {
            XmlRpcResponse response;
            client.MethodName = "sample."+methodName;
            client.Params.Clear();
            foreach (var item in value)
            {
                Console.WriteLine(item.GetType());
                client.Params.Add(item);
            }
            response = client.Send(host);
            if (response.IsFault)
            {
                return -1;
            }
            else
            {
                return response.Value;
            }

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        void RefreshMatrix(TableLayoutPanel matrix, List<List<TextBox>> textBoxes)
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
                        Action action = () => matrix.Controls.Add(textBoxes[i][j]);
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
            RefreshMatrix(tableLayoutPanel1, elements);
            SendToServer("SetSize", int.Parse(comboBox1.Text));
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
