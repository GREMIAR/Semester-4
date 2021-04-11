using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace Weather
{
    public class Weather
    {
        ClientForm form;
        public Weather(ClientForm form)
        {
            this.form = form;
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
            form.WriteTextBox("Средняя температура в данный момент в городе " + response_global.name + " = " + response_global.main.temp);
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


