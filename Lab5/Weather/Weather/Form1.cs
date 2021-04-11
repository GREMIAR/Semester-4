using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace Weather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                ConnectAsync(textBox1.Text).Wait();
                
            }


            catch (Exception ex)
            {
                textBox2.Text = ex.ToString();


            }


        }
        public async Task ConnectAsync(string CountryID)
        {
            WebRequest request = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?q=" + CountryID + "&APPID=453470e9d170c9031b798d373094ab6c");
            WebResponse response = await request.GetResponseAsync().ConfigureAwait(false);

            string answer = string.Empty;
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();


            WeatherResponse response_global = JsonConvert.DeserializeObject<WeatherResponse>(answer);
            textBox2.Text = "Средняя температура в данный момент в городе " + response_global.name + " = " + response_global.main.temp;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }
    }

    public class Temperatura
    {
        public double temp;
    }

    public class WeatherNow
    {
        public string main;
        public string description;
    }

    public class WeatherResponse
    {
        public Temperatura main;
        public string name;
        public WeatherNow[] weather;
    }

}

