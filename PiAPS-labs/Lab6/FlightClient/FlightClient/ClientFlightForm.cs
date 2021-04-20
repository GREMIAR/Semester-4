using System;
using System.Windows.Forms;

namespace FlightsClient
{
    public partial class Form1 : Form
    {
        Calculator.Flight.Service1Client flight;
        public Form1()
        {
            flight = new Calculator.Flight.Service1Client();
            InitializeComponent();
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            richTextBox1.Text = (flight.InformationSpecifiedRoute("Орёл", "Москва"));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Add(flight.InformationSpecifiedRoute("Орёл", "Москва"));
        }
    }
}
