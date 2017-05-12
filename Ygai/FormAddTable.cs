namespace Ygai
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormAddTable : Form
    {
        private IContainer components = null;
        private DBConnect dbConnect;
        private Button AddTableButton;
        private Button AddFieldButton;
        private Button AddSvyazButton;
        private Button[] PubButtonrray;
        private int xcor=30,ycor=80, ycorForeign=80;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        public TextBox[] Boxarray = new TextBox[0];
        public ComboBox[] Comboarray = new ComboBox[0];
        public TextBox[] ForeignBoxarray = new TextBox[0];
        public ComboBox[] ForeignTableComboarray = new ComboBox[0];
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Label label6;
        private Label label7;
        private Form owner;
        private string server, database, uid, password;
        private TextBox TableNameBox;
        public ComboBox[] ForeignFielComboarray = new ComboBox[0];

        public FormAddTable(DBConnect dbconnect, string server, string database, string uid, string password,Form owner)
        {
            this.owner = owner;
            this.server = server;
            this.database = database;
            this.uid = uid;
            this.password = password;
            this.dbConnect = dbconnect;
            this.InitializeComponent();
        }

        private bool AddForeign(int x, int y)
        {

            if (Boxarray.Length <= ForeignBoxarray.Length)
            {
                MessageBox.Show("Количество связей не может быть больше количества полей в таблице!");
                return false;
            }
            else
            {
                Array.Resize(ref ForeignTableComboarray, ForeignTableComboarray.Length + 1);
                //Array.Resize(ref ForeignFielComboarray, ForeignFielComboarray.Length + 1);
                Array.Resize(ref ForeignBoxarray, ForeignBoxarray.Length + 1);

                ForeignBoxarray[ForeignBoxarray.Length - 1] = new System.Windows.Forms.TextBox();
                ForeignBoxarray[ForeignBoxarray.Length - 1].Location = new Point(x + 270, y);
                this.Controls.Add(ForeignBoxarray[ForeignBoxarray.Length - 1]);
                ForeignBoxarray[ForeignBoxarray.Length - 1].Size = new System.Drawing.Size(130, 30);
                ForeignBoxarray[ForeignBoxarray.Length - 1].Show();
                int tablesCount = dbConnect.ShowTables()[0].Count;
                List<string>[] TableArray = new List<string>[tablesCount];
                TableArray = dbConnect.ShowTables();


                ForeignTableComboarray[ForeignTableComboarray.Length - 1] = new System.Windows.Forms.ComboBox();
                ForeignTableComboarray[ForeignTableComboarray.Length - 1].DropDownStyle = ComboBoxStyle.DropDownList;
                ForeignTableComboarray[ForeignTableComboarray.Length - 1].Location = new Point(x + 470, y);
                this.Controls.Add(ForeignTableComboarray[ForeignTableComboarray.Length - 1]);
                ForeignTableComboarray[ForeignTableComboarray.Length - 1].Size = new System.Drawing.Size(70, 30);
                for (int i = 0; i < tablesCount; i++)
                {
                    ForeignTableComboarray[ForeignTableComboarray.Length - 1].Items.Add(TableArray[0][i]);
                }
                ForeignTableComboarray[ForeignTableComboarray.Length - 1].SelectedIndex = 0;
                ForeignTableComboarray[ForeignTableComboarray.Length - 1].Show();
                return true;
                /* ForeignFielComboarray[ForeignFielComboarray.Length - 1] = new System.Windows.Forms.ComboBox();
                ForeignFielComboarray[ForeignFielComboarray.Length - 1].DropDownStyle = ComboBoxStyle.DropDownList;
                ForeignFielComboarray[ForeignFielComboarray.Length - 1].Location = new Point(x + 660, y);
                //ForeignFielComboarray[ForeignFielComboarray.Length - 1].Items.Add("Int");
                this.Controls.Add(ForeignFielComboarray[ForeignFielComboarray.Length - 1]);
                ForeignFielComboarray[ForeignFielComboarray.Length - 1].Size = new System.Drawing.Size(70, 30);
                int columncount = dbConnect.Describe(ForeignTableComboarray[ForeignTableComboarray.Length - 1].Text)[0].Count;
                List<string>[] listArrayColumn = new List<string>[columncount];
                listArrayColumn = dbConnect.Describe(ForeignTableComboarray[ForeignTableComboarray.Length - 1].Text);
                for (int i = 1; i < columncount; i++)
                {
                    string name = listArrayColumn[0][i];
                    ForeignFielComboarray[ForeignFielComboarray.Length - 1].Items.Add(name);
                }
                ForeignFielComboarray[ForeignFielComboarray.Length - 1].SelectedIndex = 0;
                ForeignFielComboarray[ForeignFielComboarray.Length - 1].Show(); */
            }
            
        }


        private void AddField(int x, int y)
        {
            Array.Resize(ref Comboarray, Comboarray.Length + 1);
            Array.Resize(ref Boxarray, Boxarray.Length+1);

            Boxarray[Boxarray.Length-1] = new System.Windows.Forms.TextBox();
            Boxarray[Boxarray.Length - 1].Location = new Point(x-20, y);
            this.Controls.Add(Boxarray[Boxarray.Length - 1]);
            Boxarray[Boxarray.Length - 1].Size = new System.Drawing.Size(100, 30);
            Boxarray[Boxarray.Length - 1].Show();

            Comboarray[Comboarray.Length - 1] = new System.Windows.Forms.ComboBox();
            Comboarray[Comboarray.Length - 1].DropDownStyle = ComboBoxStyle.DropDownList;
            Comboarray[Comboarray.Length - 1].Location = new Point(x+110, y);
            Comboarray[Comboarray.Length - 1].Items.Add("int");
            Comboarray[Comboarray.Length - 1].Items.Add("varchar(255)");
            this.Controls.Add(Comboarray[Comboarray.Length - 1]);
            Comboarray[Comboarray.Length - 1].Size = new System.Drawing.Size(70, 30);
            Comboarray[Comboarray.Length - 1].SelectedIndex = 0 ;
            Comboarray[Comboarray.Length - 1].Show();
        }
       

       /* protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }*/

        private void InitializeComponent()
        {
            this.AddTableButton = new System.Windows.Forms.Button();
            this.AddFieldButton = new System.Windows.Forms.Button();
            this.AddSvyazButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TableNameBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AddTableButton
            // 
            this.AddTableButton.Location = new System.Drawing.Point(272, 329);
            this.AddTableButton.Name = "AddTableButton";
            this.AddTableButton.Size = new System.Drawing.Size(113, 20);
            this.AddTableButton.TabIndex = 0;
            this.AddTableButton.Text = "Добавить таблицу";
            this.AddTableButton.UseVisualStyleBackColor = true;
            this.AddTableButton.Click += new System.EventHandler(this.AddTableButton_Click);
            // 
            // AddFieldButton
            // 
            this.AddFieldButton.Location = new System.Drawing.Point(49, 329);
            this.AddFieldButton.Name = "AddFieldButton";
            this.AddFieldButton.Size = new System.Drawing.Size(117, 20);
            this.AddFieldButton.TabIndex = 1;
            this.AddFieldButton.Text = "Добавить поле";
            this.AddFieldButton.UseVisualStyleBackColor = true;
            this.AddFieldButton.Click += new System.EventHandler(this.AddFieldButton_Click);
            // 
            // AddSvyazButton
            // 
            this.AddSvyazButton.Location = new System.Drawing.Point(503, 330);
            this.AddSvyazButton.Name = "AddSvyazButton";
            this.AddSvyazButton.Size = new System.Drawing.Size(106, 20);
            this.AddSvyazButton.TabIndex = 2;
            this.AddSvyazButton.Text = "Добавить Связь";
            this.AddSvyazButton.UseVisualStyleBackColor = true;
            this.AddSvyazButton.Click += new System.EventHandler(this.AddSvyazButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Название поля";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Тип Данных";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Название поля Текущие таблицы";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(470, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Название  Таблицы";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(621, 384);
            this.shapeContainer1.TabIndex = 8;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.lineShape1.BorderWidth = 2;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 233;
            this.lineShape1.X2 = 234;
            this.lineShape1.Y1 = 17;
            this.lineShape1.Y2 = 359;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(88, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "Поля";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(412, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 18);
            this.label7.TabIndex = 10;
            this.label7.Text = "Связи";
            // 
            // TableNameBox
            // 
            this.TableNameBox.Location = new System.Drawing.Point(391, 330);
            this.TableNameBox.Name = "TableNameBox";
            this.TableNameBox.Size = new System.Drawing.Size(106, 20);
            this.TableNameBox.TabIndex = 11;
            // 
            // FormAddTable
            // 
            this.ClientSize = new System.Drawing.Size(621, 384);
            this.Controls.Add(this.TableNameBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddSvyazButton);
            this.Controls.Add(this.AddFieldButton);
            this.Controls.Add(this.AddTableButton);
            this.Controls.Add(this.shapeContainer1);
            this.MaximizeBox = false;
            this.Name = "FormAddTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить Таблицу";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAddTable_FormClosed);
            this.Load += new System.EventHandler(this.FormAddTable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AddTableMenu_Click(object sender, EventArgs e)
        {

        }

        private void FormAddTable_Load(object sender, EventArgs e)
        {
        }

        private void AddFieldButton_Click(object sender, EventArgs e)
        {
            AddField(xcor, ycor);
            ycor += 40;

        }

        private void AddSvyazButton_Click(object sender, EventArgs e)
        {
            bool success=AddForeign(xcor, ycorForeign);
            if (success == true) { ycorForeign += 40; }
        }

        private void AddTableButton_Click(object sender, EventArgs e)
        {
            bool pust=false;
            foreach(TextBox box in Boxarray)
            {
                if (box.Text == "") { pust = true; }
            }
            foreach (TextBox box in ForeignBoxarray)
            {
                if (box.Text == "") { pust = true; }
            }
            if (TableNameBox.Text == "") { pust = true; }
            if (pust)
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
              
                    string values = " id int auto_increment , ";
                    for (int i=0;i<Boxarray.Length;i++)
                    {
                        values += " " + Boxarray[i].Text + " "+ Comboarray[i].Text +",";
                    }

                    for (int i = 0; i < ForeignBoxarray.Length; i++)
                    {
                        int columncount = dbConnect.Describe(ForeignTableComboarray[ForeignTableComboarray.Length - 1].Text)[0].Count;
                        List<string>[] listArrayColumn = new List<string>[columncount];
                        listArrayColumn = dbConnect.Describe(ForeignTableComboarray[ForeignTableComboarray.Length - 1].Text);
                        values += " foreign key (" + ForeignBoxarray[i].Text + ") references " + ForeignTableComboarray[i].Text + " (" + listArrayColumn[0][0] + ") , ";
                        
                    }
                    int l = 1;

                        bool sucs=dbConnect.CreateTable(TableNameBox.Text.ToString(), values);
                        if (sucs) 
                        {
                            
                            this.Close();
                            
                        }
                    
                   // new Form1(dbConnect).Show();
                
            }
        }

        private void FormAddTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            new Form1(server, database, uid, password).Show(owner);
        }

      
    }
}

