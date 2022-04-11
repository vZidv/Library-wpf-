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
using System.Data.SqlClient;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Classes.SqlConnectClass sqlConnect = new Classes.SqlConnectClass();
            sqlConnect.SqlConnect();
            sqlConnect.adapter = new SqlDataAdapter("Select Login,Password,TypeUser From UserTable", sqlConnect.sqlCon);
            sqlConnect.adapter.Fill(sqlConnect.table);
            dataGridView_Users.ItemsSource = sqlConnect.table.DefaultView;
        }
    }
}
