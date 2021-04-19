using System.ServiceModel;

namespace FlightInformationService
{
    [ServiceContract]
    public interface FlightInformation
    {
        [OperationContract]
        string InformationSpecifiedRoute(string startPoint,string destinationPoint);
        [OperationContract]
        string NumberTickets(string numberFlight);
        [OperationContract]
        string BookTickets(string numberFlight);
    }
}
