using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;


namespace Library__WPF_.Classes
{
    class SqlConnectClass
    {
        public  SqlConnection sqlCon;
        public SqlDataAdapter adapter;
        public DataTable table = new DataTable();
        public DataSet datSet = new DataSet();

        public  void SqlConnect()
        {
            sqlCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = F:\Артём\Проекты и их материалы\Library(Wpf)\Project\Library-wpf-\Library (WPF)\Library (WPF)\Library.mdf; Integrated Security = True");
            sqlCon.Open();
        }
        public void LoadTable(string command,DataGrid dataGrid)
        {
            table.Clear();
            SqlConnect();
                adapter = new SqlDataAdapter(command, sqlCon);   
                adapter.Fill(table);
                dataGrid.ItemsSource = table.DefaultView;
        }
        public void AddInHistory(String NameBook, String Author, int idClient, String Action)
        {
            SqlCommand command = new SqlCommand("INSERT INTO HistoryAction (DateAction,NameBook,Autor,Client,Status) " +
                "VALUES (@DateAction,@NameBook,@Autor,@Client,@Status)", sqlCon);
            DateTime date = DateTime.Now;
            date.ToString("dd MM yyyy");

            command.Parameters.AddWithValue("DateAction", date);
            command.Parameters.AddWithValue("NameBook", NameBook);
            command.Parameters.AddWithValue("Autor", Author);
            command.Parameters.AddWithValue("Client", idClient);
            command.Parameters.AddWithValue("Status", Action);

            command.ExecuteNonQuery();

        }
    }
}
