using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject.View.Menu
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public static Main init;
        public View.Airlines.Main AirlinesMain;
        public View.Planes.Main PlanesMain;
        public View.Flights.Main FlightsMain;
        public View.Passengers.Main PassengersMain;
        public View.Baggage.Main BaggageMain;
        public Main()
        {
            InitializeComponent();
            init = this;
            labelRole.Content = "Добро пожаловать! Роль - " + MainWindow.init.curUser.Role;
            if (MainWindow.init.curUser.Role != "admin")
            {
                passengers.Visibility = Visibility.Hidden;
                baggage.Visibility = Visibility.Hidden;
            }
            else if (MainWindow.init.curUser.Role == "admin")
            {
                passengers.Visibility = Visibility.Visible;
                baggage.Visibility = Visibility.Visible;
            }
        }

        private void OpenAirlines(object sender, RoutedEventArgs e)
        {
            AirlinesMain = new View.Airlines.Main();
            frame.Navigate(AirlinesMain);
        }

        private void OpenPlanes(object sender, RoutedEventArgs e)
        {
            PlanesMain = new View.Planes.Main();
            frame.Navigate(PlanesMain);
        }

        private void OpenFlights(object sender, RoutedEventArgs e)
        {
            FlightsMain = new View.Flights.Main();
            frame.Navigate(FlightsMain);
        }
        private void OpenPassengers(object sender, RoutedEventArgs e)
        {
            PassengersMain = new View.Passengers.Main();
            frame.Navigate(PassengersMain);
        }

        private void OpenBaggages(object sender, RoutedEventArgs e)
        {
            BaggageMain = new View.Baggage.Main();
            frame.Navigate(BaggageMain);
        }

        private void CloseMenu(object sender, RoutedEventArgs e)
        {
            MainWindow.init.curUser = new MainWindow.User();
            MainWindow.init.frame.Navigate(new Login.Login());
        }
    }
}
