namespace ArchitectureLab2
{
    partial class ClientForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.buttonLogIn = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxUserList = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxChat
            // 
            this.textBoxChat.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBoxChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxChat.Location = new System.Drawing.Point(294, 12);
            this.textBoxChat.Multiline = true;
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.ReadOnly = true;
            this.textBoxChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxChat.Size = new System.Drawing.Size(742, 603);
            this.textBoxChat.TabIndex = 0;
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLogIn.Location = new System.Drawing.Point(12, 117);
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Size = new System.Drawing.Size(276, 47);
            this.buttonLogIn.TabIndex = 1;
            this.buttonLogIn.Text = "Вход";
            this.buttonLogIn.UseVisualStyleBackColor = true;
            this.buttonLogIn.Click += new System.EventHandler(this.buttonLogIn_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(12, 82);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(276, 29);
            this.textBoxName.TabIndex = 2;
            this.textBoxName.Text = " Имя";
            this.textBoxName.Click += new System.EventHandler(this.textBoxName_Click);
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBoxMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxMsg.Location = new System.Drawing.Point(294, 621);
            this.textBoxMsg.Multiline = true;
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMsg.Size = new System.Drawing.Size(607, 112);
            this.textBoxMsg.TabIndex = 3;
            this.textBoxMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxMsg_KeyDown);
            // 
            // buttonSend
            // 
            this.buttonSend.Enabled = false;
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSend.Location = new System.Drawing.Point(907, 621);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(129, 112);
            this.buttonSend.TabIndex = 4;
            this.buttonSend.Text = "Отправить";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxUserList
            // 
            this.textBoxUserList.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBoxUserList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUserList.Location = new System.Drawing.Point(12, 222);
            this.textBoxUserList.Multiline = true;
            this.textBoxUserList.Name = "textBoxUserList";
            this.textBoxUserList.ReadOnly = true;
            this.textBoxUserList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxUserList.Size = new System.Drawing.Size(276, 511);
            this.textBoxUserList.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Список пользователей:";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBoxAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAddress.Location = new System.Drawing.Point(12, 12);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(276, 29);
            this.textBoxAddress.TabIndex = 10;
            this.textBoxAddress.Text = " Адрес";
            this.textBoxAddress.Click += new System.EventHandler(this.textBoxAddress_Click);
            // 
            // textBoxPort
            // 
            this.textBoxPort.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBoxPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPort.Location = new System.Drawing.Point(12, 47);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(276, 29);
            this.textBoxPort.TabIndex = 12;
            this.textBoxPort.Text = " Порт";
            this.textBoxPort.Click += new System.EventHandler(this.textBoxPort_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1048, 745);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxUserList);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxMsg);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonLogIn);
            this.Controls.Add(this.textBoxChat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ClientForm";
            this.Text = "ChatClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.Button buttonLogIn;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxMsg;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxUserList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.TextBox textBoxPort;
    }
}

