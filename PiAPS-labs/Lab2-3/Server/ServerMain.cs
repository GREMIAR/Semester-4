using System;
using System.Net;
using System.Net.Sockets;
namespace ServerChat
{
    class Program
    {
        const int port = 8888;
        const string ipAdd = "Ваш IP";//нужно раскомментировать 12 и закомментировать 11
        static void Main(string[] args)
        {
            //IPAddress ipAddress = Dns.GetHostAddresses(Dns.GetHostName())[3];
            Console.WriteLine(GetLocalIPAddress());
            IPAddress ipAddress = IPAddress.Parse(GetLocalIPAddress());
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
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}