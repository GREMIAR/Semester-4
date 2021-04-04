using System;
using System.Net;
namespace ServerChat
{
    class Program
    {
        const int port = 8888;
        //const string ipAdd = "Ваш IP";//нужно раскомментировать 12 и закомментировать 11
        static void Main(string[] args)
        {
            IPAddress ipAddress = Dns.GetHostAddresses(Dns.GetHostName())[3];
            //IPAddress ipAddress = IPAddress.Parse(ipAdd);
            Server server = new Server();
            try
            {
                server.StartingServer(ipAddress,port);
                while(true)
                {
                   server.ClientConnect();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.ClossServer();
            }
        }
    }
}