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
using System.Data;
using System.Data.SqlClient;

namespace Library__WPF_.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        Classes.SqlConnectClass sqlConnect = new Classes.SqlConnectClass();
        DataTable table = new DataTable();

        public Authorization()
        {
            InitializeComponent();
            sqlConnect.SqlConnect();
        }

        private void authorization_Button_Click(object sender, RoutedEventArgs e)
        {

            if (login_Textbox.Text == String.Empty || password_Textbox.Text == String.Empty)
            {
                MessageBox.Show("Одна из строчек пуста!", "Ошибка");
            }
            else
            {
                table.Clear();

                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM UserTable WHERE Login = '{login_Textbox.Text}' AND Password = '{password_Textbox.Text}'", sqlConnect.sqlCon);
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    if (table.Rows[0][3].ToString() == "admin")
                    {
                        MessageBox.Show("Вы админ!", "Победа");
                    }
                    else if (table.Rows[0][3].ToString() == "user")
                    {
                        MessageBox.Show("Вы халоп!", "Проигрышь");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный ввод данных!", "Ошибка");
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnect.SqlConnect();
        }
    }
}
