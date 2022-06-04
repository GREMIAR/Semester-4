using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace FlightInformationService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class Service1 : IService1
    {
        List<Flight> flights;
        Service1()
        {
            flights = new List<Flight>();
            flights.Add(new Flight(1, "Орёл", "Москва", 23));
            flights.Add(new Flight(2, "Орёл", "Москва", 2));
            flights.Add(new Flight(3, "Орёл", "Москва", 5));
            flights.Add(new Flight(4, "Орёл", "Москва", 6));
            flights.Add(new Flight(5, "Орёл", "Москва", 7));
        }

        public string InformationSpecifiedRoute(string numberFlight)
        {
            string flightInfo = string.Empty;
            try
            {
                foreach (Flight flight in flights)
                {
                    if (flight.NumberFlight.ToString() == numberFlight)
                    {
                        flightInfo = flight.Info();
                        break;
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
        public string BookTickets(int numberFlight)
        {
            string numberTickets =string.Empty;
            try
            {
                for(int i=0;i<flights.Count;i++)
                {
                    if (flights[i].NumberFlight == numberFlight&& flights[i].QuantityTickets != 0)
                    {

                        flights[i].QuantityTickets--;
                        numberTickets = "Вы успешно забронировали билет";
                        break;
                    }
                }
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
        public string numberFlightInfo(string startPoint, string destinationPoint)
        {
            string info = string.Empty;
            try
            {
                foreach (Flight flight in flights)
                {
                    if(flight.StartPoint == startPoint&& flight.DestinationPoint == destinationPoint)
                    {
                        info += flight.NumberFlight.ToString() + " ";
                    }
                }
            }
            catch
            {
                info = "К сожалению, на нас совершается хакерская атака, зайдите через [ОШИБКА] минут";
            }
            if (info == string.Empty)
            {
                info = "Рейсов нет";
            }
            return info;
        }
        public void AddFlight(int numberFlight, string startPoint, string destinationPoint, int quantityTickets)
        {
            foreach (Flight flight in flights)
            {
                if (flight.NumberFlight==numberFlight)
                {
                    return;
                }
            }
            flights.Add(new Flight(numberFlight, startPoint, destinationPoint, quantityTickets));
        }
        public void ChangesFlightQuantityTickets(int numberFlight,int quantityTickets)
        {
            for(int i=0;i<flights.Count;i++)
            {
                if (flights[i].NumberFlight == numberFlight)
                {
                    flights[i].QuantityTickets = quantityTickets;
                    break;
                }
            }
        }
        public void DelFlight(int numberFlight)
        {
            for (int i = 0; i < flights.Count; i++)
            {
                if (flights[i].NumberFlight == numberFlight)
                {
                    flights.RemoveAt(i);
                    break;
                }
            }
        }
        public string FullFlight()
        {
            string flightInfo = string.Empty;
            foreach (Flight flight in flights)
            {
                 flightInfo += flight.Info();
            }
            return flightInfo;
        }
    }
}
