using System;
using System.Windows.Forms;

namespace FlightsClient
{
    public partial class Form1 : Form
    {

        FlightClient.Flight.Service1Client flight;
        public Form1()
        {
            flight = new FlightClient.Flight.Service1Client();
            InitializeComponent();
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] words = flight.numberFlightInfo(textBox1.Text,textBox2.Text).Split(new char[] {' '});
            foreach(string word in words)
            {
                listBox1.Items.Add(word);
            }    
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = (flight.InformationSpecifiedRoute(listBox1.SelectedItem.ToString()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = flight.BookTickets(int.Parse(listBox1.SelectedItem.ToString()));
            richTextBox1.Text = (flight.InformationSpecifiedRoute(listBox1.SelectedItem.ToString()));
        }
    }
}
