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
        static List<string> userArr = new List<string>();

        TcpListener listener;

        TcpClient client;
        
        public Server(TcpClient client)
        {
            this.client = client;
        }
        public Server()
        {

        }

        public void StartingServer(string ipAddress,int port)
        {
            listener = new TcpListener(IPAddress.Parse(ipAddress), port);
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
        }

        public void ClientConnect()
        {
            client = listener.AcceptTcpClient();
            clients.Add(client);
            Thread clientThread = new Thread(new ThreadStart(Process));
            clientThread.Start();
        }

        public string ReadClient(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] data = new byte[2048];
            int bytes = stream.Read(data, 0, data.Length); 
            return Encoding.Unicode.GetString(data, 0, bytes);
        }

        public void Process()
        {
            NetworkStream stream = client.GetStream();;
            string username;
            TcpClient localClient = client;
            try
            {
                username = ReadClient(localClient);
                Console.WriteLine(username);
                username = username.Substring(0, username.LastIndexOf(':'));
                userArr.Add(username);
                UpdateUserOnline(localClient);
                while (true)
                {                        
                    string message = ReadClient(localClient);
                    Console.WriteLine(message);
                    if(message=="/Close")
                    {
                        userArr.Remove(username);
                        clients.Remove(localClient);
                        UpdateUserOnline(localClient);
                        localClient.Close();
                        stream.Close();
                        Console.WriteLine(username+": вышел");
                        return;
                    }
                    Console.WriteLine(message);
                    byte[] data = Encoding.Unicode.GetBytes(String.Format(message));
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
        void UpdateUserOnline(TcpClient localClient)
        {
            NetworkStream stream = localClient.GetStream();
            byte[] data = new byte[2048]; 
            string userOnline= "/user ";
            foreach (string user in userArr)
            {
                userOnline+=(user+"#");
            }
            //data = Encoding.Unicode.GetBytes(String.Format("Добро пожаловать в чат!"));
            //stream.Write(data, 0, data.Length);
            foreach (TcpClient Client in clients)
            {
                stream.Write(Encoding.Unicode.GetBytes(String.Format(userOnline)));
                if(Client!=localClient)
                {
                    stream = Client.GetStream();
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        public void ClossServer()
        {
            if(listener!=null)
                listener.Stop();
        }
    }
}