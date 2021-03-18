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
            AutoCompleteStringCollection source = new AutoCompleteStringCollection()
            {
            "8888"
            };
            textBoxPort.AutoCompleteCustomSource = source;
            textBoxPort.AutoCompleteMode = AutoCompleteMode.Append;
            textBoxPort.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            LogIn();
        }

        public void LogIn()
        {
            try
            {
                userName = textBoxName.Text;
                client = new TcpClient(textBoxAddress.Text, int.Parse(textBoxPort.Text));
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.Unicode.GetBytes(String.Format(userName + ": вошёл в чат "));
                stream.Write(data, 0, data.Length);
                Thread ReceThread = new Thread(Receive);
                ReceThread.Start();
                buttonSend.Enabled = true;
            }
            catch
            {
                MessageBox.Show(
                "Мы не можем установить надёжного соединения",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public void Receive()
        {
            NetworkStream stream = client.GetStream();
            while (true)
            {
                byte[] data = new byte[1024];
                int bytes = stream.Read(data, 0, data.Length);
                string message = Encoding.Unicode.GetString(data, 0, bytes);
                if (!OnlineClient(message))
                {
                    textBoxChat.AppendText(message);
                    //textBoxChat.AppendText(Environment.NewLine);
                }
            }
        }
        public bool OnlineClient(string user)
        {
            if (user[0] == '/')
            {
                user = user.Substring(5);
                string[] words = user.Split('#');
                textBoxUserList.Clear();
                foreach (var word in words)
                {
                    textBoxUserList.AppendText(word);
                    textBoxUserList.AppendText(Environment.NewLine);
                }
                return true;
            }
            else
            {
                return false;
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

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void textBoxAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = textBoxPort;
            }
        }

        private void textBoxPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = textBoxName;
            }
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LogIn();
            }
        }
    }
}
