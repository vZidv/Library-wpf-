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
    /// Логика взаимодействия для adminAddNewPerson_Page.xaml
    /// </summary>
    public partial class adminAddNewPerson_Page : Page
    {
        public Windows.admin_Window admin;
        Classes.SqlConnectClass sqlConnect = new Classes.SqlConnectClass();
        public adminAddNewPerson_Page()
        {
            InitializeComponent();
        }

        private void backToMainMenu_button_Click(object sender, RoutedEventArgs e)
        {
            admin.MainFrame.Content = new Pages.adminMain_Page{admin = this.admin};
        }

        private void adminAddNewPerson_button_Click(object sender, RoutedEventArgs e)
        {
            if (login_textbox.Text == String.Empty || login_textbox.Text == String.Empty)
            {
                MessageBox.Show("Одна из строчек пуста!", "Ошибка");
            }
            else
            {
                sqlConnect.SqlConnect();

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO UserTable (Login,Password,TypeUser) VALUES (@Login,@Password,@TypeUser)", sqlConnect.sqlCon);

                sqlCommand.Parameters.AddWithValue("@Login", login_textbox.Text);
                sqlCommand.Parameters.AddWithValue("@Password", Password_textbox.Text);
                sqlCommand.Parameters.AddWithValue("@TypeUser", TypeUser_comboBox.Text);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Пользователь добавлен!", "Сообщение");
            }
        }
    }
}
