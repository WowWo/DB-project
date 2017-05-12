namespace Ygai
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Form1 : Form
    {
        private IContainer components = null;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem AddTableMenu;
        private ToolStripMenuItem DeleteTableMenu;
        private DBConnect dbConnect;
        private string server, database, uid, password;
        private bool Show=true;
        private Button[] PubButtonrray;

        public Form1(string server, string database, string uid, string password)
        {
            this.dbConnect = new DBConnect(server, database, uid, password);
            this.server = server;
            this.database = database;
            this.uid = uid;
            this.password = password;
            this.InitializeComponent();
        }



        private void buttonShowTable_Click(object sender, EventArgs e )
        {
            Button btn = sender as Button;
            this.Visible = false;
            new FormGai(btn.Text,dbConnect).ShowDialog();
            this.Visible = true;
            
            
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.AddTableMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteTableMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddTableMenu,
            this.DeleteTableMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(625, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // AddTableMenu
            // 
            this.AddTableMenu.Name = "AddTableMenu";
            this.AddTableMenu.Size = new System.Drawing.Size(119, 20);
            this.AddTableMenu.Text = "Добавить таблицу";
            this.AddTableMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.AddTableMenu.Click += new System.EventHandler(this.AddTableMenu_Click);
            // 
            // DeleteTableMenu
            // 
            this.DeleteTableMenu.Name = "DeleteTableMenu";
            this.DeleteTableMenu.Size = new System.Drawing.Size(111, 20);
            this.DeleteTableMenu.Text = "Удалить таблицу";
            this.DeleteTableMenu.Click += new System.EventHandler(this.DeleteTableMenu_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(625, 276);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор таблицы";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AddTableMenu_Click(object sender, EventArgs e)
        {
            Show = false;
            
            new FormAddTable (dbConnect,server,database,uid,password,this.Owner).Show();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Show = true;
            int tablesCount = dbConnect.ShowTables()[0].Count;
            Button[] Buttonrray = new Button[tablesCount];
            List<string>[] TableArray = new List<string>[tablesCount];
            TableArray = dbConnect.ShowTables();
            int wid=-85;
            int heig=35;
            int col = 0;
            for (int i = 0; i < tablesCount; i++)
            {
                col += 1;
                wid = wid + 90;
                Buttonrray[i] = new System.Windows.Forms.Button();
                Buttonrray[i].Location = new Point(wid,heig);
                Buttonrray[i].Text = TableArray[0][i];
                this.Controls.Add(Buttonrray[i]);
                Buttonrray[i].Click += new System.EventHandler(buttonShowTable_Click);
                if (col == 7) { wid = -85; heig = heig + 35; } 
            }
        }

        private void DeleteTableMenu_Click(object sender, EventArgs e)
        {
            Show = false;
            new FormDeleteTable(dbConnect,server,database,uid,password,this.Owner).Show();
            this.Close();
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Show)
            {
                this.Owner.Visible = true;
            }
        }

      
    }
}

