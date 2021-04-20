using System;
using System.Windows.Forms;

namespace ClientCOMbine
{
    public partial class Form1 : Form
    {
        COMbine.Starsurge combaen = new COMbine.Starsurge();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = combaen.PingPong("");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = combaen.PingPong(comboBox1.Text);
        }
    }
}
