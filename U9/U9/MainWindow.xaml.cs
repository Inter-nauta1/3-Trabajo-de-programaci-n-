using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace U9
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int FilaActual { get; set; } = 0;
        public int totalFilas { get; set; }
        DataSet dsProfesores = new DataSet();
        DataRow drFilaActual;
        SqlDataAdapter DataAdapter;

        public List <string> Profesores { get; set; } = new List <string>(); 
        public MainWindow()
        {
            InitializeComponent();
            string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Instituto.mdf;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();


            DataAdapter = new SqlDataAdapter("SELECT * FROM Profesores ", conexion);

            dsProfesores = new DataSet();
            DataAdapter.Fill(dsProfesores, "Profesores");
            totalFilas = dsProfesores.Tables["Profesores"].Rows.Count;

            MostrarDatos();
            conexion.Close();




        }

      
        public void MostrarDatos()
        {
            if (totalFilas > 0)
            {
                drFilaActual = dsProfesores.Tables["Profesores"].Rows[FilaActual];
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
            drFilaActual = dsProfesores.Tables["Profesores"].Rows[FilaActual];
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


            drFilaActual = dsProfesores.Tables["Profesores"].Rows[FilaActual];
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

            DataRow dr = dsProfesores.Tables["Profesores"].NewRow();
            dr["dni"] = tbDni.Text;
            dr["nombre"] = nombre.Text;
            dr["apellido"] = Apellidos.Text;
            dr["tlf"] = telefono.Text;
            dr["Email"] = email.Text;

            /* if (SeEscribio())
             {*/

            DataAdapter.Update(dsProfesores, "Profesores");
            MessageBox.Show("Se ha añadido correctamente");

            /* }else
             {
                 MessageBox.Show("No se ha introducido .Compruebe que se hayan escrito todos los campos ");
             }*/




            SqlCommandBuilder cb = new SqlCommandBuilder(DataAdapter);
            DataAdapter.Update(dsProfesores, "Profesores");
            /* /*bool vacio = true; 
              if (tbDni.Text == null || tbDni.Text == "")
              {

                  MessageBox.Show("No se ha introducido nada en el apartado DNI ");
              }
              else
              {
                  vacio = false;*/
            // dr["dni"] = tbDni.Text;

            //}

            /* if (nombre.Text == null || nombre.Text == "")
             {

                 MessageBox.Show("No se ha introducido nada en el apartado nombre ");
             }
             else
             {
                 vacio = false;*/
            // dr["nombre"] = nombre.Text;
            //}

            /*if (Apellidos.Text == null || Apellidos.Text == "")
            {
                
                MessageBox.Show("No se ha introducido nada en el apartado apellidos ");
           }
            else
            {
                vacio = false;*/
            // dr["apellido"] = Apellidos.Text;
            /*}

            if (telefono.Text == null || telefono.Text == "")
            {
                
                MessageBox.Show("No se ha introducido nada en el apartado telefono ");
            }
            else
            {
                vacio = false;*/
            // dr["tlf"] = telefono.Text;
            /* }

             if (email.Text == null || email.Text == "")
             {

                 MessageBox.Show("No se ha introducido nada en el apartado email ");
             }
             else
             {
                 vacio = false;*/
            //dr["email"] = email.Text;
            /*}
            if (!vacio)
            {*/
            /* dsProfesores.Tables["Profesores"].Rows.Add(dr);
             SqlCommandBuilder cb = new SqlCommandBuilder(DataAdapter);
             DataAdapter.Update(dsProfesores, "Profesores");*/
            //}*/


            MostrarPagina();
        }

        private void BotonActualizar(object sender, RoutedEventArgs e)
        {
            if (totalFilas > 0)
            {
                DataRow d = dsProfesores.Tables["Profesores"].Rows[FilaActual];
                d["dni"] = tbDni.Text;
                d["nombre"] = nombre.Text;
                d["apellido"] = Apellidos.Text;
                d["tlf"] = telefono.Text;
                d["Email"] = email.Text;

                SqlCommandBuilder c = new SqlCommandBuilder(DataAdapter);
                DataAdapter.Update(dsProfesores, "Profesores");
            }
            MostrarPagina();
        }

        private void BotonBorrar(object sender, RoutedEventArgs e)
        {
            if (dsProfesores.Tables["Profesores"].Rows.Count == 1)
            {
                MessageBox.Show("Si borras te quedaras sin registros");
            }
            MessageBoxResult borrar = MessageBox.Show("¿Estas seguro de que deseas elimiar?", "Borrar Datos", MessageBoxButton.OKCancel);
            if (borrar == MessageBoxResult.OK)
            {
                dsProfesores.Tables["Profesores"].Rows[FilaActual].Delete();
                SqlCommandBuilder c = new SqlCommandBuilder(DataAdapter);
                DataAdapter.Update(dsProfesores, "Profesores");
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
            Profesores.Add(nombre.Text);
            for (int i =0;i <Profesores.Count; i++)
            {
                MessageBox.Show("Los profesores son : "+Profesores[i]);
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

