using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
namespace ClientInterface
{
    class Client
    {
        ClientForm form;
        //конструктор
        public Client(ClientForm form)
        {
            this.form = form;
        }
        TcpClient client = null;
        string userName;
        //подключаемя к серверу и создаём поток на прослушивание сообщений от него
        public void LogIn(string userName,string ipAddr,string port)
        {
            this.userName = userName;
            client = new TcpClient(ipAddr, int.Parse(port));
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(String.Format(userName + ": вошёл в чат "));
            stream.Write(data, 0, data.Length);
            Thread ReceThread = new Thread(Receive);
            ReceThread.Start();
        }
        //поток для прослушивания сообщений от сервера и обработка их
        public void Receive()
        {
            NetworkStream stream = client.GetStream();
            while (true)
            {
                try
                {
                    byte[] data = new byte[1024];
                    int bytes = stream.Read(data, 0, data.Length);
                    string message = Encoding.Unicode.GetString(data, 0, bytes);
                    if (!form.OnlineClient(message))
                    {
                        form.WriteTextBoxChat(message);
                    }
                }
                catch
                {
                    return;
                }
            }
        }
        //отправка массивай байт на сервер
        public void Send(string message)
        {
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(String.Format("{0}: {1}", userName, message));
            stream.Write(data, 0, data.Length);
        }
        //отправка команды отключения клиента на сервер
        public void CloseClient()
        {
            try
            {
                NetworkStream stream = client.GetStream();
                string message = "/Close";
                byte[] data = Encoding.Unicode.GetBytes(String.Format(message));
                stream.Write(data, 0, data.Length);
            }
            catch
            {
                return;
            }
        }
    }
}