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
using System.Data.OleDb;

namespace ScreenDoctor
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OleDbConnection conexion;
        public static String selectedPatient  ;
        String SelectedName ;
        int myTrue = 1;
        int myFalse = 0;
        
        String[] result = { "(sin datos)", "(sin datos)", "(sin datos)", "(sin datos)", "(sin datos)", "(sin datos)", "(sin datos)" };

        

        public MainWindow()
        {
            InitializeComponent();
            String newPath = (System.AppDomain.CurrentDomain.BaseDirectory.ToString()).Replace("\\", "/");
            //MessageBox.Show(newPath);
            conectar();
        }

        public void conectar()
        {
            String ds =System.AppDomain.CurrentDomain.BaseDirectory.ToString()+"HospitalDB2v2.accdb";
            String stringConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + ds;
            conexion = new OleDbConnection(stringConexion);
            MessageBox.Show("Conexion a (MS)Access realizada");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Historial myHist = new Historial(selectedPatient, SelectedName, result, myFalse);
            
            myHist.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string inputPacient = IDPaciente.Text;
           
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            conexion.Open();
            
            selectedPatient = IDPaciente.Text;

            string qryID = "SELECT * FROM tblPaciente WHERE IDPaciente =@inputPacient";
            OleDbCommand cmd = new OleDbCommand(qryID, conexion);
            cmd.Parameters.AddWithValue("@inputPacient", IDPaciente.Text  );

            OleDbDataReader reader = cmd.ExecuteReader();

          
                if (!(reader.Read()))
                {
                    MessageBox.Show("Usuario no existe/Campo en blanco");
                }
                else
                {
                    NameTab.Text = (String)reader["Nombre"] + " " + (String)reader["Apellido"];
                    
                    SelectedName = (String)reader["Nombre"] + " " + (String)reader["Apellido"];
                    dniEdad.Text = String.Empty + reader["DNI"] + "  (letra omitida)";
                    result[0] = String.Empty + reader["numHistorial"];
                    result[1] = String.Empty + reader["Ingresado"];
                    result[2] = String.Empty + reader["CitasPruebas"];
                    result[3] = String.Empty + reader["Dieta"];
                    result[4] = String.Empty + reader["Talla"];
                    result[5] = String.Empty + reader["Observaciones"];
                    result[6] = String.Empty + reader["DNI"];
            }
            
            //MessageBox.Show(selectedPatient);
            
            conexion.Close();
            //IDPaciente.Clear();
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            adduser newUser = new adduser();
            newUser.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            conexion.Open();
            string myUpdate = "UPDATE tblPaciente SET Ingresado='no' WHERE IDPaciente =@idPaciente";
            OleDbCommand myCmd = new OleDbCommand(myUpdate, conexion);
            myCmd.Parameters.AddWithValue("@idPaciente", selectedPatient);
            try
            {
                myCmd.ExecuteNonQuery();
                
            }
            catch(Exception)
            {
                MessageBox.Show("Algo ocurrió");
               
            }

            conexion.Close();
            if (IDPaciente.Text != String.Empty)
            {
                MessageBox.Show("Alta exitosa");
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            conexion.Open();
            string myUpdate = "UPDATE tblPaciente SET Ingresado='si' WHERE IDPaciente =@idPaciente";
            OleDbCommand myCmd = new OleDbCommand(myUpdate, conexion);
            myCmd.Parameters.AddWithValue("@idPaciente", selectedPatient);
            try
            {
                myCmd.ExecuteNonQuery();
            }
            catch(Exception)
            {
                MessageBox.Show("Algo ocurrió");
            }
            conexion.Close();
            if (IDPaciente.Text != String.Empty)
            {
                MessageBox.Show("Ingreso exitoso");
            } 
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            CitasNuevas newAppoint = new CitasNuevas(IDPaciente.Text);
            newAppoint.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            View myView = new View(IDPaciente.Text,SelectedName );
            myView.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Historial editView = new Historial(selectedPatient, SelectedName, result, myTrue);
            editView.Show();

            
        }
    }
}
