namespace FlightInformationService
{
    public class Flight
    {
        int numberFlight;
        public int NumberFlight 
        {
            get { return numberFlight; }
        }
        string startPoint;
        public string StartPoint
        {
            get { return startPoint; }
        }
        string destinationPoint;
        public string DestinationPoint
        {
            get { return destinationPoint; }
        }
        int quantityTickets;
        public int QuantityTickets
        { 
            get { return quantityTickets; }
            set { quantityTickets = value; } 
        }
        public string Info()
        {
            return "Номер полёта: " + numberFlight + ";\nОт: " + startPoint + ";\nДо: " + destinationPoint + ";\nКоличесвто билетов: " + quantityTickets + ";\n";
        }

        public Flight(int numberFlight, string startPoint,string destinationPoint,int quantityTickets)
        {
            this.numberFlight = numberFlight;
            this.startPoint = startPoint;
            this.destinationPoint = destinationPoint;
            this.quantityTickets = quantityTickets;
        }
    }
}