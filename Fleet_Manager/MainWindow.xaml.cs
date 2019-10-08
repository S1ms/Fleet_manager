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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Fleet_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> OutputList;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
 
            SqlConnection conn = Yhteys.openConnection();
            string insertStatement = "Insert into AutoTable (merkki, malli, rekisterinumero, vuosimalli, katsastuspvm, koko, teho) values('" + MerkkiBox.Text + "' , '" + MalliBox.Text + "' , '" + RekisteriBox.Text + "' , '" + VuosimalliBox.Text + "' , '" + KatsastusBox.Text + "' , '" + KokoBox.Text + "' , '" + TehoBox.Text + "' )";

            SqlCommand insertCommand = new SqlCommand(insertStatement, conn);
            conn.Open();
            insertCommand.ExecuteNonQuery();
        }

        private void MuokkaaButton_Click(object sender, RoutedEventArgs e)
        {
            if(checkBoxMuokkaus.IsChecked == true)
            {
                SqlConnection conn = Yhteys.openConnection();
                string UpdateStatement = "UPDATE AutoTable SET Merkki=('" + MerkkiBox.Text + "'), Malli=('" + MalliBox.Text + "'), rekisterinumero=('" + RekisteriBox.Text + "'), vuosimalli=('" + VuosimalliBox.Text + "'), katsastuspvm=('" + KatsastusBox.Text + "'), koko=('" + KokoBox.Text + "'), teho=('" + TehoBox.Text + "') WHERE Id=('" + textBoxId.Text + "')";

                SqlCommand selectCommand = new SqlCommand(UpdateStatement, conn);
                conn.Open();
                selectCommand.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Muokkaustila ei ole päällä");
            }
        }

        private void HaeButton_Click(object sender, RoutedEventArgs e)
        {
            listViewInfo.Items.Clear();
            SqlDataReader dataReader;
            String Output = "";
            SqlConnection conn = Yhteys.openConnection();

            if (radioButtonKaikki.IsChecked == true && radioButtonMerkki.IsChecked == true)
            {
                string JarjestaStatement = "Select * from AutoTable Order by merkki";
                SqlCommand jarjestaCommand = new SqlCommand(JarjestaStatement, conn);
                conn.Open();
                jarjestaCommand.ExecuteNonQuery();  
                dataReader = jarjestaCommand.ExecuteReader();
            }

            else if (radioButtonKaikki.IsChecked == true && radioButtonMalli.IsChecked == true)
            {
                string JarjestaStatement = "Select * from AutoTable Order by malli";
                SqlCommand jarjestaCommand = new SqlCommand(JarjestaStatement, conn);
                conn.Open();
                jarjestaCommand.ExecuteNonQuery();
                dataReader = jarjestaCommand.ExecuteReader();
            }
            else if (radioButtonKaikki.IsChecked == true && radioButtonVanhin.IsChecked == true)
            {
                string JarjestaStatement = "Select * from AutoTable Order by vuosimalli ASC";
                SqlCommand jarjestaCommand = new SqlCommand(JarjestaStatement, conn);
                conn.Open();
                jarjestaCommand.ExecuteNonQuery();
                dataReader = jarjestaCommand.ExecuteReader();
            }
            else if (radioButtonKaikki.IsChecked == true && radioButtonUusin.IsChecked == true)
            {
                string JarjestaStatement = "Select * from AutoTable Order by vuosimalli DESC";
                SqlCommand jarjestaCommand = new SqlCommand(JarjestaStatement, conn);
                conn.Open();
                jarjestaCommand.ExecuteNonQuery();
                dataReader = jarjestaCommand.ExecuteReader();
            }
            else if (radioButtonKaikki.IsChecked == true)
            {
                string selectStatement = "Select * from AutoTable";
                SqlCommand selectCommand = new SqlCommand(selectStatement, conn);
                conn.Open();
                selectCommand.ExecuteNonQuery();
                dataReader = selectCommand.ExecuteReader();
            }
            else
            {
                string selectStatement = "Select * from AutoTable WHERE rekisterinumero =('" + RekisteriBox.Text + "')";
                SqlCommand selectCommand = new SqlCommand(selectStatement, conn);
                conn.Open();
                selectCommand.ExecuteNonQuery();
                dataReader = selectCommand.ExecuteReader();
            }

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + " - Merkki:" + dataReader.GetValue(1) + " - Malli:" + dataReader.GetValue(2) + " - Rekisterinumero:" + dataReader.GetValue(3) + " - Vuosimalli:" + dataReader.GetValue(4) + " - Katsastuspvm:" + dataReader.GetValue(5) + " - Koko:" + dataReader.GetValue(6) + " - Teho:" + dataReader.GetValue(7) + "\n";
            }
            MessageBox.Show(Output);

            OutputList = Output.Split('\n').ToList();

            foreach (string str in OutputList)
            {
                listViewInfo.Items.Add(str);
            }
        }

        private void ButtonPoista_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = Yhteys.openConnection();
            string PoistaStatement = "Delete FROM AutoTable WHERE id=('" + textBoxPoista.Text + "')";

            SqlCommand insertCommand = new SqlCommand(PoistaStatement, conn);
            conn.Open();
            insertCommand.ExecuteNonQuery();
            MessageBox.Show("Kohde poistettu");
        }

        private void CheckBoxMuokkaus_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBoxMuokkaus.IsChecked == true)
            {
                textBoxId.Visibility = Visibility.Visible;
                Label_Id.Visibility = Visibility.Visible;
            }
        }

        private void CheckBoxMuokkaus_Unchecked(object sender, RoutedEventArgs e)
        {
            if (checkBoxMuokkaus.IsChecked == false)
            {
                textBoxId.Visibility = Visibility.Hidden;
                Label_Id.Visibility = Visibility.Hidden;

            }
        }

        private void ButtonJarjesta_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = Yhteys.openConnection();

            if (radioButtonMerkki.IsChecked == true)
            {
                string JarjestaStatement = "Select * from AutoTable Order by merkki";
                SqlCommand jarjestaCommand = new SqlCommand(JarjestaStatement, conn);
                conn.Open();
                jarjestaCommand.ExecuteNonQuery();
            }
        }
    }
}
