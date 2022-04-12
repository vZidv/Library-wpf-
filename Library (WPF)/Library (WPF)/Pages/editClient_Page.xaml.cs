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
    /// Логика взаимодействия для editClient_Page.xaml
    /// </summary>
    public partial class editClient_Page : Page
    {
        public Windows.user_Window user = new Windows.user_Window();
        public editClient_Page()
        {
            InitializeComponent();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.clients_Page() { user = this.user };
        }
    }
}
