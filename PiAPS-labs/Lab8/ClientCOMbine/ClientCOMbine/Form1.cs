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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Пинг-Понг")
            {
                textBox1.Text = combaen.PingPong(textBox2.Text);
            }
            else if (comboBox1.Text == "Кто ты из смешариков")
            {
                textBox1.Text = combaen.WhichSmesharik();
            }
            else if (comboBox1.Text == "Цельсия")
            {
                textBox1.Text = combaen.CelsiusToFahrenheit(float.Parse(textBox2.Text)).ToString();
            }
            else if (comboBox1.Text == "Фаренгейты")
            {
                textBox1.Text = combaen.FahrenheitToCelsius(float.Parse(textBox2.Text)).ToString();
            }
        }
    }
}
