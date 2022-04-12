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

namespace Library__WPF_.Pages
{
    /// <summary>
    /// Логика взаимодействия для Books_Page.xaml
    /// </summary>
    public partial class Books_Page : Page
    {
        public Windows.user_Window user = new Windows.user_Window();
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Books_Page()
        {
            InitializeComponent();
            connectClass.LoadTable("Select NameBook,Autor,Genre,NumberOfBooks From Books ", books_Dg);
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            connectClass.LoadTable("Select NameBook,Autor,Genre,NumberOfBooks From Books ", books_Dg);
        }

        private void addBook_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.addBook_Page() {user = this.user};
        }
    }
}
