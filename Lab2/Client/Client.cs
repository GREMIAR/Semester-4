using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
 
namespace Client
{
    class Program
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Write("Введите свое имя:");
            string userName = Console.ReadLine();
            try
            {
                ClientMessage.LogIn(port,address,userName);
                Thread SendThread = new Thread(ClientMessage.Send);
                SendThread.Start();
                Thread ReceThread = new Thread(ClientMessage.Rece);
                ReceThread.Start();
                while(true)
                {
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ClientMessage.CloseMessage();
            }
        }
    }
}