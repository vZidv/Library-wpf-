using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace Library__WPF_.Pages
{
    /// <summary>
    /// Логика взаимодействия для overdueBooks_Page.xaml
    /// </summary>
    public partial class overdueBooks_Page : Page
    {
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Windows.user_Window user = new Windows.user_Window();

        public overdueBooks_Page()
        {
            InitializeComponent();

            CheckLostBook();
            connectClass.LoadTable("SELECT OverdueBooks.LastDate,ClientsProf.NameClient +' '+ ClientsProf.Surname +' '+ ClientsProf.Patronymic [Client],OverdueBooks.NumberOfPhone,OverdueBooks.BookName,OverdueBooks.Autor " +
                "FROM OverdueBooks INNER JOIN ClientsProf ON ClientsProf.id = OverdueBooks.Client", overdueBooks_Dg);
        }
        public void CheckLostBook()
        {
            connectClass.SqlConnect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM IssuedBooks", connectClass.sqlCon);
            DataTable table = new DataTable();
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DateTime date = Convert.ToDateTime(table.Rows[i][2].ToString());
                if (date < DateTime.Now)
                {
                    SqlCommand commNumber = new SqlCommand($"Select NumberOfPhone FROM ClientsProf WHERE id ={table.Rows[i][5]}", connectClass.sqlCon);
                    string numberOfPhone = Convert.ToString(commNumber.ExecuteScalar());

                    SqlCommand command = new SqlCommand("INSERT INTO OverdueBooks (LastDate,Client,NumberOfPhone,BookName,Autor) " +
                        "VALUES (@LastDate,@Client,@NumberOfPhone,@BookName,@Autor)", connectClass.sqlCon);
                    command.Parameters.AddWithValue("LastDate", table.Rows[i][2]);
                    command.Parameters.AddWithValue("Client", table.Rows[i][5]);
                    command.Parameters.AddWithValue("NumberOfPhone", numberOfPhone);
                    command.Parameters.AddWithValue("BookName", table.Rows[i][3]);
                    command.Parameters.AddWithValue("Autor", table.Rows[i][4]);

                    command.ExecuteNonQuery();
                    SqlCommand commandDelete = new SqlCommand($"DELETE FROM IssuedBooks WHERE id = {table.Rows[i][0]}", connectClass.sqlCon);
                    commandDelete.ExecuteNonQuery();
                }
            }
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            CheckLostBook();
            connectClass.LoadTable("SELECT OverdueBooks.LastDate,ClientsProf.NameClient +' '+ ClientsProf.Surname +' '+ ClientsProf.Patronymic [Client],OverdueBooks.NumberOfPhone,OverdueBooks.BookName,OverdueBooks.Autor " +
                "FROM OverdueBooks INNER JOIN ClientsProf ON ClientsProf.id = OverdueBooks.Client", overdueBooks_Dg);
        }
    }
}
