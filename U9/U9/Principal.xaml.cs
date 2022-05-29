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


namespace U9
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void ButtonGestionProfes(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void ButtonGestionAlumnos(object sender, RoutedEventArgs e)
        {
            Alumno al = new Alumno();
            al.Show();
        }

        private void ButtonGestionCursos(object sender, RoutedEventArgs e)
        {
            Curso cr = new Curso();
            cr.Show();
        }
    }
}
