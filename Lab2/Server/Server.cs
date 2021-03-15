using System;
 
namespace ServerChat
{
    class Program
    {
        const int port = 8888;
        static void Main(string[] args)
        {
            Server server = new Server();
            try
            {
                server.StartingServer(port);
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