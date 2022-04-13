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
    /// Логика взаимодействия для discriptionGriveBook.xaml
    /// </summary>
    public partial class discriptionGriveBook : Page
    {
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Windows.user_Window user = new Windows.user_Window();

        public int idClient;

        public discriptionGriveBook()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connectClass.SqlConnect();

            SqlDataAdapter adapterForIssuedBooks = new SqlDataAdapter($"SELECT IssuedBooks.GiveData,IssuedBooks.LastDate,IssuedBooks.NameBook,IssuedBooks.Autor,IssuedBooks.Client,ClientsProf.NameClient,ClientsProf.Surname,ClientsProf.Patronymic,ClientsProf.NumberOfPhone FROM IssuedBooks " +
                $"INNER JOIN ClientsProf ON ClientsProf.id = '{idClient}'", connectClass.sqlCon);
            DataTable table = new DataTable();
            adapterForIssuedBooks.Fill(table);
            giveDate_datePecker.Text = Convert.ToString(table.Rows[0][0]);
            lastDate_datePecker.Text = Convert.ToString(table.Rows[0][1]);

            nameBook_textbox.Text = Convert.ToString(table.Rows[0][2]);
            authorBook_textbox.Text = Convert.ToString(table.Rows[0][3]);
            nameClient_textBox.Text = Convert.ToString(table.Rows[0][5]);
            secondNameClient_textBox.Text = Convert.ToString(table.Rows[0][6]);
            patronymicClient_textBox.Text = Convert.ToString(table.Rows[0][7]);
            phoneNumberClient_textBox.Text = Convert.ToString(table.Rows[0][8]);

        }

        private void returnBook_button_Click(object sender, RoutedEventArgs e)
        {
            // sqlConnectClass.AddInHistory(textBox_nameBook, textBox_autor, idClient, "Возврат");
            connectClass.SqlConnect();

            SqlCommand command = new SqlCommand($"DELETE FROM IssuedBooks WHERE Client = '{idClient}' and NameBook = N'{nameBook_textbox.Text}'and Autor = N'{authorBook_textbox.Text}'", connectClass.sqlCon);
            command.ExecuteNonQuery();

            user.userMainFrame.Content = new Pages.giveBook_Page() { user = this.user };
        }
    }
}
