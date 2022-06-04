using System;
using System.Net;
using System.Net.Sockets;
namespace ServerChat
{
    class Program
    {
        const int port = 8888;
        static void Main(string[] args)
        {
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
            throw new Exception("В системе нет сетевых адаптеров с IPv4 адресом!");
        }
    }
}