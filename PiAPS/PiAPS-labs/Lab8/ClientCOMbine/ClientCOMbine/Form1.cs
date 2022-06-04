using System;
using System.Windows.Forms;

namespace ClientCOMbine
{
    public partial class Form1 : Form
    {
        COMbine.Starsurge combain = new COMbine.Starsurge();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Кто ты из смешариков")
            {
                textBox1.Text = combain.WhichSmesharik();
            }
            else if (comboBox1.Text == "Из цельсия в фаренгейты")
            {
                float degrees;
                Single.TryParse(textBox2.Text, out degrees);
                textBox2.Text = degrees.ToString();
                textBox1.Text = degrees + "°C = " + combain.CelsiusToFahrenheit(degrees).ToString() +"°F";
            }
            else if (comboBox1.Text == "Из фаренгейтов в цельсия")
            {
                float degrees;
                Single.TryParse(textBox2.Text, out degrees);
                textBox2.Text = degrees.ToString();
                textBox1.Text = degrees + "°F = " + combain.FahrenheitToCelsius(degrees).ToString() + "°C";
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 48 || e.KeyChar >= 59) && e.KeyChar != 8 && e.KeyChar != 45)
                e.Handled = true;
        }
    }
}
