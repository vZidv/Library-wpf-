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
    /// Логика взаимодействия для decriptionOverdueBook_Page.xaml
    /// </summary>
    public partial class decriptionOverdueBook_Page : Page
    {
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Windows.user_Window user = new Windows.user_Window();

        public int idOverdue;
        private int idClient;
        public decriptionOverdueBook_Page()
        {
            InitializeComponent();


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connectClass.SqlConnect();
            SqlDataAdapter adapterForIssuedBooks = new SqlDataAdapter($"Select OverdueBooks.LastDate,ClientsProf.NameClient,ClientsProf.Surname,ClientsProf.Patronymic,OverdueBooks.NumberOfPhone,OverdueBooks.BookName,OverdueBooks.Autor,ClientsProf.id " +
                $"From OverdueBooks INNER JOIN ClientsProf ON ClientsProf.id = OverdueBooks.Client WHERE OverdueBooks.id = '{idOverdue}'", connectClass.sqlCon);
            DataTable table = new DataTable();
            adapterForIssuedBooks.Fill(table);
            lastDate_datePecker.Text = Convert.ToString(table.Rows[0][0]);

            nameBook_textbox.Text = Convert.ToString(table.Rows[0][5]);
            authorBook_textbox.Text = Convert.ToString(table.Rows[0][6]);

            idClient = Convert.ToInt32(table.Rows[0][7]);

            nameClient_textBox.Text = Convert.ToString(table.Rows[0][1]);
            secondNameClient_textBox.Text = Convert.ToString(table.Rows[0][2]);
            patronymicClient_textBox.Text = Convert.ToString(table.Rows[0][3]);
            phoneNumberClient_textBox.Text = Convert.ToString(table.Rows[0][4]);

            var date = (DateTime.Now - Convert.ToDateTime( lastDate_datePecker.Text)).Days;
            daysOverdueBook_textbox.Text =Convert.ToString( date);

            SqlCommand sqlCommand = new SqlCommand($"Select Penalty from SettingLibrary Where id ='{1}'", connectClass.sqlCon);

            penalty_textbox.Text = Convert.ToString(Convert.ToInt32(daysOverdueBook_textbox.Text) * Convert.ToInt32(sqlCommand.ExecuteScalar()));
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.overdueBooks_Page() { user = this.user };
        }

        private void pay_button_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new SqlCommand($"DELETE FROM OverdueBooks WHERE id ='{idOverdue}'", connectClass.sqlCon);
            command.ExecuteNonQuery();
            connectClass.AddInHistory(nameBook_textbox.Text, authorBook_textbox.Text, idClient, "Оплачен");
            MessageBox.Show("Штраф оплачен!","Сообщение");

            user.userMainFrame.Content = new Pages.overdueBooks_Page() { user = this.user };
        }
    }
}
