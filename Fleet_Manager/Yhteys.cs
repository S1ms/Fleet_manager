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

namespace Fleet_Manager
{
    class Yhteys
    {
        public static SqlConnection openConnection()
        {

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Simo\source\repos\Fleet_Manager\Fleet_Manager\AutoDatabase.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(connectionString.ToString());
            return conn;
        }
    }
}
