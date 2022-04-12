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
    /// Логика взаимодействия для addBook_Page.xaml
    /// </summary>
    public partial class addBook_Page : Page
    {
        public Windows.user_Window user = new Windows.user_Window();
        public addBook_Page()
        {
            InitializeComponent();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.Books_Page() {user = this.user};
        }

        private void addBook_button_Click(object sender, RoutedEventArgs e)
        {
            Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();

            SqlCommand command = new SqlCommand("INSERT INTO Books (NameBook,Autor,Genre,NumberOfBooks) VALUES (@NameBook,@Autor,@Genre,@NumberOfBooks)", connectClass.sqlCon);

            command.Parameters.AddWithValue("NameBook", nameBook_textBox.Text);
            command.Parameters.AddWithValue("Autor", authorBook_textBox.Text);
            command.Parameters.AddWithValue("Genre", genreBook_combobox.Text);
            command.Parameters.AddWithValue("NumberOfBooks", amountBook_textBox.Text);

            command.ExecuteNonQuery();

            MessageBox.Show("Книга добавлена!");
        }
    }
}
