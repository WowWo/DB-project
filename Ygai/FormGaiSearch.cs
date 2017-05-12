namespace Ygai
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormGaiSearch : Form
    {
        private Button button1;
        private IContainer components = null;
        private DataGridView data;
        public DBConnect dbConnect;
        private DataGridView dataGridView1;
        private Label label1;
        public string table;
        private Button ExportGaiSearchbutton;
        private Button buttonDeleteGai;
        private Button buttonShowChangeGai;
        private ComboBox ExportSearchGaiBox;

        public FormGaiSearch(DataGridView datagrid, string table,DBConnect dbconnect)
        {
            this.InitializeComponent();
            this.data = datagrid;
            this.dbConnect = dbconnect;
            this.table = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormGaiSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            int num;
            for (num = 0; num < this.dataGridView1.Rows.Count; num++)
            {
                this.dataGridView1.Rows.RemoveAt(0);
            }
            for (num = 0; num < this.data.Rows.Count; num++)
            {
                this.data.Rows.RemoveAt(0);
            }
            new FormGai(table,dbConnect).Show();
        }

        private void FormGaiSearch_Load(object sender, EventArgs e)
        {
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.AllowUserToAddRows = false;
            ExportSearchGaiBox.SelectedIndex = 0;
            int columncount = data.ColumnCount;
            for (int i = 0; i < columncount; i++)
            {
                string name = data.Columns[i].HeaderText;
                dataGridView1.Columns.Add("Column1" + i.ToString(), name);
            }
            dataGridView1.Columns[0].Visible = false;
            for (int i = 0; i < this.data.Rows.Count; i++)
            {
                this.dataGridView1.Rows.Add();
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    this.dataGridView1.Rows[i].Cells[j].Value = this.data.Rows[i].Cells[j].Value;

                }
             
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.ExportGaiSearchbutton = new System.Windows.Forms.Button();
            this.ExportSearchGaiBox = new System.Windows.Forms.ComboBox();
            this.buttonDeleteGai = new System.Windows.Forms.Button();
            this.buttonShowChangeGai = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(129, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Результаты поиска ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(374, 138);
            this.dataGridView1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(392, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 24);
            this.button1.TabIndex = 8;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExportGaiSearchbutton
            // 
            this.ExportGaiSearchbutton.Location = new System.Drawing.Point(392, 95);
            this.ExportGaiSearchbutton.Name = "ExportGaiSearchbutton";
            this.ExportGaiSearchbutton.Size = new System.Drawing.Size(87, 23);
            this.ExportGaiSearchbutton.TabIndex = 9;
            this.ExportGaiSearchbutton.Text = "Экспорт";
            this.ExportGaiSearchbutton.UseVisualStyleBackColor = true;
            this.ExportGaiSearchbutton.Click += new System.EventHandler(this.ExportGaiSearchbutton_Click);
            // 
            // ExportSearchGaiBox
            // 
            this.ExportSearchGaiBox.AutoCompleteCustomSource.AddRange(new string[] {
            "ID",
            "Названию",
            "Адрессу",
            "Области"});
            this.ExportSearchGaiBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExportSearchGaiBox.FormattingEnabled = true;
            this.ExportSearchGaiBox.Items.AddRange(new object[] {
            "Excel",
            "PDF"});
            this.ExportSearchGaiBox.Location = new System.Drawing.Point(392, 124);
            this.ExportSearchGaiBox.Name = "ExportSearchGaiBox";
            this.ExportSearchGaiBox.Size = new System.Drawing.Size(87, 21);
            this.ExportSearchGaiBox.TabIndex = 11;
            // 
            // buttonDeleteGai
            // 
            this.buttonDeleteGai.Location = new System.Drawing.Point(392, 66);
            this.buttonDeleteGai.Name = "buttonDeleteGai";
            this.buttonDeleteGai.Size = new System.Drawing.Size(87, 23);
            this.buttonDeleteGai.TabIndex = 13;
            this.buttonDeleteGai.Text = "Удалить";
            this.buttonDeleteGai.UseVisualStyleBackColor = true;
            this.buttonDeleteGai.Click += new System.EventHandler(this.buttonDeleteGai_Click);
            // 
            // buttonShowChangeGai
            // 
            this.buttonShowChangeGai.Location = new System.Drawing.Point(392, 37);
            this.buttonShowChangeGai.Name = "buttonShowChangeGai";
            this.buttonShowChangeGai.Size = new System.Drawing.Size(87, 23);
            this.buttonShowChangeGai.TabIndex = 12;
            this.buttonShowChangeGai.Text = "Изменить";
            this.buttonShowChangeGai.UseVisualStyleBackColor = true;
            this.buttonShowChangeGai.Click += new System.EventHandler(this.buttonShowChangeGai_Click);
            // 
            // FormGaiSearch
            // 
            this.ClientSize = new System.Drawing.Size(485, 201);
            this.Controls.Add(this.buttonDeleteGai);
            this.Controls.Add(this.buttonShowChangeGai);
            this.Controls.Add(this.ExportSearchGaiBox);
            this.Controls.Add(this.ExportGaiSearchbutton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "FormGaiSearch";
            this.Text = "Результаты поиска";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGaiSearch_FormClosing);
            this.Load += new System.EventHandler(this.FormGaiSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void ExportGaiSearchbutton_Click(object sender, EventArgs e)
        {
            switch (ExportSearchGaiBox.SelectedIndex)
            {
                case 0: { dbConnect.ExportExcel(dataGridView1); break; }
                case 1: { dbConnect.ExportPDF(dataGridView1,"SearchGai"); break; }    
            }
        }

        private void buttonShowChangeGai_Click(object sender, EventArgs e)
        {
            string[] HeadeList = new string[dataGridView1.Columns.Count - 1];
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                HeadeList[i - 1] = dataGridView1.Columns[i].HeaderText;
            }
            string primaryField = dataGridView1.Columns[0].HeaderText;
            FormGaiChange change = new FormGaiChange(Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value), HeadeList,table,dbConnect,primaryField);
            change.Show();      
        }

        private void buttonDeleteGai_Click(object sender, EventArgs e)
        {
                this.dbConnect.Delete(table, Convert.ToString(dataGridView1[0, dataGridView1.CurrentRow.Index].Value));
                for (int i = 0; i <= this.dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToString(this.dataGridView1.Rows[i].Cells[0].Value) == Convert.ToString(dataGridView1[0, dataGridView1.CurrentRow.Index].Value))
                    {
                        string str = Convert.ToString(this.dataGridView1.Rows[i].Cells[0].Value);
                        this.dataGridView1.Rows.RemoveAt(i);
                        break;
                    }
                }
                MessageBox.Show("Данные успешно удалены");
        }

        
    }
}

