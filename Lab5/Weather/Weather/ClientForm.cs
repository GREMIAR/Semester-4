using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Net;

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
            try
            {
                weather.ConnectAsync(textBox1.Text).Wait();
            }
            catch
            {
                textBox2.Text = "Город не найден";
            }
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
    }
}

