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
using System.Data;
using System.Data.SqlClient;


namespace U9
{
    /// <summary>
    /// Lógica de interacción para Window3.xaml
    /// </summary>
    public partial class Alumno : Window
    {

        public int FilaActual { get; set; } = 0;
        public int totalFilas { get; set; }
        DataSet dsAlumnos = new DataSet();
        DataRow drFilaActual;
        SqlDataAdapter DataAdapter;

        public List<string> Alumnos { get; set; } = new List<string>();
        public Alumno()
        {
            InitializeComponent();

            string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Instituto.mdf;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();


            DataAdapter = new SqlDataAdapter("SELECT * FROM Alumnos ", conexion);

            dsAlumnos = new DataSet();
            DataAdapter.Fill(dsAlumnos, "Alumnos");
            totalFilas = dsAlumnos.Tables["Alumnos"].Rows.Count;

            MostrarDatos();
            conexion.Close();
        }

        public void MostrarDatos()
        {
            if (totalFilas > 0)
            {
                drFilaActual = dsAlumnos.Tables["Alumnos"].Rows[FilaActual];
                tbDni.Text = drFilaActual["dni"].ToString();
                nombre.Text = drFilaActual["nombre"].ToString();
                Apellidos.Text = drFilaActual["apellido"].ToString();
                telefono.Text = drFilaActual["tlf"].ToString();
                email.Text = drFilaActual["Email"].ToString();
            }
            else
            {
                Nuevo();
            }

        }

        private void Atras(object sender, RoutedEventArgs e)
        {

            if (FilaActual == 0)
            {

                FilaActual = totalFilas;

            }
            FilaActual--;
            drFilaActual = dsAlumnos.Tables["Alumnos"].Rows[FilaActual];
            MostrarDatos();
            MostrarPagina();


        }

        private void Adelante(object sender, RoutedEventArgs e)
        {
            FilaActual++;
            if (FilaActual == totalFilas)
            {

                FilaActual = 0;

            }


            drFilaActual = dsAlumnos.Tables["Alumnos"].Rows[FilaActual];
            MostrarDatos();
            MostrarPagina();
        }

        private void MostrarPagina()
        {
            pagina.Content = FilaActual + 1 + "/" + totalFilas;
        }

        private void MostrarPrimero(object sender, RoutedEventArgs e)
        {
            FilaActual = 0;
            MostrarDatos();
            MostrarPagina();
        }

        private void MostrarUltimo(object sender, RoutedEventArgs e)
        {
            FilaActual = totalFilas - 1;
            MostrarDatos();
            MostrarPagina();
        }

        private void BotonNuevo(object sender, RoutedEventArgs e)
        {
            Nuevo();
            MostrarPagina();

        }

        private void Nuevo()
        {
            tbDni.Clear();
            nombre.Clear();
            Apellidos.Clear();
            telefono.Clear();
            email.Clear();
            MostrarPagina();

        }

        private void BotonAnyadir(object sender, RoutedEventArgs e)
        {

            DataRow dr = dsAlumnos.Tables["Alumnos"].NewRow();
            dr["dni"] = tbDni.Text;
            dr["nombre"] = nombre.Text;
            dr["apellido"] = Apellidos.Text;
            dr["tlf"] = telefono.Text;
            dr["Email"] = email.Text;

            

            DataAdapter.Update(dsAlumnos, "Alumnos");
            MessageBox.Show("Se ha añadido correctamente");

            



            SqlCommandBuilder cb = new SqlCommandBuilder(DataAdapter);
            DataAdapter.Update(dsAlumnos, "Alumnos");
           

            MostrarPagina();
        }

        private void BotonActualizar(object sender, RoutedEventArgs e)
        {
            if (totalFilas > 0)
            {
                DataRow d = dsAlumnos.Tables["Alumnos"].Rows[FilaActual];
                d["dni"] = tbDni.Text;
                d["nombre"] = nombre.Text;
                d["apellido"] = Apellidos.Text;
                d["tlf"] = telefono.Text;
                d["Email"] = email.Text;

                SqlCommandBuilder c = new SqlCommandBuilder(DataAdapter);
                DataAdapter.Update(dsAlumnos, "Alumnos");
            }
            MostrarPagina();
        }

        private void BotonBorrar(object sender, RoutedEventArgs e)
        {
            if (dsAlumnos.Tables["Alumnos"].Rows.Count == 1)
            {
                MessageBox.Show("Si borras te quedaras sin registros");
            }
            MessageBoxResult borrar = MessageBox.Show("¿Estas seguro de que deseas elimiar?", "Borrar Datos", MessageBoxButton.OKCancel);
            if (borrar == MessageBoxResult.OK)
            {
                dsAlumnos.Tables["Alumnos"].Rows[FilaActual].Delete();
                SqlCommandBuilder c = new SqlCommandBuilder(DataAdapter);
                DataAdapter.Update(dsAlumnos, "Alumnos");
                totalFilas--;
                if (FilaActual > totalFilas - 1)
                {
                    FilaActual = 0;
                }
            }
            MostrarDatos();
            MostrarPagina();
        }

        private void MostrarProfesores(object sender, RoutedEventArgs e)
        {
            Alumnos.Add(nombre.Text);
            for (int i = 0; i < Alumnos.Count; i++)
            {
                MessageBox.Show("Los Alumnos son : " + Alumnos[i]);
            }
        }

        private void tbDni_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidarC();
        }



        private void ValidarC()
        {
            while (1 == 1)
            {
                var vr = string.IsNullOrEmpty(tbDni.Text);
                if (!vr && tbDni.Text != "")
                {
                    BtAnydir.IsEnabled = true;

                }
            }

        }


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            BtAnydir.IsEnabled = false;
        }
    }
}

