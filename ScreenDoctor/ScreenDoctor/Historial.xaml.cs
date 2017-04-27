using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
using System.Windows.Shapes;

namespace ScreenDoctor
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Historial : Window
    {
        public string user;
        public string userId = MainWindow.selectedPatient;
        public int isEditable;
        OleDbConnection conexion;
        string ansHosp;
        string[] mySize = { "S", "L", "M", "XS", "XL" };
        string[] myDiet = { "Hipopro", "Hyperpro", "Celiaco", "Alergico" };

        public void conectar()
        {
            String ds = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "HospitalDB2v2.accdb";
            String stringConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + ds;
            conexion = new OleDbConnection(stringConexion);
            //MessageBox.Show("Conexion a (MS)Access realizada");
        }

        public Historial(string userId, string user, String[] myData, int editable)
        {
            isEditable = editable;
           
            if (isEditable==0)
            {
                InitializeComponent();
                //userName.Text = user;
                

                userNum.Text = userId;
                userName.Text = user;
                medHistNum.Text = myData[0];
                idPaciente.Text = myData[6];
                if (myData[1] == "si")
                {
                    isHosp.IsChecked = true;
                }
                else if (myData[1] == "no")
                {
                    isHosp.IsChecked = false;
                }
                appoint.Text = myData[2];
                diet.Text = myData[3];
                size.Text = myData[4];
                obs.Text = myData[5];
                appoint.IsEnabled = false;
                diet.IsEnabled = false;
                size.IsEnabled = false;
                obs.IsEnabled = false;
            }
            else if(isEditable==1)
            {
                InitializeComponent();
                cbxDiet.Visibility = System.Windows.Visibility.Visible;
                cbxSize.Visibility = System.Windows.Visibility.Visible;
                foreach (string userVar in mySize)
                {
                    cbxSize.Items.Add(userVar);
                }
                foreach (string userVar2 in myDiet)
                {
                    cbxDiet.Items.Add(userVar2);
                }
                diet.IsEnabled = false;
                size.IsEnabled = false;
                conectar();
                isHosp.IsEnabled = true;
                userNum.Text = userId;
                userName.Text = user;
                medHistNum.Text = myData[0];
                idPaciente.Text = myData[6];
                if (myData[1] == "si")
                {
                    isHosp.IsChecked = true;
                }
                else if (myData[1] == "no")
                {
                    isHosp.IsChecked = false;
                }
                appoint.Text = myData[2];
                diet.Text = myData[3];
                size.Text = myData[4];
                obs.Text = myData[5];
                

            }
            
            

        }

        public void accept_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(userId);
            if(isEditable==1)
            {
                if (isHosp.IsChecked == true)
                {
                    ansHosp = "si";
                }
                else if (isHosp.IsChecked == false)
                {
                    ansHosp = "no";
                }
                conexion.Open();
                string updateSql = "UPDATE tblPaciente SET Ingresado=@newIng, CitasPruebas=@newCit, Observaciones=@newObs, Dieta=@newDie, Talla=@newTal WHERE IDPaciente=@id";
                //MessageBox.Show(userId);
                OleDbCommand cmd = new OleDbCommand(updateSql, conexion);

                cmd.Parameters.AddWithValue("@newIng", ansHosp);
                cmd.Parameters.AddWithValue("@newCit", appoint.Text);
                cmd.Parameters.AddWithValue("@newObs", obs.Text);
                cmd.Parameters.AddWithValue("@newDie", cbxDiet.Text);
                cmd.Parameters.AddWithValue("@newTal", cbxSize.Text);
                cmd.Parameters.AddWithValue("@id", userId);

                cmd.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Actualizado con exito!");
                Close();
            }
            else if (isEditable == 0)
            {

                Close();
            }
            
        }
    }
}
