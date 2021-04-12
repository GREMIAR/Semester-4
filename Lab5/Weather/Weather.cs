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
            /*
            <current>
                <city id="0" name="Mountain View">
                    <coord lon="-122.09" lat="37.39" />
                    <country>US</country>
                    <timezone>-28800</timezone>
                    <sun rise="2020-01-07T15:22:59" set="2020-01-08T01:05:37" />
                </city>
                <temperature value="278.07" min="273.15" max="282.59" unit="kelvin" />
                <feels_like value="275.88" unit="kelvin" />
                <humidity value="86" unit="%" />
                <pressure value="1026" unit="hPa" />
                <wind>
                    <speed value="0.93" unit="m/s" name="Calm" />
                    <gusts />
                    <direction value="23" code="NNE" name="North-northeast" />
                </wind>
                <clouds value="1" name="clear sky" />
                <visibility value="16093" />
                <precipitation mode="no" />
                <weather number="800" value="clear sky" icon="01n" />
                <lastupdate value="2020-01-07T11:33:40" />
            </current>
             */
            XDocument doc;
            using (var sr = new StringReader(answer))
            {
                doc = XDocument.Load(sr);
            }
            XElement current = doc.Element("current");
            string info = "Город:" + current.Element("city").Attribute("name").Value;
            info += "\r\nСтрана:" + current.Element("city").Element("country").Value;
            info += "\r\nРассвет:" + current.Element("city").Element("sun").Attribute("rise").Value;
            info += "\r\nЗакат:" + current.Element("city").Element("sun").Attribute("set").Value;
            info += "\r\nТемпература:" + current.Element("temperature").Attribute("value").Value;
            /*info += "\r\nЗакат:" + current.Element("city").Element("sun").Attribute("set").Value;
            info += "\r\nЗакат:" + current.Element("city").Element("sun").Attribute("set").Value;
            info += "\r\nЗакат:" + current.Element("city").Element("sun").Attribute("set").Value;*/

            //"Средняя температура в данный момент в городе " + current.Element("city").Element("coord").Attribute("lon").Value)
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


