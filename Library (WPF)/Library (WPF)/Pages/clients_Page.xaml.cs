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
    /// Логика взаимодействия для clients_Page.xaml
    /// </summary>
    public partial class clients_Page : Page
    {
        Classes.SqlConnectClass connectClass = new Classes.SqlConnectClass();
        public Windows.user_Window user = new Windows.user_Window();
        public clients_Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connectClass.LoadTable("Select NameClient,Surname,Patronymic From ClientsProf", clients_Dg);
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            connectClass.LoadTable("Select NameClient,Surname,Patronymic From ClientsProf", clients_Dg);
        }

        private void addClient_button_Click(object sender, RoutedEventArgs e)
        {
            user.userMainFrame.Content = new Pages.addClient_Page() { user = this.user };
        }

        private void descriptionClient_button_Click(object sender, RoutedEventArgs e)
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
            Classes.SqlConnectClass sqlConnect = new Classes.SqlConnectClass();
            sqlConnect.SqlConnect();

            SqlCommand command = new SqlCommand($"Select id FROM ClientsProf WHERE NameClient = N'{name}' AND Surname = N'{secondName}' AND Patronymic = N'{patronymic}'", sqlConnect.sqlCon);

             int id = (int)(command.ExecuteScalar());

            user.userMainFrame.Content = new Pages.editClient_Page() { id = id };
        }
    }
}
