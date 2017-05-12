namespace Ygai
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormDeleteTable : Form
    {
        private IContainer components = null;
        private DBConnect dbConnect;
        private Button DeleteTableButton;
        private ComboBox DeleteTableComoBox;
        private Form owner;
        private string server, database, uid, password;
        private Button[] PubButtonrray;

        public FormDeleteTable(DBConnect dbconnect, string server, string database, string uid, string password, Form owner)
        {
            this.owner = owner;
            this.server = server;
            this.database = database;
            this.uid = uid;
            this.password = password;
            this.dbConnect = dbconnect;
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
            this.DeleteTableButton = new System.Windows.Forms.Button();
            this.DeleteTableComoBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // DeleteTableButton
            // 
            this.DeleteTableButton.Location = new System.Drawing.Point(46, 97);
            this.DeleteTableButton.Name = "DeleteTableButton";
            this.DeleteTableButton.Size = new System.Drawing.Size(108, 39);
            this.DeleteTableButton.TabIndex = 0;
            this.DeleteTableButton.Text = "Удалить";
            this.DeleteTableButton.UseVisualStyleBackColor = true;
            this.DeleteTableButton.Click += new System.EventHandler(this.DeleteTableButton_Click);
            // 
            // DeleteTableComoBox
            // 
            this.DeleteTableComoBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeleteTableComoBox.FormattingEnabled = true;
            this.DeleteTableComoBox.Location = new System.Drawing.Point(46, 23);
            this.DeleteTableComoBox.Name = "DeleteTableComoBox";
            this.DeleteTableComoBox.Size = new System.Drawing.Size(108, 21);
            this.DeleteTableComoBox.TabIndex = 1;
            // 
            // FormDeleteTable
            // 
            this.ClientSize = new System.Drawing.Size(224, 171);
            this.Controls.Add(this.DeleteTableComoBox);
            this.Controls.Add(this.DeleteTableButton);
            this.MaximizeBox = false;
            this.Name = "FormDeleteTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Удалить таблицу";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDeleteTable_FormClosed);
            this.Load += new System.EventHandler(this.FormDeleteTable_Load);
            this.ResumeLayout(false);

        }

        

        private void FormDeleteTable_Load(object sender, EventArgs e)
        {
            int tablesCount = dbConnect.ShowTables()[0].Count;
            List<string>[] TableArray = new List<string>[tablesCount];
            TableArray = dbConnect.ShowTables();
            for (int i = 0; i < tablesCount; i++)
            {
                DeleteTableComoBox.Items.Add(TableArray[0][i]);
            }
            DeleteTableComoBox.SelectedIndex = 0;
        }

       

        private void DeleteTableButton_Click(object sender, EventArgs e)
        {
            bool susc = dbConnect.DropTable(DeleteTableComoBox.Text);
            if (susc) { MessageBox.Show("Таблица Удалена!"); this.Close(); }
        }

        private void FormDeleteTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            new Form1(server, database, uid, password).Show(owner);
        }

        

      
    }
}

