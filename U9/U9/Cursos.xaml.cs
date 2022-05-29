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
    /// Lógica de interacción para Window2.xaml
    /// </summary>
    public partial class Curso : Window
    {
        public int FilaActual { get; set; } = 0;
        public int totalFilas { get; set; }
        DataSet dsCursos = new DataSet();
        DataRow drFilaActual;
        SqlDataAdapter DataAdapter;
        public List<string> Crusos { get; set; } = new List<string>();


        public Curso()
        {
            InitializeComponent();
            string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Instituto.mdf;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();


            DataAdapter = new SqlDataAdapter("SELECT * FROM Cursos ", conexion);

            dsCursos = new DataSet();
            DataAdapter.Fill(dsCursos, "Cursos");
            totalFilas = dsCursos.Tables["Cursos"].Rows.Count;

            MostrarDatos();
            conexion.Close();
        }

        public void MostrarDatos()
        {
            if (totalFilas > 0)
            {
                drFilaActual = dsCursos.Tables["Cursos"].Rows[FilaActual];
                tbDni.Text = drFilaActual["codigo"].ToString();
                nombre.Text = drFilaActual["nombre"].ToString();
                
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
            drFilaActual = dsCursos.Tables["Cursos"].Rows[FilaActual];
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


            drFilaActual = dsCursos.Tables["Cursos"].Rows[FilaActual];
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

            DataRow dr = dsCursos.Tables["Cursos"].NewRow();
            dr["codigo"] = tbDni.Text;
            dr["nombre"] = nombre.Text;
            

            /* if (SeEscribio())
             {*/

            DataAdapter.Update(dsCursos, "Cursos");
            MessageBox.Show("Se ha añadido correctamente");

            /* }else
             {
                 MessageBox.Show("No se ha introducido .Compruebe que se hayan escrito todos los campos ");
             }*/




            SqlCommandBuilder cb = new SqlCommandBuilder(DataAdapter);
            DataAdapter.Update(dsCursos, "Cursos");
            


            MostrarPagina();
        }

        private void BotonActualizar(object sender, RoutedEventArgs e)
        {
            if (totalFilas > 0)
            {
                DataRow d = dsCursos.Tables["Cursos"].Rows[FilaActual];
                d["codigo"] = tbDni.Text;
                d["nombre"] = nombre.Text;
                

                SqlCommandBuilder c = new SqlCommandBuilder(DataAdapter);
                DataAdapter.Update(dsCursos, "Cursos");
            }
            MostrarPagina();
        }

        private void BotonBorrar(object sender, RoutedEventArgs e)
        {
            if (dsCursos.Tables["Cursos"].Rows.Count == 1)
            {
                MessageBox.Show("Si borras te quedaras sin registros");
            }
            MessageBoxResult borrar = MessageBox.Show("¿Estas seguro de que deseas elimiar?", "Borrar Datos", MessageBoxButton.OKCancel);
            if (borrar == MessageBoxResult.OK)
            {
                dsCursos.Tables["Cursos"].Rows[FilaActual].Delete();
                SqlCommandBuilder c = new SqlCommandBuilder(DataAdapter);
                DataAdapter.Update(dsCursos, "Cursos");
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
            Crusos.Add(nombre.Text);
            for (int i = 0; i < Crusos.Count; i++)
            {
                MessageBox.Show("Los cursos son : " + Crusos[i]);
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
