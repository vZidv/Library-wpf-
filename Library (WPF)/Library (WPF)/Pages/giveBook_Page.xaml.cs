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
    }
}
