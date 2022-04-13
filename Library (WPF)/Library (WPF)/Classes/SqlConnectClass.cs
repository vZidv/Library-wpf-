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
    }
}
