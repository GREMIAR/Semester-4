using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather
{
    public partial class ClientForm : Form
    {
        Weather weather;
        public ClientForm()
        {
            weather = new Weather(this);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendRequest();
        }
        public void WriteTextBox(string info)
        {
            new Task(() =>
            {
                this.Invoke(new Action(() =>
                {
                    textBox2.Text = info;
                }));
            }).Start();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendRequest();
            }
        }
        private void SendRequest()
        {
            try
            {
                weather.ConnectAsync(textBox1.Text).Wait();
            }
            catch
            {
                textBox2.Text = "Город не найден";
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

