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
using System.Windows.Shapes;

namespace Library__WPF_.Windows
{
    /// <summary>
    /// Логика взаимодействия для user_Window.xaml
    /// </summary>
    public partial class user_Window : Window
    {
        public user_Window()
        {
            InitializeComponent();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userMainFrame.Content = new Pages.Books_Page() { user = this };
        }

        private void userMainFrame_FragmentNavigation(object sender, System.Windows.Navigation.FragmentNavigationEventArgs e)
        {

        }

        private void clients_Menu_Click(object sender, RoutedEventArgs e)
        {
            userMainFrame.Content = new Pages.clients_Page() {user = this};
        }

        private void book_Menu_Click(object sender, RoutedEventArgs e)
        {
            userMainFrame.Content = new Pages.Books_Page() { user = this};
        }

        private void usedBooks_Menu_Click(object sender, RoutedEventArgs e)
        {
            userMainFrame.Content = new Pages.giveBook_Page() { user = this };
        }

        private void history_Menu_Click(object sender, RoutedEventArgs e)
        {
            userMainFrame.Content = new Pages.histore_Page() { user = this };
        }
    }
}
