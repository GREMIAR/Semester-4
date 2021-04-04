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
        // массивы подключённых клиентов разных типов
        static List<TcpClient>  clients = new List<TcpClient>();
        static List<string> userArr = new List<string>();


        static TcpListener listener;

        TcpClient client;
        
        public Server()
        {

        }
        //начинает прослушивать подключения от TCP-клиентов
        public void StartingServer(IPAddress ipAddress,int port)
        {
            listener = new TcpListener(ipAddress, port);
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
        }
        //принимает запрос TCP-клиента и запускает для его обработки новый поток
        public void ClientConnect()
        {
            client = listener.AcceptTcpClient();
            clients.Add(client);
            Thread clientThread = new Thread(new ThreadStart(Process));
            clientThread.Start();
        }
        //принимет массив байт от TCP-клиента и возращает её, преобразованную в тип string
        public string ReadClient(TcpClient tcpClient,NetworkStream stream)
        {
            stream = tcpClient.GetStream();
            byte[] data = new byte[2048];
            int bytes = stream.Read(data, 0, data.Length); 
            return Encoding.Unicode.GetString(data, 0, bytes);
        }
        //поток обработки TCP-клиента
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

        //обработчик команд от TCP-клиента
        //возращает true если это комнада,false если это не команда
        bool Commands(string message,string username,TcpClient localClient,NetworkStream stream)
        {
            //команда выхода TCP-клиента с сервера
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
        // отправлет массив байт от TCP-клиента всем остальным клиентам, находящимся на сервере
        public void WriteClient(TcpClient localClient,NetworkStream stream,string message)
        {
            stream = localClient.GetStream();
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
        //отправлет всем TCP-клиентам сервера массив байт с TCP-клиентами, которые находятся на сервере
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
        //Закрываем прослушку новых клиентов
        public void ClossServer()
        {
            listener.Stop();
        }
    }
}