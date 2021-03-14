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

        public ClientForm()
        {
            InitializeComponent();
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            userName = textBoxName.Text;
            client = new TcpClient(textBoxAddress.Text,int.Parse(textBoxPort.Text));
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(String.Format(userName + ": вошёл в чат "));
            stream.Write(data, 0, data.Length);
            Thread ReceThread = new Thread(Receive);
            ReceThread.Start();
            buttonSend.Enabled = true;
        }
        public void Receive()
        {
            NetworkStream stream = client.GetStream();
            while (true)
            {
                byte[] data = new byte[1024];
                int bytes = stream.Read(data, 0, data.Length);
                string message = Encoding.Unicode.GetString(data, 0, bytes);
                textBoxChat.AppendText(message);
                textBoxChat.AppendText(Environment.NewLine);
            }
        }
        public void Send()
        {
            NetworkStream stream = client.GetStream();
            string message = textBoxMsg.Text;
            textBoxChat.AppendText("Вы: " + message);
            textBoxChat.AppendText(Environment.NewLine);
            byte[] data = Encoding.Unicode.GetBytes(String.Format("{0}: {1}", userName, message));
            stream.Write(data, 0, data.Length);
            textBoxMsg.Clear();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void textBoxAddress_Click(object sender, EventArgs e)
        {
            textBoxAddress.Clear();
        }

        private void textBoxPort_Click(object sender, EventArgs e)
        {
            textBoxPort.Clear();
        }

        private void textBoxName_Click(object sender, EventArgs e)
        {
            textBoxName.Clear();
        }

        private void textBoxMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send();
            }
        }
    }
}
