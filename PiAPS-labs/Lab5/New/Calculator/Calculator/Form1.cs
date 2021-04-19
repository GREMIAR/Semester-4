using System;
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

        private void textBox3_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            int one = int.Parse(textBox1.Text);
            int two = int.Parse(textBox2.Text);
            textBox3.Text = await Task.Run(() => client.Add(one, two).ToString());
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            int one = int.Parse(textBox1.Text);
            int two = int.Parse(textBox2.Text);
            textBox3.Text = await Task.Run(() => client.Subtract(one, two).ToString());
        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            int one = int.Parse(textBox1.Text);
            int two = int.Parse(textBox2.Text);
            textBox3.Text = await Task.Run(() => client.Multiply(one, two).ToString());
        }

        private async void button4_Click_1(object sender, EventArgs e)
        {
            int one = int.Parse(textBox1.Text);
            int two = int.Parse(textBox2.Text);
            textBox3.Text = await Task.Run(() => client.Divide(one, two).ToString());
        }
    }
}
