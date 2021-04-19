using System.IO;
using System.Text.RegularExpressions;

namespace FlightInformationService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        string path = "InfoFlight.txt";
        public string InformationSpecifiedRoute(string startPoint, string destinationPoint)
        {
            string flightInfo = string.Empty;
            try
            {
                using (StreamReader str = new StreamReader(path))
                {
                    string line;
                    while ((line = str.ReadLine()) != null)
                    {
                        string[] subs = line.Split(' ');
                        if (subs[1] == startPoint && subs[2] == destinationPoint)
                        {
                            flightInfo += line;
                        }
                    }
                }
            }
            catch
            {
                flightInfo = "К сожалению, на нас совершается хакерская атака, зайдите через [ОШИБКА] минут";
            }
            if (flightInfo == string.Empty)
            {
                flightInfo = "К сожлению, мы не смогли ничего найти для вас";
            }
            return flightInfo;
        }
        public string NumberTickets(string numberFlight)
        {
            string numberTickets = string.Empty;
            try
            {
                using (StreamReader str = new StreamReader(path))
                {
                    string line;
                    while ((line = str.ReadLine()) != null)
                    {
                        string[] subs = line.Split(' ');
                        if (subs[0] == numberFlight)
                        {
                            numberTickets = subs[4];
                            break;
                        }
                    }
                }
            }
            catch
            {
                numberTickets = "К сожалению, на нас совершается хакерская атака, зайдите через [ОШИБКА] минут";
            }
            if (numberTickets == string.Empty)
            {
                numberTickets = "К сожалению, все билеты были раскупленны";
            }
            return numberTickets;
        }
        public string BookTickets(string numberFlight)
        {
            string numberTickets = string.Empty;
            try
            {
                string line;
                string[] subs = { };
                using (StreamReader str = new StreamReader(path))
                {

                    int index = 0;
                    while ((line = str.ReadLine()) != null)
                    {
                        subs = line.Split(' ');
                        if (subs[0] == numberFlight)
                        {
                            numberTickets = subs[4];
                            break;
                        }
                        index++;
                    }
                }
                StreamReader reader = new StreamReader(path);
                string content = reader.ReadToEnd();
                reader.Close(); content = Regex.Replace(content, line, Regex.Replace(line, subs[4], (int.Parse(subs[4]) - 1).ToString()));

                StreamWriter writer = new StreamWriter(path);
                writer.Write(content);
                writer.Close();

            }
            catch
            {
                numberTickets = "К сожалению, на нас совершается хакерская атака, зайдите через [ОШИБКА] минут";
            }
            if (numberTickets == string.Empty)
            {
                numberTickets = "К сожалению, нам не удалось забронировать билеты на ваш рейс";
            }
            return numberTickets;
        }
    }
}
