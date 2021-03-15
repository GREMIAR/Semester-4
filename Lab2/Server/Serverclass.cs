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

        public void ClientConnect()
        {
            client = listener.AcceptTcpClient();
            clients.Add(client);
            Thread clientThread = new Thread(new ThreadStart(Process));
            clientThread.Start();
        }

        public void StartingServer(int port)
        {
            listener = new TcpListener(IPAddress.Parse("26.165.89.67"), port);
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
        }

        public void Process()
        {
            string username;
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[2048];
                int bytes = stream.Read(data, 0, data.Length); 
                username = Encoding.Unicode.GetString(data, 0, bytes);
                Console.WriteLine(username);
                username = username.Substring(0, username.LastIndexOf(':'));
                userArr.Add(username);
                UpdateUserOnline(stream,client);
                TcpClient localClient = client;
                while (true)
                {                        
                    data = new byte[2048]; 
                    stream = localClient.GetStream();
                    bytes = stream.Read(data, 0, data.Length); 
                    string message = Encoding.Unicode.GetString(data, 0, bytes);
                    Console.WriteLine(message);
                    if(message=="/Close")
                    {
                        UpdateUserOnline(stream,localClient);
                        userArr.Remove(username);
                        clients.Remove(localClient);
                        Console.WriteLine("Сюда");
                        localClient.Close();
                        stream.Close();
                        Console.WriteLine(username+"вышел");
                        return;
                    }
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
        public void UpdateUserOnline(NetworkStream stream,TcpClient localClient)
        {
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