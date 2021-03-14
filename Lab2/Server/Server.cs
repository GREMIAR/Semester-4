using System;
using System.Net.Sockets;
using System.Text;
 
namespace Server
{
    public class СonnectedClient
    {
        TcpClient client;
        
        public СonnectedClient(TcpClient client)
        {
            this.client = client;
        }
        
        public TcpClient GetClient()
        {
            return client;
        }

        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                while(true)
                {
                    stream = client.GetStream();
                    byte[] data = new byte[64]; 
                    while (true)
                    {
                        int bytes = stream.Read(data, 0, data.Length); 
                        string message = Encoding.Unicode.GetString(data, 0, bytes);
                        Console.WriteLine(message);
                        
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
        }
    }
}