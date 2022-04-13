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
using System.Data.SqlClient;
using System.Data;

namespace Library__WPF_.Pages
{
    /// <summary>
    /// Логика взаимодействия для giveBook_Page.xaml
    /// </summary>
    public partial class giveBook_Page : Page
    {
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Windows.user_Window user = new Windows.user_Window();

        public giveBook_Page()
        {
            InitializeComponent();

            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (giveBooks_Dg.Columns[0] as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";

            connectClass.LoadTable("SELECT IssuedBooks.LastDate, IssuedBooks.NameBook, IssuedBooks.Autor, ClientsProf.NameClient + ' ' + ClientsProf.Surname + ' ' + ClientsProf.Patronymic[Client]" +
                "FROM IssuedBooks INNER JOIN ClientsProf ON ClientsProf.id = IssuedBooks.Client", giveBooks_Dg);
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {

            connectClass.LoadTable("SELECT IssuedBooks.LastDate, IssuedBooks.NameBook, IssuedBooks.Autor, ClientsProf.NameClient + ' ' + ClientsProf.Surname + ' ' + ClientsProf.Patronymic[Client]" +
              "FROM IssuedBooks INNER JOIN ClientsProf ON ClientsProf.id = IssuedBooks.Client", giveBooks_Dg);
        }

        private void addGiveBook_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.addGiveBook() { user = this.user };
        }

        private void descriptionUseBook_button_Click(object sender, RoutedEventArgs e)
        {
            int r = giveBooks_Dg.SelectedIndex;

            string clientFullName = null;

            DateTime lastDate = new DateTime(2020, 02, 17, 23, 50, 30);
            string nameBook = null;
            string author = null;

            TextBlock itemL = giveBooks_Dg.Columns[3].GetCellContent(giveBooks_Dg.Items[r]) as TextBlock;
            clientFullName = itemL.Text;

            for (int i = 0; i < 4;)
            {
                switch (i)
                {
                    case 0:
                        TextBlock itemS = giveBooks_Dg.Columns[i].GetCellContent(giveBooks_Dg.Items[r]) as TextBlock;
                        lastDate = Convert.ToDateTime(itemS.Text);
                        break;
                    case 1:
                        TextBlock itemP = giveBooks_Dg.Columns[i].GetCellContent(giveBooks_Dg.Items[r]) as TextBlock;
                        nameBook = itemP.Text;
                        break;
                    case 2:
                        TextBlock itemG = giveBooks_Dg.Columns[i].GetCellContent(giveBooks_Dg.Items[r]) as TextBlock;
                        author = itemG.Text;
                        break;
                }
                i++;
            }

            string data = lastDate.ToString("yyyy-MM-dd");
            connectClass.SqlConnect();

            SqlDataAdapter adapter = new SqlDataAdapter($"Select Client From IssuedBooks WHERE LastDate = '{data}' And NameBook = N'{nameBook}' And Autor = N'{author}'", connectClass.sqlCon);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if(table.Rows.Count > 1)
            {

            }
            else
            {
                connectClass.SqlConnect();
                SqlCommand command = new SqlCommand($"Select Client From IssuedBooks WHERE LastDate = '{data}' And NameBook = N'{nameBook}' And Autor = N'{author}'", connectClass.sqlCon);

                user.userMainFrame.Content = new Pages.discriptionGriveBook() { user = this.user , idClient = Convert.ToInt32(command.ExecuteScalar())};
            }
        }
    }
}
