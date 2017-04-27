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
using System.Windows.Shapes;
using System.Data.OleDb;


namespace ScreenDoctor
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class adduser : Window
    {
        OleDbConnection conexion;
        private String[] sexSelect = { "femenino", "masculino" };
        String newPath2 = (System.AppDomain.CurrentDomain.BaseDirectory.ToString()).Replace("\\", "/");
        //MessageBox.Show(newPath);
        public adduser()
        {
            InitializeComponent();
            
            conectar();
            foreach (string userVar in sexSelect)
            {
                addSex.Items.Add(userVar);
            }
        }

        public void conectar()
        {
            String ds = newPath2+"HospitalDB2v2.accdb";
            String stringConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + ds;
            conexion = new OleDbConnection(stringConexion);
            //MessageBox.Show("Conexion a Access(MS) realizada");
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            conexion.Open();
            addSex.Items.Clear();
            addSex.IsEnabled = true;
            foreach (string userVar in sexSelect)
            {
                addSex.Items.Add(userVar);
            }
            int age = int.Parse(addAge.Text);
            int dni = int.Parse(addDni.Text);
            Random randVar = new Random();
            int randNum = randVar.Next(1, 1000000000);


            string insertSQL = "INSERT INTO tblPaciente(Nombre, Apellido, Sexo, DNI, Edad, NumHistorial) VALUES(@nombre, @apellido, @sexo, @dni, @edad, @numHis)";
            OleDbCommand cmd = new OleDbCommand(insertSQL, conexion);
            cmd.Parameters.AddWithValue("@nombre", (String)addName.Text);
            cmd.Parameters.AddWithValue("@apellido", (String)addSurname.Text);
            cmd.Parameters.AddWithValue("@sexo", (String)addSex.Text);
            cmd.Parameters.AddWithValue("@dni", dni);
            cmd.Parameters.AddWithValue("@edad", age);
            cmd.Parameters.AddWithValue("@numHis", randNum);
            cmd.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Guardado con exito!");
            reset();
        }

        public void reset()
        {
            addName.Clear();
            addSurname.Clear();
            addSex.Items.Clear();
            foreach (string userVar in sexSelect)
            {
                addSex.Items.Add(userVar);
            }
            addDni.Clear();
            addAge.Clear();
        }
    }
}
