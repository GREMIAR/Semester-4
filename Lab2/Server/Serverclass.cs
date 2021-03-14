using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
 
namespace Server
{
    class Program
    {
        const int port = 8888;
        static TcpListener listener;
        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                listener.Start();
                Console.WriteLine("Ожидание подключений...");
                TcpClient client = listener.AcceptTcpClient();
                Console.Clear();
                СonnectedClient clientObject = new СonnectedClient(client);
                Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                clientThread.Start();
                while(true)
                {
                    client = listener.AcceptTcpClient();
                    clientObject = new СonnectedClient(client);
                    clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(listener!=null)
                    listener.Stop();
            }
        }
    }
}