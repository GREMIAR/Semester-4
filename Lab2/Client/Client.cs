using System;
using System.Threading;
 
namespace Client
{
    class Program
    {
        const string address = "127.0.0.1"; 
        const int port = 8888;
        static void Main(string[] args)
        {
            try
            {
                ClientMessage.SignUp(address,port);
                Thread SendThread = new Thread(ClientMessage.Send);
                SendThread.Start();
                Thread ReceThread = new Thread(ClientMessage.Receive);
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