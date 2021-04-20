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
            try
            {
                int one, two;
                StrToInt(out one, out two);
                textBox3.Text = await Task.Run(() => client.Add(one, two).ToString());
            }
            catch (Exception)
            {
                textBox3.Text = "Ошибка";
            }
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                int one, two;
                StrToInt(out one, out two);
                textBox3.Text = await Task.Run(() => client.Subtract(one, two).ToString());
            }
            catch (Exception)
            {
                textBox3.Text = "Ошибка";
            }
        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                int one, two;
                StrToInt(out one, out two);
                textBox3.Text = await Task.Run(() => client.Multiply(one, two).ToString());
            }
            catch (Exception)
            {
                textBox3.Text = "Ошибка";
            }
}

        private async void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                int one, two;
                StrToInt(out one, out two);
                textBox3.Text = await Task.Run(() => client.Divide(one, two).ToString());
            }
            catch (Exception)
            {
                textBox3.Text = "Ошибка";
            }
        }

        void StrToInt(out int one, out int two)
        {
            Int32.TryParse(textBox1.Text, out one);
            Int32.TryParse(textBox2.Text, out two);
            textBox1.Text = one.ToString();
            textBox2.Text = two.ToString();
        }
    }
}
