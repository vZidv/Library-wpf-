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

                    DateTime dateTime = new DateTime();
                    dateTime = Convert.ToDateTime( table.Rows[i][2]);
                    command.Parameters.AddWithValue("LastDate", dateTime);
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

        private void descriptionBooks_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void descriptionOverdueBooks_button_Click(object sender, RoutedEventArgs e)
        {
            int r = overdueBooks_Dg.SelectedIndex;

            string clientFullName = null;

            
            string nameBook = null;
            string author = null;
            int idClient = 0;

            TextBlock itemL = overdueBooks_Dg.Columns[1].GetCellContent(overdueBooks_Dg.Items[r]) as TextBlock;
            clientFullName = itemL.Text;

            for (int i = 0; i < 5;)
            {
                switch (i)
                {
                    
                    case 3:
                        TextBlock itemP = overdueBooks_Dg.Columns[i].GetCellContent(overdueBooks_Dg.Items[r]) as TextBlock;
                        nameBook = itemP.Text;
                        break;
                    case 4:
                        TextBlock itemG = overdueBooks_Dg.Columns[i].GetCellContent(overdueBooks_Dg.Items[r]) as TextBlock;
                        author = itemG.Text;
                        break;
                }
                i++;
            }

            
            connectClass.SqlConnect();
            
            SqlDataAdapter adapter = new SqlDataAdapter($"Select Client from OverdueBooks where BookName = N'{nameBook}' and Autor = N'{author}'", connectClass.sqlCon);
            DataTable table = new DataTable();
            adapter.Fill(table);
            

            if (table.Rows.Count > 1)
            {

                for (int i = 0; i < table.Rows.Count;)
                {
                    SqlDataAdapter adapter1 = new SqlDataAdapter($"Select NameClient,Surname,Patronymic From ClientsProf Where id = '{Convert.ToInt32(table.Rows[i][0])}'", connectClass.sqlCon);
                    DataTable newTable = new DataTable();
                    adapter1.Fill(newTable);

                    string FullnameClient = $"{Convert.ToString(newTable.Rows[0][0])} {Convert.ToString(newTable.Rows[0][1])} {Convert.ToString(newTable.Rows[0][2])}";
                    if (FullnameClient == clientFullName)
                    {
                        idClient = Convert.ToInt32(table.Rows[i][0]);
                        enterToDescription();
                        return;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            else
            {
                enterToDescription();
            }
            void enterToDescription()
            {
                connectClass.SqlConnect();
                SqlCommand command = new SqlCommand($"Select id From OverdueBooks WHERE BookName = N'{nameBook}' And Autor = N'{author}' And Client ='{idClient}'", connectClass.sqlCon);
                user.userMainFrame.Content = new Pages.decriptionOverdueBook_Page() { user = this.user, idOverdue = Convert.ToInt32(command.ExecuteScalar()) };
            }
        }
    }
}
