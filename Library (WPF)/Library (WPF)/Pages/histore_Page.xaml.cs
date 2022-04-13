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
    /// Логика взаимодействия для histore_Page.xaml
    /// </summary>
    public partial class histore_Page : Page
    {
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Windows.user_Window user = new Windows.user_Window();
        public histore_Page()
        {
            InitializeComponent();

            connectClass.SqlConnect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT HistoryAction.DateAction,HistoryAction.NameBook,HistoryAction.Autor,ClientsProf.NameClient +' '+ ClientsProf.Surname +' '+ ClientsProf.Patronymic [Client],HistoryAction.Status " +
             "FROM HistoryAction INNER JOIN ClientsProf ON ClientsProf.id = HistoryAction.Client", connectClass.sqlCon);
            connectClass.table.Clear();
            adapter.Fill(connectClass.table);
            historyActions_Dg.ItemsSource = connectClass.table.DefaultView;
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            connectClass.SqlConnect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT HistoryAction.DateAction,HistoryAction.NameBook,HistoryAction.Autor,ClientsProf.NameClient +' '+ ClientsProf.Surname +' '+ ClientsProf.Patronymic [Client],HistoryAction.Status " +
             "FROM HistoryAction INNER JOIN ClientsProf ON ClientsProf.id = HistoryAction.Client", connectClass.sqlCon);
            connectClass.table.Clear();
            adapter.Fill(connectClass.table);
            historyActions_Dg.ItemsSource = connectClass.table.DefaultView;
        }
    }
}
