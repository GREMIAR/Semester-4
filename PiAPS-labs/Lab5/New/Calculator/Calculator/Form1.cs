using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Calculator.CalculatorSoapClient client;
        public Form1()
        {
            InitializeComponent();
            client = new Calculator.CalculatorSoapClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = client.Add(int.Parse(textBox1.Text), int.Parse(textBox2.Text)).ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = client.Divide(int.Parse(textBox1.Text), int.Parse(textBox2.Text)).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = client.Multiply(int.Parse(textBox1.Text), int.Parse(textBox2.Text)).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = client.Subtract(int.Parse(textBox1.Text), int.Parse(textBox2.Text)).ToString();
        }
    }
}
