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
    /// Логика взаимодействия для adminMain_Page.xaml
    /// </summary>
    public partial class adminMain_Page : Page
    {
        public Windows.admin_Window admin;
        public adminMain_Page()
        {
            InitializeComponent();
        }
        private void backToAuthorization_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow authorization = new MainWindow();
            authorization.Show();
            admin.Close();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }

        private void addPerson_button_Click(object sender, RoutedEventArgs e)
        {
            admin.MainFrame.Content = new Pages.adminAddNewPerson_Page() {  admin = this.admin};
            
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }
        void LoadTable()
        {
            Classes.SqlConnectClass sqlConnect = new Classes.SqlConnectClass();
            sqlConnect.SqlConnect();
            sqlConnect.adapter = new SqlDataAdapter("Select Login,Password,TypeUser From UserTable", sqlConnect.sqlCon);
            sqlConnect.table.Clear();
            sqlConnect.adapter.Fill(sqlConnect.table);
            dataGridView_Users.ItemsSource = sqlConnect.table.DefaultView;
        }

        private void deletePerson_button_Click_2(object sender, RoutedEventArgs e)
        {
            int r = dataGridView_Users.SelectedIndex;

            string login = null;
            string password = null;

            for (int i = 0; i < 2;)
            {
                switch (i)
                {
                    case 0:
                        TextBlock itemL = dataGridView_Users.Columns[i].GetCellContent(dataGridView_Users.Items[r]) as TextBlock;
                         login = itemL.Text;
                        break;
                    case 1:
                        TextBlock itemP = dataGridView_Users.Columns[i].GetCellContent(dataGridView_Users.Items[r]) as TextBlock;
                         password = itemP.Text;
                        break;            
                }
                i++;           
            }

            Classes.SqlConnectClass sqlConnect = new Classes.SqlConnectClass();
            sqlConnect.SqlConnect();

            SqlCommand command = new SqlCommand($"DELETE FROM UserTable WHERE Login = '{login}' AND Password = '{password}'", sqlConnect.sqlCon);
            command.ExecuteNonQuery();

            MessageBox.Show("Пользователь удалён!");
        }
    }
}
