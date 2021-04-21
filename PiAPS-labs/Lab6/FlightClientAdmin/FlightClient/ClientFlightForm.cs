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
    }
}
