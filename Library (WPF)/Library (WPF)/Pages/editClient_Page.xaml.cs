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
    /// Логика взаимодействия для editClient_Page.xaml
    /// </summary>
    public partial class editClient_Page : Page
    {
        public Windows.user_Window user = new Windows.user_Window();
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public int id;
        public editClient_Page()
        {
            InitializeComponent();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.clients_Page() { user = this.user };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connectClass.SqlConnect();

            SqlDataAdapter adapter = new SqlDataAdapter($"Select * From [ClientsProf] Where id = {id}", connectClass.sqlCon);
            DataTable table = new DataTable();
            adapter.Fill(table);

            nameClient_textBox.Text = Convert.ToString(table.Rows[0][1]);
            surnameClient_textBox.Text = Convert.ToString(table.Rows[0][2]);
            patronymicClient_textBox.Text = Convert.ToString(table.Rows[0][3]);
            birthdayClient_datePecker.Text = Convert.ToString(table.Rows[0][5]);
            genderClient_combobox.Text = Convert.ToString(table.Rows[0][4]);
            phoneClient_textbox.Text = Convert.ToString(table.Rows[0][6]);
            indexClient_textbox.Text = Convert.ToString(table.Rows[0][7]);
            addressClient_textbox.Text = Convert.ToString(table.Rows[0][9]);
        }

        private void editClient_button_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE [ClientsProf] SET NameClient = @NameClient,Surname = @Surname,Patronymic =@Patronymic" +
                    ",Gender = @Gender,NumberOfPhone = @NumberOfPhone,IndexClient = @IndexClient,Address = @Address,DateOfBirth =@DateOfBirth " +
                    "WHERE id = '" + id + "'", connectClass.sqlCon);

            command.Parameters.AddWithValue("NameClient", nameClient_textBox.Text);
            command.Parameters.AddWithValue("Surname", surnameClient_textBox.Text);
            command.Parameters.AddWithValue("Patronymic", patronymicClient_textBox.Text);
            command.Parameters.AddWithValue("Gender", genderClient_combobox.Text);
            command.Parameters.AddWithValue("DateOfBirth", birthdayClient_datePecker.Text);
            command.Parameters.AddWithValue("NumberOfPhone", phoneClient_textbox.Text);
            command.Parameters.AddWithValue("IndexClient", indexClient_textbox.Text);
            command.Parameters.AddWithValue("Address", addressClient_textbox.Text);

            command.ExecuteNonQuery();

            MessageBox.Show("Данные обновлены!", "Сообщение");
        }
    }
}
