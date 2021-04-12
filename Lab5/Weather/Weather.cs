using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            WebRequest request = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?q=" + CountryID + "&units=metric&mode=xml&lang=ru&APPID=453470e9d170c9031b798d373094ab6c");
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
            XDocument doc;
            using (var sr = new StringReader(answer))
            {
                doc = XDocument.Load(sr);
            }
            XElement current = doc.Element("current");
            string info = "Город: " + current.Element("city").Attribute("name").Value;
            info += "\r\nСтрана: " + current.Element("city").Element("country").Value;
            info += "\r\nРассвет: " + current.Element("city").Element("sun").Attribute("rise").Value;
            info += "\r\nЗакат: " + current.Element("city").Element("sun").Attribute("set").Value;
            info += "\r\nСредняя температура: " + current.Element("temperature").Attribute("value").Value;
            info += " " + current.Element("temperature").Attribute("unit").Value;
            info += "\r\nМинимальная температура: " + current.Element("temperature").Attribute("min").Value;
            info += " " + current.Element("temperature").Attribute("unit").Value;
            info += "\r\nМаксимальная температура: " + current.Element("temperature").Attribute("max").Value;
            info += " " + current.Element("temperature").Attribute("unit").Value;
            info += "\r\nОщущается как: " + current.Element("feels_like").Attribute("value").Value;
            info += " " + current.Element("feels_like").Attribute("unit").Value;
            info += "\r\nВлажность: " + current.Element("humidity").Attribute("value").Value;
            info += " " + current.Element("humidity").Attribute("unit").Value;
            info += "\r\nАтмосферное давление: " + current.Element("pressure").Attribute("value").Value;
            info += " " + current.Element("pressure").Attribute("unit").Value;
            foreach (XElement elem in current.Elements("wind"))
            {
                info += "\r\nСкорость ветра: " + elem.Element("speed").Attribute("value").Value;
                info += " " + elem.Element("speed").Attribute("unit").Value;
                info += " " + elem.Element("speed").Attribute("name").Value;
                info += "\r\nНаправление: " + elem.Element("direction").Attribute("value").Value;
                info += " " + elem.Element("direction").Attribute("code").Value;
                info += " " + elem.Element("direction").Attribute("name").Value;
            }
            info += "\r\nОблака: " + current.Element("clouds").Attribute("value").Value;
            info += " " + current.Element("clouds").Attribute("name").Value;
            info += "\r\nВидимость: " + current.Element("visibility").Attribute("value").Value;
            info += "\r\nОсадки: " + current.Element("precipitation").Attribute("mode").Value;
            info += "\r\nПогода: " + current.Element("weather").Attribute("number").Value;
            info += " " + current.Element("weather").Attribute("value").Value;
            info += " " + current.Element("weather").Attribute("icon").Value;
            info += "\r\n\r\nПоследнее обновление: " + current.Element("lastupdate").Attribute("value").Value;
            form.WriteTextBox(info);
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


