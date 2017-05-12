namespace Ygai
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormGaiAdd : Form
    {
        private Button buttonAddGai;
        private IContainer components = null;
        private DBConnect dbConnect;
        private string[] header;
        public string table;
        private TextBox[] PubBoxarray;
        private Label[] Publabelarray;
        private ComboBox[] PubComboarray;
        public FormGaiAdd(string[] headerList,string table,DBConnect dbconnect)
        {
            this.InitializeComponent();
            this.dbConnect = dbconnect;
            this.header = headerList;
            this.table = table;
           
        }

        private void buttonAddGai_Click(object sender, EventArgs e)
        {
            string[] values = new string[header.Length + 1];
            int num = this.dbConnect.Max(table) + 1;
            bool pust = false;
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
                values[0] = num.ToString() + " , ";
                for (int i = 1; i < header.Length+1; i++)
                {
                    if (PubBoxarray[i - 1] == null)
                    {
                        string[] val = PubComboarray[i - 1].Text.Split(' ');
                        if (i != header.Length)
                            
                            values[i] = "'" + val[0] + "' , ";
                        else
                            values[i] = "'" + val[0] + "'";
                    }
                    else
                    {
                        if (i != header.Length)
                            values[i] = "'" + PubBoxarray[i - 1].Text + "' , ";
                        else
                            values[i] = "'" + PubBoxarray[i - 1].Text + "'";
                    }
                }
                string value = "";
                for (int i = 0; i < header.Length + 1; i++)
                {
                    value += values[i];
                }
               
                    this.dbConnect.Insert(table, value);
                   
               
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

        private void FormGaiAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            new FormGai(table,dbConnect).Show();
        }

        private void InitializeComponent()
        {
            this.buttonAddGai = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonAddGai
            // 
            this.buttonAddGai.Location = new System.Drawing.Point(72, 189);
            this.buttonAddGai.Name = "buttonAddGai";
            this.buttonAddGai.Size = new System.Drawing.Size(176, 32);
            this.buttonAddGai.TabIndex = 0;
            this.buttonAddGai.Text = "Добавить";
            this.buttonAddGai.UseVisualStyleBackColor = true;
            this.buttonAddGai.Click += new System.EventHandler(this.buttonAddGai_Click);
            // 
            // FormGaiAdd
            // 
            this.ClientSize = new System.Drawing.Size(302, 262);
            this.Controls.Add(this.buttonAddGai);
            this.Name = "FormGaiAdd";
            this.Text = "Добавление данных";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGaiAdd_FormClosing);
            this.Load += new System.EventHandler(this.FormGaiAdd_Load);
            this.ResumeLayout(false);

        }

        private void FormGaiAdd_Load(object sender, EventArgs e)
        {
           
            TextBox[] Boxarray = new TextBox[header.Length];
            Label[] labelarray = new Label[header.Length];
            ComboBox[] Comboarray = new ComboBox[header.Length];
            List<string>[] listForeign = new List<string>[header.Length];
            List<string>[] listRefer = new List<string>[header.Length];
            for (int i = 0; i < header.Length; i++)
            {
                listForeign = dbConnect.SelectForeign(table, header[i]);
                if (listForeign[0].Count== 0)
                {
                    Boxarray[i] = new System.Windows.Forms.TextBox();
                    Boxarray[i].Location = new Point(140, 30 + (i * 40));
                    this.Controls.Add(Boxarray[i]);
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
                        Comboarray[i].Items.Add(listRefer[0][j] + " ("+listRefer[1][j] +")");
                    }
                }
                if (i == header.Length - 1) { this.buttonAddGai.Location = new System.Drawing.Point(65, 30 + (i * 65)); }
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
    }
}

