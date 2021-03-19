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

        static TcpListener listener;

        TcpClient client;
        
        public Server(TcpClient client)
        {
            this.client = client;
        }
        public Server()
        {

        }

        public void StartingServer(IPAddress ipAddress,int port)
        {
            listener = new TcpListener(ipAddress, port);
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

        public string ReadClient(TcpClient tcpClient,NetworkStream stream)
        {
            stream = tcpClient.GetStream();
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
                username = ReadClient(localClient,stream);
                Console.WriteLine(username);
                username = username.Substring(0, username.LastIndexOf(':'));
                userArr.Add(username);
                UpdateUserOnline(localClient);
                while (true)
                {                        
                    string message = ReadClient(localClient,stream);
                    if(Commands(message,username,localClient,stream))
                    {
                        return;
                    }
                    Console.WriteLine(message);
                    WriteClient(localClient,stream,message);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        bool Commands(string message,string username,TcpClient localClient,NetworkStream stream)
        {
            if(message=="/Close")
            {
                userArr.Remove(username);
                UpdateUserOnline(localClient);
                clients.Remove(localClient);
                localClient.Close();
                stream.Close();
                Console.WriteLine(username+": вышел");
                return true;
            }
            return false;
        }

        public void  WriteClient(TcpClient tcpClient,NetworkStream stream,string message)
        {
            stream = tcpClient.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(String.Format(message));
            foreach (TcpClient Client in clients)
            {
                if(Client!=tcpClient)
                {
                    stream = Client.GetStream();
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        public void WriteLocalClient(TcpClient localClient,NetworkStream stream,string message)
        {
            stream = localClient.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(String.Format(message));
            foreach (TcpClient Client in clients)
            {
                if(Client==localClient)
                {
                    stream = Client.GetStream();
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        void UpdateUserOnline(TcpClient localClient)
        {
            string userOnline= "/user";
            foreach (string user in userArr)
            {
                userOnline+=(user+"#");
            }
            foreach (TcpClient Client in clients)
            {
                NetworkStream stream = Client.GetStream();
                stream.Write(Encoding.Unicode.GetBytes(String.Format(userOnline)));
            }
        }

        public void ClossServer()
        {
            listener.Stop();
        }
    }
}