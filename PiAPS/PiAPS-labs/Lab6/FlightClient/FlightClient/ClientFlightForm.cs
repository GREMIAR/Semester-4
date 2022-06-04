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
            Thread refreshing = new Thread(RefreshInfo);
            refreshing.Start();
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            RefreshListBox();
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
                                RefreshListBox();
                                richTextBox1.Text = (flight.InformationSpecifiedRoute(listBox1.SelectedItem.ToString()));
                            }
                        }));
                    }).Start();
                    Thread.Sleep(1000);
                }
            }
        }
        void RefreshListBox()
        {
            int selectedIndex = listBox1.SelectedIndex;
            listBox1.Items.Clear();
            string[] words = flight.numberFlightInfo(textBox1.Text, textBox2.Text).Split(new char[] { ' ' });
            foreach (string word in words)
            {
                listBox1.Items.Add(word);
            }
            listBox1.SelectedIndex = selectedIndex;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
