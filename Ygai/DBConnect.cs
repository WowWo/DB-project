namespace Ygai
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using Microsoft.Office.Interop.Excel;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class DBConnect
    {
        private string charset;
        private MySqlConnection connection;
        private string database;
        private string password;
        private string server;
        private string uid;

        public DBConnect(string server, string database, string uid, string password)
        {
            this.server = server;
            this.database = database;
            this.uid = uid;
            this.password = password;
            this.Initialize(server, database, uid, password);
            //this.Initialize();
        }

       

        private bool CloseConnection()
        {
            try
            {
                this.connection.Close();
                return true;
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
        }

        public int Count(string table)
        {
            string query = "SELECT Count(*) FROM " + table;
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        public void Delete(string table, string id)
        {
            string cmdText = "DELETE FROM " + table + " WHERE id=" + id;
            if (this.OpenConnection())
            {
                
                    new MySqlCommand(cmdText, this.connection).ExecuteNonQuery();
                    this.CloseConnection();
                
                
            }
        }

        private void Initialize(string server, string database,string uid,string password)
        {
           // this.server = "localhost";
           // this.database = "Ygai";
          //  this.uid = "root";
          //  this.password="2602753";
            this.charset = "utf8";
            string connectionString = "SERVER=" + this.server + ";DATABASE=" + this.database + ";UID=" + this.uid + ";PASSWORD=" + this.password + ";CharSet=" + this.charset + ";";
            this.connection = new MySqlConnection(connectionString);
        }
        public bool CreateTable( string table,string data)
        {
            bool cusc = false ;
            string cmdText = "Create table  " + table + " (" + data + " primary key(id))";
            if (this.OpenConnection())
            {
                cusc = true;
                
                try
                {
                    new MySqlCommand(cmdText, this.connection).ExecuteNonQuery();
                    MessageBox.Show("Таблица добавлена!");
                }
                catch 
                { MessageBox.Show("Заполните данные верно!"); this.CloseConnection(); cusc = false; }

                
                this.CloseConnection();
            }
            return cusc;
        }

        public void Insert(string table, string value)
        {
            string cmdText = "INSERT INTO " + table + " VALUES (" + value + ")";
            if (this.OpenConnection())
            {
                try
                {
                    new MySqlCommand(cmdText, this.connection).ExecuteNonQuery();
                    MessageBox.Show("Данные успешно Добавлены!");
                }
                catch { this.CloseConnection(); MessageBox.Show("Введите данные правильно!"); }

              
                this.CloseConnection();
            }
        }

        public bool DropTable(string table)
        {
            string cmdText = "drop table " + table;
            bool susc=false;
            if (this.OpenConnection())
            {
                try
                {

                new MySqlCommand(cmdText, this.connection).ExecuteNonQuery();
                    susc=true;
                }
                catch{MessageBox.Show("Нельзя удалить таблицу т.к с ней связаны другие таблицы!");}



                this.CloseConnection();
            }
            return susc;
        }
        public void ExportExcel(DataGridView data)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 30;
           // ExcelApp.Rows.
            for (int i = 1; i < data.ColumnCount; i++)
            {
                ExcelApp.Cells[1, i] = Convert.ToString(data.Columns[i].HeaderText);
                for (int j = 0; j < data.RowCount; j++)
                {
                    ExcelApp.Cells[j + 2, i] = (data[i, j].Value).ToString();
                }
            }
            ExcelApp.Visible = true;
        }
        public void ExportPDF(DataGridView data, string name)
        {
            BaseFont fnt = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf",
            System.Text.Encoding.GetEncoding(1251).BodyName, true);
            iTextSharp.text.Font font = new iTextSharp.text.Font(fnt, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            PdfPTable pdfTable = new PdfPTable(data.ColumnCount-1);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 2;
      

            //Adding Header row
            for(int i=1;i<data.ColumnCount;i++)
            {
                if (data.Columns[i].HeaderText!= "")
                {
                    PdfPCell cell = new PdfPCell(new Phrase(data.Columns[i].HeaderText,font));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    pdfTable.AddCell(cell);
                }
            }

            //Adding DataRow
            for (int i = 0; i < data.RowCount; i++)
            {
                for (int j = 1; j < data.ColumnCount; j++)
                {
                    if (data[j, i].Value != null)
                        pdfTable.AddCell(new PdfPCell(new Phrase(data[j, i].Value.ToString(), font)));
                }
            }
           

            //Exporting to PDF
            string folderPath = Convert.ToString(System.Windows.Forms.Application.StartupPath) + @"\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + name + ".pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 15f, 15f, 15f, 15f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
               
            }
           
        }

        public int Max(string table)
        {
            string query = "SELECT MAX(id) FROM " + table;
            int MAX = 0;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                try
                {
                    MAX = int.Parse(cmd.ExecuteScalar() + "");
                }
                catch { this.CloseConnection(); return 0; }

                //close Connection
                this.CloseConnection();

                return MAX;
            }
            else
            {
                return MAX;
            }
        }

        private bool OpenConnection()
        {
            try
            {
                this.connection.Open();
                return true;
            }
            catch (MySqlException exception)
            {
                switch (exception.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 0x415:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

      
        public List<string>[] SelectColumn(string table, string field, string data, string order)
        {
            string query;
            if (order == null)
            {

                if ((field == null) || (data == null))
                {
                    query = "SELECT * FROM " + table;
                }
                else
                {
                    query = "SELECT * FROM " + table + " where " + field + "='" + data + "'";
                }
            }
            else
            {
                if ((field == null) || (data == null))
                {
                    query = "SELECT * FROM " + table + " ORDER BY " + order;
                }
                else
                {
                    query = "SELECT * FROM " + table + " where " + field + "='" + data + "' ORDER BY " + order;
                }
            }
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int fieldcount = dataReader.FieldCount;

                List<string>[] list = new List<string>[fieldcount];
                for (int i = 0; i < fieldcount; i++)
                {
                    list[i] = new List<string>();
                }
                //Read the data and store them in the list
                
                    for (int i = 0; i < fieldcount; i++)
                    {
                        string name = dataReader.GetName(i);
                        list[i].Add(name);
                    }
                

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                List<string>[] list = new List<string>[1];
                return list;
            }
        }

        public List<string>[] Select(string table, string field, string data, string order)
        {
            string query;
            if (order == null)
            {

                if ((field == null) || (data == null))
                {
                    query = "SELECT * FROM " + table;
                }
                else
                {
                    query = "SELECT * FROM " + table + " where " + field + "='" + data + "'" ;
                }
            }
            else
            {
                if ((field == null) || (data == null))
                {
                    query = "SELECT * FROM " + table + " ORDER BY " + order;
                }
                else
                {
                    query = "SELECT * FROM " + table + " where " + field + "='" + data + "' ORDER BY " + order;
                }
            }
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int fieldcount = dataReader.FieldCount;

                List<string>[] list = new List<string>[fieldcount];
                for (int i = 0; i < fieldcount; i++)
                {
                    list[i] = new List<string>();
                }
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    for (int i = 0; i < fieldcount; i++)
                    {
                        string name = dataReader.GetName(i);
                        list[i].Add(dataReader[name] + "");
                    }
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                List<string>[] list = new List<string>[1];
                return list;
            }
        }

        public void Update(string table, string values)
        {
            string str = "UPDATE " + table + " SET " + values;
            if (this.OpenConnection())
            {
                try
                {
                    new MySqlCommand { CommandText = str, Connection = this.connection }.ExecuteNonQuery();
                    this.CloseConnection();
                    MessageBox.Show("Данные успешно Изменены!");
                }
                catch { this.CloseConnection(); MessageBox.Show("Введите данные правильно!"); }
            }
        }


        public List<string>[] SelectForeign(string table, string field)
        {
            string query;
            query = "SELECT TABLE_NAME,COLUMN_NAME,CONSTRAINT_NAME, REFERENCED_TABLE_NAME,REFERENCED_COLUMN_NAME  FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE where TABLE_NAME ='" + table + "' AND COLUMN_NAME = '" + field + " ' AND CONSTRAINT_NAME !='Primary'";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int fieldcount = dataReader.FieldCount;

                List<string>[] list = new List<string>[fieldcount];
                for (int i = 0; i < fieldcount; i++)
                {
                    list[i] = new List<string>();
                }
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    for (int i = 0; i < fieldcount; i++)
                    {
                        string name = dataReader.GetName(i);
                        list[i].Add(dataReader[name] + "");
                    }
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                List<string>[] list = new List<string>[1];
                return list;
            }
        }



        public List<string>[] Describe(string tables)
        {
            string query;
            query = "desc "+ tables;
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int fieldcount = dataReader.FieldCount;

                List<string>[] list = new List<string>[fieldcount];
                for (int i = 0; i < fieldcount; i++)
                {
                    list[i] = new List<string>();
                }
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    for (int i = 0; i < fieldcount; i++)
                    {
                        string name = dataReader.GetName(i);
                        list[i].Add(dataReader[name] + "");
                    }
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                List<string>[] list = new List<string>[1];
                return list;
            }
        }


        public List<string>[] ShowTables()
        {
            string query;
            query = "show tables";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int fieldcount = dataReader.FieldCount;

                List<string>[] list = new List<string>[fieldcount];
                for (int i = 0; i < fieldcount; i++)
                {
                    list[i] = new List<string>();
                }
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    for (int i = 0; i < fieldcount; i++)
                    {
                        string name = dataReader.GetName(i);
                        list[i].Add(dataReader[name] + "");
                    }
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                List<string>[] list = new List<string>[1];
                return list;
            }
        }

        
       
    }
}

