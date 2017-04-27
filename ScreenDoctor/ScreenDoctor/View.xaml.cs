using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {
        OleDbConnection conexion;
        string id;
        string name;
        String newPath3 = (System.AppDomain.CurrentDomain.BaseDirectory.ToString()).Replace("\\", "/");
       

        public View(string idValue, string textIn)
        {
            
            InitializeComponent();
            id = idValue;
            name = textIn;
            conectar();
            conexion.Open();
            string viewMe = "SELECT * FROM tblCita WHERE IDPaciente = @thisID";
            OleDbCommand cmd = new OleDbCommand(viewMe, conexion);
            cmd.Parameters.AddWithValue("@thisID", id);
            OleDbDataAdapter read = new OleDbDataAdapter(cmd);



            DataTable dataReg = new DataTable("tblCita");
            read.Fill(dataReg);
            appData.ItemsSource = dataReg.DefaultView;
            read.Update(dataReg);
            pId.Text = name;
            conexion.Close();
        }

        public void conectar()
        {
            String ds = newPath3+"HospitalDB2v2.accdb";
            String stringConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + ds;
            conexion = new OleDbConnection(stringConexion);
           
            //MessageBox.Show("Conexion a (MS)Access realizada");
        }
    }
}
