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
    /// Логика взаимодействия для admin_Window.xaml
    /// </summary>
    public partial class admin_Window : Window
    {
        public admin_Window()
        {
            InitializeComponent();
        }

        private void backToAuthorization_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow authorization = new MainWindow();
            authorization.Show();
            this.Close();
        }
    }
}
