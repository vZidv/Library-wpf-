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
    /// Логика взаимодействия для addGiveBook.xaml
    /// </summary>
    public partial class addGiveBook : Page
    {
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Windows.user_Window user = new Windows.user_Window();

        public addGiveBook()
        {
            InitializeComponent();

           // connectClass.LoadTable("Select NameBook,Autor,Genre,NumberOfBooks From Books ", books_Dg);
            connectClass.LoadTable("Select NameClient,Surname,Patronymic From ClientsProf", clients_Dg);

            connectClass.SqlConnect();
            SqlDataAdapter adapter = new SqlDataAdapter("Select NameBook,Autor,Genre,NumberOfBooks From Books ", connectClass.sqlCon);
            DataTable table = new DataTable();
            adapter.Fill(table);
            books_Dg.ItemsSource = table.DefaultView;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.giveBook_Page() { user = this.user };
        }

        private void chooseClient_button_Click(object sender, RoutedEventArgs e)
        {
            int r = clients_Dg.SelectedIndex;

            string name = null;
            string secondName = null;
            string patronymic = null;

            for (int i = 0; i < 4;)
            {
                switch (i)
                {
                    case 0:
                        TextBlock itemL = clients_Dg.Columns[i].GetCellContent(clients_Dg.Items[r]) as TextBlock;
                        name = itemL.Text;
                        break;
                    case 1:
                        TextBlock itemP = clients_Dg.Columns[i].GetCellContent(clients_Dg.Items[r]) as TextBlock;
                        secondName = itemP.Text;
                        break;
                    case 2:
                        TextBlock itemG = clients_Dg.Columns[i].GetCellContent(clients_Dg.Items[r]) as TextBlock;
                        patronymic = itemG.Text;
                        break;

                }
                i++;
            }
            fullNameClient_textBox.Text = name + " " + secondName + " " + patronymic;
        }
    }
}
