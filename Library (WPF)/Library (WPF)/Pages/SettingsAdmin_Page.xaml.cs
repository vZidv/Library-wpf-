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
using System.Data;

namespace Library__WPF_.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsAdmin_Page.xaml
    /// </summary>
    public partial class SettingsAdmin_Page : Page
    {
        public Windows.admin_Window admin;
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public SettingsAdmin_Page()
        {
            InitializeComponent();
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            admin.MainFrame.Content = new Pages.adminMain_Page() { admin = this.admin };
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            connectClass.SqlConnect();
            SqlCommand command = new SqlCommand($"UPDATE SettingLibrary " +
                $"SET LastDays ='{Convert.ToInt32(lastDate_textbox.Text)}',Penalty ='{Convert.ToInt32(penalty_textbox.Text)}'",connectClass.sqlCon);
            command.ExecuteNonQuery();
            MessageBox.Show("Данные сохранены!");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connectClass.SqlConnect();
            DataTable table = connectClass.table;

            SqlDataAdapter adapter = new SqlDataAdapter("Select LastDays,Penalty From SettingLibrary where id = '1'",connectClass.sqlCon);
            adapter.Fill(table);
            lastDate_textbox.Text = table.Rows[0][0].ToString();
            penalty_textbox.Text = table.Rows[0][1].ToString();
        }
    }
}
