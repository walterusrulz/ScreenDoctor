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
    /// Lógica de interacción para CitasNuevas.xaml
    /// </summary>
    public partial class CitasNuevas : Window
    {
        String[] appType =  {"Selecciona:", "Control", "Revision", "Pruebas", "Especialista" };//0.1.2.3.4
        OleDbConnection conexion;
        public string selectedID;
        string preference;
        String newPath3 = (System.AppDomain.CurrentDomain.BaseDirectory.ToString()).Replace("\\", "/");
        //MessageBox.Show(newPath);
        public CitasNuevas(string id)
        {
            selectedID = id;
            InitializeComponent();
            conectar();
            foreach (string userVar in appType)
            {
                app.Items.Add(userVar);
            }
            date.SelectedDate = DateTime.Now;

        }

        public void conectar()
        {
            String ds =newPath3+"HospitalDB2v2.accdb";
            String stringConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + ds;
            conexion = new OleDbConnection(stringConexion);
            //MessageBox.Show("Conexion a Access(MS) realizada");
        }

        private void acc_Click(object sender, RoutedEventArgs e)
        {

            if(selectedID != String.Empty)
            {
                if (rbtUrg.IsChecked == true)
                {
                    preference = "URGENTE";
                }
                else if (rbtPre.IsChecked == true)
                {
                    preference = "PREFERENTE";
                }
                conexion.Open();
                string insertApp = "INSERT INTO tblCita(IDPaciente, FechaCita, TipoCita) VALUES (@id, @fecha, @tipo)";
                OleDbCommand comm = new OleDbCommand(insertApp, conexion);
                comm.Parameters.AddWithValue("@id", selectedID);
                comm.Parameters.AddWithValue("@fecha", date.SelectedDate);
                comm.Parameters.AddWithValue("@tipo", preference + app.Text);

                try
                {
                    comm.ExecuteNonQuery();
                }
                catch(Exception)
                {
                    MessageBox.Show("Excepcion de SQL!");
                }

                conexion.Close();
                MessageBox.Show("Cita correctamente guardada!");
               
            }
            else
            {
                MessageBox.Show("Algo ocurrió.");
            }
            
            
        }
        public void reset()
        {
            
            app.SelectedItem = appType[0];
            date.SelectedDate = DateTime.Now;
            rbtPre.IsChecked = true;
        }

        private void clean_Click(object sender, RoutedEventArgs e)
        {

            reset();
        }
    }

}
