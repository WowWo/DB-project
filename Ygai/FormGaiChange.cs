namespace Ygai
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormGaiChange : Form
    {
        private Button buttonChangeGai;
        private IContainer components = null;
        private DBConnect dbConnect;
        public int id;
        public string table,primaryField;
        private string[] header;
        private TextBox[] PubBoxarray ;
        private Label[] Publabelarray ;
        private ComboBox[] PubComboarray;
        public FormGaiChange(int id, string[] headerList, string table, DBConnect dbconnect,string primaryField)
        {
            this.InitializeComponent();
            this.dbConnect = dbconnect;
            this.id = id;
            this.primaryField = primaryField;
            this.table = table;
            this.header = headerList;
            
        }
        private void buttonChangeGai_Click(object sender, EventArgs e)
        {
            string[] values= new string [header.Length+1];
            bool pust=false;
            foreach (TextBox box in PubBoxarray)
            {
                if (box != null)
                {
                    if (box.Text == "") { pust = true; break; }
                }
            }
             foreach (ComboBox comb in PubComboarray)
            {
                if (comb != null)
                {
                    if (comb.Text == "") { pust = true; break; }
                }
            }
            if (pust == false)
            {
                for (int i = 0; i < header.Length; i++)
                {
                    if (PubBoxarray[i] == null)
                    {
                        string[] val = PubComboarray[i].Text.Split(' ');
                        if (i != header.Length - 1)
                            values[i] = Publabelarray[i].Text + "=" + "'" + val[0] + "' , ";
                        else
                            values[i] = Publabelarray[i].Text + "=" + "'" + val[0] + "'";
                    }
                    else
                    {
                        if (i != header.Length - 1)
                            values[i] = Publabelarray[i].Text + "=" + "'" + PubBoxarray[i].Text + "' , ";
                        else
                            values[i] = Publabelarray[i].Text + "=" + "'" + PubBoxarray[i].Text + "'";
                    }
                }
                values[header.Length] = " where id=" + this.id;
                string value = "";
                for (int i = 0; i < header.Length + 1; i++)
                {
                    value += values[i];
                }
               
                    this.dbConnect.Update(table, value);
                 
            }
            else
            { MessageBox.Show("Заполните все поля!"); }
        }
            
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormGaiChange_FormClosing(object sender, FormClosingEventArgs e)
        {
            new FormGai(table,dbConnect).Show();
        }

        private void FormGaiChange_Load(object sender, EventArgs e)
        {
            TextBox[] Boxarray = new TextBox[header.Length];
            Label[] labelarray = new Label[header.Length];
            List<string>[] listArray = new List<string>[header.Length];
            listArray = this.dbConnect.Select(table, primaryField, Convert.ToString(this.id), null);
            ComboBox[] Comboarray = new ComboBox[header.Length];
            List<string>[] listForeign = new List<string>[header.Length];
            List<string>[] listRefer = new List<string>[header.Length];
            for (int i = 0; i < header.Length; i++)
            {
                listForeign = dbConnect.SelectForeign(table, header[i]);
                if (listForeign[0].Count == 0)
                {
                    Boxarray[i] = new System.Windows.Forms.TextBox();
                    Boxarray[i].Location = new Point(140, 30 + (i * 40));
                    this.Controls.Add(Boxarray[i]);
                    Boxarray[i].Text = listArray[i + 1][0];
                    Boxarray[i].Size = new System.Drawing.Size(130, 30);
                    Boxarray[i].Show();
                }
                else
                {
                    Comboarray[i] = new System.Windows.Forms.ComboBox();
                    Comboarray[i].Location = new Point(140, 30 + (i * 40));
                    this.Controls.Add(Comboarray[i]);
                    Comboarray[i].Size = new System.Drawing.Size(130, 30);
                    Comboarray[i].DropDownStyle = ComboBoxStyle.DropDownList;
                    Comboarray[i].Show();
                    string reftable = listForeign[3][0];
                    listRefer = dbConnect.Select(reftable, null, null, null);
                    for (int j = 0; j < listRefer[0].Count; j++)
                    {
                        Comboarray[i].Items.Add(listRefer[0][j] + " (" + listRefer[1][j] + ")");
                    }
                }
                if (i == header.Length - 1) { this.buttonChangeGai.Location = new System.Drawing.Point(65, 30 + (i * 65)); }
                labelarray[i] = new System.Windows.Forms.Label();
                labelarray[i].Location = new Point(50, 30 + (i * 40));
                this.Controls.Add(labelarray[i]);
                labelarray[i].Size = new System.Drawing.Size(120, 30);
                labelarray[i].Text = header[i];
                labelarray[i].Show();
            }
            PubBoxarray = Boxarray;
            Publabelarray = labelarray;
            PubComboarray = Comboarray;
        }

        private void InitializeComponent()
        {
            this.buttonChangeGai = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonChangeGai
            // 
            this.buttonChangeGai.Location = new System.Drawing.Point(0, 0);
            this.buttonChangeGai.Name = "buttonChangeGai";
            this.buttonChangeGai.Size = new System.Drawing.Size(176, 32);
            this.buttonChangeGai.TabIndex = 0;
            this.buttonChangeGai.Text = "Изменить";
            this.buttonChangeGai.UseVisualStyleBackColor = true;
            this.buttonChangeGai.Click += new System.EventHandler(this.buttonChangeGai_Click);
            // 
            // FormGaiChange
            // 
            this.ClientSize = new System.Drawing.Size(302, 262);
            this.Controls.Add(this.buttonChangeGai);
            this.Name = "FormGaiChange";
            this.Text = "Изменение данных";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGaiChange_FormClosing);
            this.Load += new System.EventHandler(this.FormGaiChange_Load);
            this.ResumeLayout(false);

        }
    }
}

