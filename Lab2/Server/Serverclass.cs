using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Net;

namespace ServerChat
{
    public class Server
    {
        static List<TcpClient>  clients = new List<TcpClient>();
        TcpListener listener;

        TcpClient client;
        
        public Server(TcpClient client)
        {
            this.client = client;
        }
        public Server()
        {
        }
        
        public TcpClient GetClient()
        {
            return client;
        }

        public void FirstClient()
        {
            Console.Clear();
            Server clientObject = new Server(client);
            Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
            clientThread.Start();
        }
        public void SecondClient()
        {
            client = listener.AcceptTcpClient();
            clients.Add(client);
            Server clientObject = new Server(client);
            Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
            clientThread.Start();
        }

        public void StartingServer(int port)
        {
            listener = new TcpListener(IPAddress.Parse("26.146.45.95"), port);
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
            client = listener.AcceptTcpClient();
            clients.Add(client);
        }

        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                while(true)
                {
                    stream = client.GetStream();
                    TcpClient localClient = client;
                    while (true)
                    {
                        byte[] data = new byte[1024]; 
                        stream = localClient.GetStream();
                        int bytes = stream.Read(data, 0, data.Length); 
                        string message = Encoding.Unicode.GetString(data, 0, bytes);
                        Console.WriteLine(message);
                        foreach (TcpClient Client in clients)
                        {
                            if(Client!=localClient)
                            {
                                stream = Client.GetStream();
                                stream.Write(data, 0, data.Length);
                            }
                        }
                        
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

        public void ClossServer()
        {
            if(listener!=null)
                listener.Stop();
        }
    }
}