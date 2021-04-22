using System.ServiceModel;

namespace FlightInformationService
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string InformationSpecifiedRoute(string numberFlight);
        [OperationContract]
        string BookTickets(int numberFlight);
        [OperationContract]
        string numberFlightInfo(string startPoint, string destinationPoint);
        [OperationContract]
        void AddFlight(int numberFlight, string startPoint, string destinationPoint, int quantityTickets);
        [OperationContract]
        void ChangesFlightQuantityTickets(int numberFlight, int quantityTickets);
        [OperationContract]
        void DelFlight(int numberFlight);
        [OperationContract]
        string FullFlight();
    }
}
