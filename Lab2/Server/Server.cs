using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
 
namespace ServerChat
{
    class Program
    {
        const int port = 8888;
        static void Main(string[] args)
        {
            Server test = new Server();
            try
            {

                List<TcpClient> client = new List<TcpClient>();

                test.StartingServer(port);

                Console.Clear();



                test.FirstClient();

                while(true)
                {
                   test.SecondClient();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                test.ClossServer();
            }
        }
    }
}