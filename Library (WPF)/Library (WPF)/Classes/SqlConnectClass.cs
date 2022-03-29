using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Library__WPF_.Classes
{
    class SqlConnectClass
    {
        public SqlConnection sqlCon;
        public SqlDataAdapter adapter;

        public void SqlConnect()
        {
            sqlCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = F:\Артём\Проекты и их материалы\Library(Wpf)\Project\Library-wpf-\Library (WPF)\Library (WPF)\Library.mdf; Integrated Security = True");
            sqlCon.Open();
        }
    }
}
