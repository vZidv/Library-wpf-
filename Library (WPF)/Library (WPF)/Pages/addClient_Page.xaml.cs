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
    /// Логика взаимодействия для addClient_Page.xaml
    /// </summary>
    public partial class addClient_Page : Page
    {
        public Windows.user_Window user = new Windows.user_Window();
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();


        public addClient_Page()
        {
            InitializeComponent();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.clients_Page() { user = this.user };
        }

        private void addClient_button_Click(object sender, RoutedEventArgs e)
        {
            connectClass.SqlConnect();
            SqlCommand com = new SqlCommand("INSERT INTO [ClientsProf] (NameClient,Surname,Patronymic,Gender,NumberOfPhone,IndexClient,Address,DateOfBirth) " +
                    " VALUES (@NameClient,@Surname,@Patronymic,@Gender,@NumberOfPhone,@IndexClient,@Address,@DateOfBirth)", connectClass.sqlCon);


            com.Parameters.AddWithValue("NameClient", nameClient_textBox.Text);
            com.Parameters.AddWithValue("Surname", surnameClient_textBox.Text);
            com.Parameters.AddWithValue("Patronymic", patronymicClient_textBox.Text);
            com.Parameters.AddWithValue("Gender", genderClient_combobox.Text);
            com.Parameters.AddWithValue("DateOfBirth", birthdayClient_datePecker.Text);
            com.Parameters.AddWithValue("NumberOfPhone", phoneClient_textbox.Text);
            com.Parameters.AddWithValue("IndexClient", indexClient_textbox.Text);
            com.Parameters.AddWithValue("Address", Convert.ToString( addressClient_textbox.Text));

            com.ExecuteNonQuery();

            MessageBox.Show("Клиент добавлен!", "Сообщение");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
