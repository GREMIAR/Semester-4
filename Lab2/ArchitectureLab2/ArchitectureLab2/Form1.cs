using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace ArchitectureLab2
{
    public partial class ClientForm : Form
    {
        static TcpClient client = null;
        static string userName;
        const string address = "127.0.0.1";
        const int port = 8888;
        public ClientForm()
        {
            InitializeComponent();
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            userName = textBoxName.Text;
            client = new TcpClient(address, port);
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(String.Format(userName + ": вошёл в чат "));
            stream.Write(data, 0, data.Length);


            Thread ReceThread = new Thread(Receive);
            ReceThread.Start();
        }
        public void Receive()
        {
            NetworkStream stream = client.GetStream();
            while (true)
            {
                byte[] data = new byte[64]; // буфер для получаемых данных
                int bytes = stream.Read(data, 0, data.Length);
                string message = Encoding.Unicode.GetString(data, 0, bytes);
                textBoxChat.Text = message+"\n";
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            NetworkStream stream = client.GetStream();
            string message = textBoxMsg.Text;
            byte[] data = Encoding.Unicode.GetBytes(String.Format("{0}: {1}", userName, message));
            stream.Write(data, 0, data.Length);
            textBoxMsg.Text = "";
        }
    }
}
