using CourseProject.View.Airlines;
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

namespace CourseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public Main BaggageMain;
        public Main AirlinesMain;
        public Main FlightsMain;
        public Main PassengersMain;
        public Main PlanesMain;
        public MainWindow()
        {
            InitializeComponent();
            init = this;
        }

        private void OpenAirlines(object sender, RoutedEventArgs e)
        {
            AirlinesMain = new View.Airlines.Main();
            frame.Navigate(AirlinesMain);
        }
    }
}
