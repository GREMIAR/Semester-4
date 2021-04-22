using System;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace FlightsClient
{
    public partial class Form1 : Form
    {

        FlightClient.Flight.Service1Client flight;
        public Form1()
        {
            flight = new FlightClient.Flight.Service1Client();
            InitializeComponent();
            richTextBox1.Text = flight.FullFlight();
            Thread refreshing = new Thread(RefreshInfo);
            refreshing.Start();
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 48 || e.KeyChar >= 59) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flight.AddFlight(int.Parse(textBox4.Text),textBox1.Text,textBox2.Text, int.Parse(textBox3.Text));
            richTextBox1.Text = flight.FullFlight();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flight.DelFlight(int.Parse(textBox5.Text));
            richTextBox1.Text = flight.FullFlight();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            flight.ChangesFlightQuantityTickets(int.Parse(textBox6.Text), int.Parse(textBox7.Text));
            richTextBox1.Text = flight.FullFlight();
        }

        void RefreshInfo()
        {
            while (true)
            {
                for (int i = 9; i >= 0; i--)
                {
                    new Task(() =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            toolStripStatusLabel1.Text = "Обновление информации через " + i.ToString() + " секунд(ы).";
                            if (i == 0)
                            {
                                richTextBox1.Text = flight.FullFlight();
                            }
                        }));
                    }).Start();
                    Thread.Sleep(1000);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = flight.FullFlight();
        }

        private void richTextBox1_Enter_1(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
