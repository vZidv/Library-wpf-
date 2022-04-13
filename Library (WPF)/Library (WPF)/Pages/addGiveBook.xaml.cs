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
    /// Логика взаимодействия для addGiveBook.xaml
    /// </summary>
    public partial class addGiveBook : Page
    {
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Windows.user_Window user = new Windows.user_Window();

        private int idClient;

        DataTable tableBooks = new DataTable();
        public addGiveBook()
        {
            InitializeComponent();

           // connectClass.LoadTable("Select NameBook,Autor,Genre,NumberOfBooks From Books ", books_Dg);
            connectClass.LoadTable("Select NameClient,Surname,Patronymic From ClientsProf", clients_Dg);

            connectClass.SqlConnect();
            SqlDataAdapter adapter = new SqlDataAdapter("Select NameBook,Autor,Genre,NumberOfBooks From Books ", connectClass.sqlCon);
            DataTable table = new DataTable();
            adapter.Fill(table);
            books_Dg.ItemsSource = table.DefaultView;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.giveBook_Page() { user = this.user };
        }

        private void chooseClient_button_Click(object sender, RoutedEventArgs e)
        {
            int r = clients_Dg.SelectedIndex;

            string name = null;
            string secondName = null;
            string patronymic = null;

            for (int i = 0; i < 4;)
            {
                switch (i)
                {
                    case 0:
                        TextBlock itemL = clients_Dg.Columns[i].GetCellContent(clients_Dg.Items[r]) as TextBlock;
                        name = itemL.Text;
                        break;
                    case 1:
                        TextBlock itemP = clients_Dg.Columns[i].GetCellContent(clients_Dg.Items[r]) as TextBlock;
                        secondName = itemP.Text;
                        break;
                    case 2:
                        TextBlock itemG = clients_Dg.Columns[i].GetCellContent(clients_Dg.Items[r]) as TextBlock;
                        patronymic = itemG.Text;
                        break;

                }
                i++;
            }
            connectClass.SqlConnect();
            SqlCommand command = new SqlCommand($"Select id From ClientsProf WHERE NameClient = N'{name}' AND Surname = N'{secondName}' AND Patronymic = N'{patronymic}'", connectClass.sqlCon);
            idClient = (int)command.ExecuteScalar();
            fullNameClient_textBox.Text = name + " " + secondName + " " + patronymic;

            SqlDataAdapter adapter1 = new SqlDataAdapter($"SELECT * FROM OverdueBooks WHERE Client = N'{idClient}'", connectClass.sqlCon);
            DataTable overdue = new DataTable();
            adapter1.Fill(overdue);
            overdueBook_textBox.Text = Convert.ToString(overdue.Rows.Count);
        }

        private void chooseBook_button_Click(object sender, RoutedEventArgs e)
        {
            int r = books_Dg.SelectedIndex;

            string nameBook = null;
            string author = null;

            for (int i = 0; i < 3;)
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


            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT NameBook,Autor FROM Books WHERE NameBook = N'{nameBook}' AND Autor = N'{author}'", connectClass.sqlCon);

            adapter.Fill(tableBooks);
            giveBooks_Dg.ItemsSource = tableBooks.DefaultView;
        }

        private void addGiveBook_button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            date.ToString("dd MMMM yyyy");
            DateTime LastDat = date + new TimeSpan(days: 30, hours: 0, minutes: 0, seconds: 0);
            LastDat.ToString("dd MMMM yyyy");
            connectClass.SqlConnect();

            for (int i = 0; i < giveBooks_Dg.Items.Count; i++)
            {
                SqlCommand command = new SqlCommand("INSERT INTO [IssuedBooks] (GiveData,LastDate,NameBook,Autor,Client) " +
                "VALUES (@GiveData,@LastDate,@NameBook,@Autor,@Client)", connectClass.sqlCon);

                string nameBook = null;
                string author = null;

                for (int j = 0; j < giveBooks_Dg.Columns.Count;)
                {
                    switch (j)
                    {
                        case 0:
                            TextBlock itemL = books_Dg.Columns[j].GetCellContent(books_Dg.Items[i]) as TextBlock;
                            nameBook = itemL.Text;
                            break;
                        case 1:
                            TextBlock itemP = books_Dg.Columns[j].GetCellContent(books_Dg.Items[i]) as TextBlock;
                            author = itemP.Text;
                            break;

                    }
                    j++;
                }


                command.Parameters.AddWithValue("GiveData", date);
                command.Parameters.AddWithValue("LastDate", LastDat);
                command.Parameters.AddWithValue("NameBook", nameBook);
                command.Parameters.AddWithValue("Autor", author);
                command.Parameters.AddWithValue("Client", idClient);
                command.ExecuteNonQuery();

                connectClass.AddInHistory(nameBook,author, idClient, "Выдана");
            }
            MessageBox.Show("Книги выданы!", "Сообщение");

        }

        private void deleteBook_button_Click(object sender, RoutedEventArgs e)
        {
            int r = giveBooks_Dg.SelectedIndex;
            tableBooks.Rows[r].Delete();
            giveBooks_Dg.ItemsSource = tableBooks.DefaultView;
        }
    }
}
