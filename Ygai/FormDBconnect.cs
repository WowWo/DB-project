namespace Ygai
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormDbConnect : Form
    {
        private IContainer components = null;
        private DBConnect dbConnect;
        private Button buttonDBConnect;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox ServerBox;
        private TextBox BDBox;
        private TextBox UserBox;
        private TextBox PassBox;
        private Button[] PubButtonrray;

        public FormDbConnect()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.buttonDBConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ServerBox = new System.Windows.Forms.TextBox();
            this.BDBox = new System.Windows.Forms.TextBox();
            this.UserBox = new System.Windows.Forms.TextBox();
            this.PassBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonDBConnect
            // 
            this.buttonDBConnect.Location = new System.Drawing.Point(80, 208);
            this.buttonDBConnect.Name = "buttonDBConnect";
            this.buttonDBConnect.Size = new System.Drawing.Size(149, 47);
            this.buttonDBConnect.TabIndex = 0;
            this.buttonDBConnect.Text = "Подключиться";
            this.buttonDBConnect.UseVisualStyleBackColor = true;
            this.buttonDBConnect.Click += new System.EventHandler(this.buttonDBConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(49, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сервер";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(31, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Название БД";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(31, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Пользователь";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(49, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Пароль";
            // 
            // ServerBox
            // 
            this.ServerBox.Location = new System.Drawing.Point(133, 32);
            this.ServerBox.Name = "ServerBox";
            this.ServerBox.Size = new System.Drawing.Size(119, 20);
            this.ServerBox.TabIndex = 5;
            this.ServerBox.Text = "localhost";
            // 
            // BDBox
            // 
            this.BDBox.Location = new System.Drawing.Point(133, 68);
            this.BDBox.Name = "BDBox";
            this.BDBox.Size = new System.Drawing.Size(119, 20);
            this.BDBox.TabIndex = 6;
            this.BDBox.Text = "Ygai";
            // 
            // UserBox
            // 
            this.UserBox.Location = new System.Drawing.Point(133, 106);
            this.UserBox.Name = "UserBox";
            this.UserBox.Size = new System.Drawing.Size(119, 20);
            this.UserBox.TabIndex = 7;
            this.UserBox.Text = "root";
            // 
            // PassBox
            // 
            this.PassBox.Location = new System.Drawing.Point(133, 141);
            this.PassBox.Name = "PassBox";
            this.PassBox.Size = new System.Drawing.Size(119, 20);
            this.PassBox.TabIndex = 8;
            this.PassBox.Text = "2602753";
            // 
            // FormDbConnect
            // 
            this.ClientSize = new System.Drawing.Size(335, 292);
            this.Controls.Add(this.PassBox);
            this.Controls.Add(this.UserBox);
            this.Controls.Add(this.BDBox);
            this.Controls.Add(this.ServerBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDBConnect);
            this.Name = "FormDbConnect";
            this.Text = "Подключение к базе данных";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonDBConnect_Click(object sender, EventArgs e)
        {
            if (ServerBox.Text == "" || BDBox.Text == "" || UserBox.Text == "" || PassBox.Text == "")
            {
                MessageBox.Show("Заполните все строки");
            }
            else
            {
                string server = ServerBox.Text.ToString();
                string database = BDBox.Text.ToString();
                string uid = UserBox.Text.ToString();
                string password = PassBox.Text.ToString();
                this.Visible = false;
                new Form1(server, database, uid, password).Show(this);
                
            }
        }
       
    }
}

