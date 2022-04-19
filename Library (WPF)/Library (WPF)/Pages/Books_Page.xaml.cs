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
    /// Логика взаимодействия для Books_Page.xaml
    /// </summary>
    public partial class Books_Page : Page
    {
        public Windows.user_Window user = new Windows.user_Window();
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Books_Page()
        {
            InitializeComponent();
            connectClass.LoadTable("Select NameBook,Autor,Genre,NumberOfBooks From Books ", books_Dg);
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            connectClass.LoadTable("Select NameBook,Autor,Genre,NumberOfBooks From Books ", books_Dg);
        }

        private void addBook_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.addBook_Page() {user = this.user};
        }

        private void deleteBooks_button_Click(object sender, RoutedEventArgs e)
        {
            int r = books_Dg.SelectedIndex;

            string author = null;
            string nameBook = null;

            for (int i = 0; i < 2;)
            {
                switch (i)
                {
                    case 0:
                        TextBlock itemL = books_Dg.Columns[i].GetCellContent(books_Dg.Items[r]) as TextBlock;
                        nameBook = itemL.Text;
                        break;
                    case 1:
                        TextBlock itemP = books_Dg.Columns[i].GetCellContent(books_Dg.Items[r]) as TextBlock;
                        author = itemP.Text;
                        break;
                }
                i++;
            }
            MessageBox.Show($"{nameBook} {author}");

            connectClass.SqlConnect();

            SqlCommand command = new SqlCommand($"DELETE FROM [Books] WHERE NameBook = N'{nameBook}' AND Autor = N'{author}'", connectClass.sqlCon);
            command.ExecuteNonQuery();

            MessageBox.Show("Книга удалёна!");
        }

        private void search_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (books_Dg.ItemsSource as DataTable).DefaultView.RowFilter = $"[NameBook] LIKE '%{search_textbox.Text}%'";
        }
    }
}
