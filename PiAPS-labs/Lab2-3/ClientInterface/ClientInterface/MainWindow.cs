using System;
using System.Windows.Forms;

namespace ClientInterface
{
    public partial class ClientForm : Form
    {
        //конструктор
        public ClientForm()
        {
            InitializeComponent();
            client = new Client(this);
            AutoCompleteStringCollection source = new AutoCompleteStringCollection(){"8888"};
            textBoxPort.AutoCompleteCustomSource = source;
            textBoxPort.AutoCompleteMode = AutoCompleteMode.Append;
            textBoxPort.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        Client client;
        //при клике на кнопку войти происходит попытка TCP-клиента соединиться с сервером
        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            if(LogIn())
            {
                textBoxAddress.Visible = false;
                textBoxPort.Visible = false;
                textBoxName.Visible = false;
                buttonLogIn.Visible = false;
            }
        }
        //попытка TCP-клиента соединиться с сервером
        bool LogIn()
        {
            try
            {
                string userName = textBoxName.Text;
                client.LogIn(userName, textBoxAddress.Text, textBoxPort.Text);
                buttonSend.Enabled = true;
                return true;
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
                return false;
            }
        }
        //записывает текст в текстовое поле textBoxChat
        public void WriteTextBoxChat(string message)
        {
            Action action = () => textBoxChat.AppendText(message);
            if (this.InvokeRequired)
                Invoke(action);
            else
                action();
            action = () => textBoxChat.AppendText(Environment.NewLine);
            if (this.InvokeRequired)
                Invoke(action);
            else
                action();
        }
        /*Обновляет информацию у TCP-клиента, о ползователях онлайн
         true - если это комнада об обновленни пользователей 
         false - если это не комнада об обновленни пользователей */
        public bool OnlineClient(string user)
        {
            if (user[0] == '/')
            {
                user = user.Substring(5);
                string[] words = user.Split('#');
                Action action = () => textBoxUserList.Clear();
                if (this.InvokeRequired)
                    Invoke(action);
                else
                    action();
                foreach (var word in words)
                {
                    action = () => textBoxUserList.AppendText(word);
                    if (this.InvokeRequired)
                        Invoke(action);
                    else
                        action();
                    action = () => textBoxUserList.AppendText(Environment.NewLine);
                    if (this.InvokeRequired)
                        Invoke(action);
                    else
                        action();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        //при клике на кнопку отправить отправляется массив байт на сервер
        private void buttonSend_Click(object sender, EventArgs e)
        {
            string message = textBoxMsg.Text;
            textBoxChat.AppendText("Вы: " + message);
            textBoxChat.AppendText(Environment.NewLine);
            textBoxMsg.Clear();
            client.Send(message);
        }
        //Очистка поле ввода при клике на них
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

        //при нажатии кнопки Enter откправлет массив байт на сервер 
        private void textBoxMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string message = textBoxMsg.Text;
                textBoxChat.AppendText("Вы: " + message);
                textBoxChat.AppendText(Environment.NewLine);
                textBoxMsg.Clear();
                client.Send(message);
            }
        }
        //при закрытии окна отправляет серверу команду об отключении клиента
        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                client.CloseClient();
            }
            catch
            {
                return;
            }
        }
        //при нажатии кнопки Enter переключает фокус на поле с вводом порта
        private void textBoxAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = textBoxPort;
            }
        }
        //при нажатии кнопки Enter переключает фокус на поле с вводом имени
        private void textBoxPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = textBoxName;
            }
        }
        //при нажатии кнопки Enter происходит попытка подключения к серверу
        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (LogIn())
                {
                    textBoxAddress.Visible = false;
                    textBoxPort.Visible = false;
                    textBoxName.Visible = false;
                    buttonLogIn.Visible = false;
                }
            }
        }
        //при закрытии окна отправляет серверу команду об отключении клиента
        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                client.CloseClient();
            }
            catch
            {
                return;
            }
        }
    }
}
