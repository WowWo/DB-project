namespace Ygai
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public class FormGai : Form
    {
        private Button buttonDeleteGai;
        private Button buttonShowAddGai;
        private Button buttonShowChangeGai;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DBConnect dbConnect;
        private Label label1;
        public string table;
        private Button SearchGaibutton;
        private TextBox SearchGaiText;
        private Button ShowGaiButton;
        private ComboBox ExportGaiBox;
        private Button ExportGaibutton;
        private ComboBox SortGaiBox;
        private ComboBox SearchGaiBox;

        public FormGai(string table, DBConnect dbconnect)
        {
            this.InitializeComponent();
            this.dbConnect =dbconnect;
            this.table = table;
        }

        private void buttonDeleteGai_Click(object sender, EventArgs e)
        {
            try
            {
                this.dbConnect.Delete(table, Convert.ToString(dataGridView1[0,dataGridView1.CurrentRow.Index].Value));
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
            catch { MessageBox.Show("Сначала удалить связанные с ней данные!"); }
        }
        
        private void buttonShowAddGai_Click(object sender, EventArgs e)
        {
            
            string[] HeadeList = new string[dataGridView1.Columns.Count - 1];
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                HeadeList[i - 1] = dataGridView1.Columns[i].HeaderText;
            }
            base.Close();
            FormGaiAdd add = new FormGaiAdd(HeadeList,table,dbConnect);
            add.Show();
        }

        private void buttonShowChangeGai_Click(object sender, EventArgs e)
        {
                string[] HeadeList = new string[dataGridView1.Columns.Count-1];
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    HeadeList[i-1] = dataGridView1.Columns[i].HeaderText;
                }
                string primaryField = dataGridView1.Columns[0].HeaderText;
                FormGaiChange change = new FormGaiChange(Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value),HeadeList,table,dbConnect,primaryField);
                this.Close();
                change.Show();          
        }

       

        private void FormGai_Load(object sender, EventArgs e)
        {
            label1.Text = table;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            ExportGaiBox.SelectedIndex = 0;
            dataGridView1.AllowUserToResizeRows = false;
            
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToAddRows = false;
            int columncount = this.dbConnect.Select(table, null, null, null).Count();
            List<string>[] listArray = new List<string>[columncount];
            List<string>[] listArrayColumn = new List<string>[columncount];
            
            listArray = this.dbConnect.Select(table, null, null, null);
            listArrayColumn = this.dbConnect.SelectColumn(table, null, null, null);
            int num = listArray[0].Count<string>();
            for (int i = 0; i < columncount; i++)
            {
                string name = listArrayColumn[i][0];
                dataGridView1.Columns.Add("Column1"+i.ToString(), name);
                dataGridView2.Columns.Add("Column1" + i.ToString(), name);
                if (i != 0) { SearchGaiBox.Items.Add(name); SortGaiBox.Items.Add(name); }
            }
            this.SortGaiBox.SelectedIndex = 0;
            this.SearchGaiBox.SelectedIndex = 0;
            dataGridView1.Columns[0].Visible=false;
            for (int i = 0; i < num; i++)
            {
                this.dataGridView1.Rows.Add();
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    this.dataGridView1.Rows[i].Cells[j].Value = listArray[j][i];
                    
                }
            }
        }

        private void IdGaitext_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar <= '/') || (e.KeyChar >= ':')) && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.buttonShowAddGai = new System.Windows.Forms.Button();
            this.buttonShowChangeGai = new System.Windows.Forms.Button();
            this.buttonDeleteGai = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ShowGaiButton = new System.Windows.Forms.Button();
            this.SearchGaibutton = new System.Windows.Forms.Button();
            this.SearchGaiText = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ExportGaiBox = new System.Windows.Forms.ComboBox();
            this.ExportGaibutton = new System.Windows.Forms.Button();
            this.SearchGaiBox = new System.Windows.Forms.ComboBox();
            this.SortGaiBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "asd";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonShowAddGai
            // 
            this.buttonShowAddGai.Location = new System.Drawing.Point(375, 68);
            this.buttonShowAddGai.Name = "buttonShowAddGai";
            this.buttonShowAddGai.Size = new System.Drawing.Size(75, 23);
            this.buttonShowAddGai.TabIndex = 1;
            this.buttonShowAddGai.Text = "Добавить";
            this.buttonShowAddGai.UseVisualStyleBackColor = true;
            this.buttonShowAddGai.Click += new System.EventHandler(this.buttonShowAddGai_Click);
            // 
            // buttonShowChangeGai
            // 
            this.buttonShowChangeGai.Location = new System.Drawing.Point(375, 97);
            this.buttonShowChangeGai.Name = "buttonShowChangeGai";
            this.buttonShowChangeGai.Size = new System.Drawing.Size(75, 23);
            this.buttonShowChangeGai.TabIndex = 2;
            this.buttonShowChangeGai.Text = "Изменить";
            this.buttonShowChangeGai.UseVisualStyleBackColor = true;
            this.buttonShowChangeGai.Click += new System.EventHandler(this.buttonShowChangeGai_Click);
            // 
            // buttonDeleteGai
            // 
            this.buttonDeleteGai.Location = new System.Drawing.Point(375, 126);
            this.buttonDeleteGai.Name = "buttonDeleteGai";
            this.buttonDeleteGai.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteGai.TabIndex = 3;
            this.buttonDeleteGai.Text = "Удалить";
            this.buttonDeleteGai.UseVisualStyleBackColor = true;
            this.buttonDeleteGai.Click += new System.EventHandler(this.buttonDeleteGai_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 68);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(357, 137);
            this.dataGridView1.TabIndex = 6;
            // 
            // ShowGaiButton
            // 
            this.ShowGaiButton.Location = new System.Drawing.Point(375, 39);
            this.ShowGaiButton.Name = "ShowGaiButton";
            this.ShowGaiButton.Size = new System.Drawing.Size(75, 23);
            this.ShowGaiButton.TabIndex = 7;
            this.ShowGaiButton.Text = "Показать";
            this.ShowGaiButton.UseVisualStyleBackColor = true;
            this.ShowGaiButton.Click += new System.EventHandler(this.ShowGaiButton_Click);
            // 
            // SearchGaibutton
            // 
            this.SearchGaibutton.Location = new System.Drawing.Point(14, 211);
            this.SearchGaibutton.Name = "SearchGaibutton";
            this.SearchGaibutton.Size = new System.Drawing.Size(112, 23);
            this.SearchGaibutton.TabIndex = 9;
            this.SearchGaibutton.Text = "Найти по :";
            this.SearchGaibutton.UseVisualStyleBackColor = true;
            this.SearchGaibutton.Click += new System.EventHandler(this.SearchGaibutton_Click);
            // 
            // SearchGaiText
            // 
            this.SearchGaiText.Location = new System.Drawing.Point(373, 214);
            this.SearchGaiText.Name = "SearchGaiText";
            this.SearchGaiText.Size = new System.Drawing.Size(77, 20);
            this.SearchGaiText.TabIndex = 11;
            this.SearchGaiText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchGaiText_KeyPress);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(379, 8);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(28, 25);
            this.dataGridView2.TabIndex = 12;
            this.dataGridView2.Visible = false;
            // 
            // ExportGaiBox
            // 
            this.ExportGaiBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Excel",
            "PDF"});
            this.ExportGaiBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExportGaiBox.FormattingEnabled = true;
            this.ExportGaiBox.Items.AddRange(new object[] {
            "Excel",
            "PDF"});
            this.ExportGaiBox.Location = new System.Drawing.Point(375, 184);
            this.ExportGaiBox.Name = "ExportGaiBox";
            this.ExportGaiBox.Size = new System.Drawing.Size(75, 21);
            this.ExportGaiBox.TabIndex = 14;
            // 
            // ExportGaibutton
            // 
            this.ExportGaibutton.Location = new System.Drawing.Point(375, 155);
            this.ExportGaibutton.Name = "ExportGaibutton";
            this.ExportGaibutton.Size = new System.Drawing.Size(75, 23);
            this.ExportGaibutton.TabIndex = 13;
            this.ExportGaibutton.Text = "Экспорт";
            this.ExportGaibutton.UseVisualStyleBackColor = true;
            this.ExportGaibutton.Click += new System.EventHandler(this.ExportGaibutton_Click);
            // 
            // SearchGaiBox
            // 
            this.SearchGaiBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchGaiBox.FormattingEnabled = true;
            this.SearchGaiBox.Location = new System.Drawing.Point(132, 213);
            this.SearchGaiBox.Name = "SearchGaiBox";
            this.SearchGaiBox.Size = new System.Drawing.Size(235, 21);
            this.SearchGaiBox.TabIndex = 17;
            // 
            // SortGaiBox
            // 
            this.SortGaiBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SortGaiBox.FormattingEnabled = true;
            this.SortGaiBox.Location = new System.Drawing.Point(12, 41);
            this.SortGaiBox.Name = "SortGaiBox";
            this.SortGaiBox.Size = new System.Drawing.Size(357, 21);
            this.SortGaiBox.TabIndex = 18;
            // 
            // FormGai
            // 
            this.AllowDrop = true;
            this.ClientSize = new System.Drawing.Size(462, 253);
            this.Controls.Add(this.SortGaiBox);
            this.Controls.Add(this.SearchGaiBox);
            this.Controls.Add(this.ExportGaiBox);
            this.Controls.Add(this.ExportGaibutton);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.SearchGaiText);
            this.Controls.Add(this.SearchGaibutton);
            this.Controls.Add(this.ShowGaiButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonDeleteGai);
            this.Controls.Add(this.buttonShowChangeGai);
            this.Controls.Add(this.buttonShowAddGai);
            this.Controls.Add(this.label1);
            this.Name = "FormGai";
            this.Text = "Работа с таблицей";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGai_FormClosing);
            this.Load += new System.EventHandler(this.FormGai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SearchGaibutton_Click(object sender, EventArgs e)
        {
            this.dataGridView2.AllowUserToAddRows = false;
            List<string>[] listArray = new List<string>[dataGridView1.Columns.Count];
            this.SortGaiBox.SelectedIndex = 0;
            string data = this.SearchGaiText.Text.ToString();
            string field = SearchGaiBox.Items[SearchGaiBox.SelectedIndex].ToString();
            listArray = this.dbConnect.Select(table,field , data, null);
            int num = listArray[0].Count<string>();
            for (int i = 0; i < num; i++)
            {
                this.dataGridView2.Rows.Add();
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    this.dataGridView2.Rows[i].Cells[j].Value = listArray[j][i];

                }
            }
            if (this.dataGridView2.Rows.Count != 0)
            {
                FormGaiSearch search = new FormGaiSearch(this.dataGridView2,table,dbConnect);
                search.Show();
                this.Close();
                
            }
            else
            {
                MessageBox.Show("Ничего не найдено!");
            }
        }

        private void SearchGaiText_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void ShowGaiButton_Click(object sender, EventArgs e)
        {
            string order = SortGaiBox.Items[SortGaiBox.SelectedIndex].ToString();
            List<string>[] listArray = new List<string>[4];
           
            listArray = this.dbConnect.Select(table, null, null, order);
            int num = listArray[0].Count<string>();
            for (int i = 0; i < num; i++)
            {
                this.dataGridView1.Rows[i].Cells[0].Value = listArray[0][i];
                this.dataGridView1.Rows[i].Cells[1].Value = listArray[1][i];
                this.dataGridView1.Rows[i].Cells[2].Value = listArray[2][i];
                this.dataGridView1.Rows[i].Cells[3].Value = listArray[3][i];
            }
        }

        private void ExportGaibutton_Click(object sender, EventArgs e)
        {
            switch (ExportGaiBox.SelectedIndex)
            {
                case 0: { dbConnect.ExportExcel(dataGridView1); break; }
                case 1: { dbConnect.ExportPDF(dataGridView1,table); break; }
            }
        }

        private void FormGai_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

