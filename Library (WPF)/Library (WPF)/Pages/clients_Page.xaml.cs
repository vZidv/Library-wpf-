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
    /// Логика взаимодействия для clients_Page.xaml
    /// </summary>
    public partial class clients_Page : Page
    {
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Windows.user_Window user = new Windows.user_Window();
        public clients_Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connectClass.LoadTable("Select NameClient,Surname,Patronymic From ClientsProf", clients_Dg);
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            connectClass.LoadTable("Select NameClient,Surname,Patronymic From ClientsProf", clients_Dg);
        }

        private void addClient_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.addClient_Page() { user = this.user };
        }
    }
}
