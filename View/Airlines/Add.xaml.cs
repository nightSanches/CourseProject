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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject.View.Airlines
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Add(object Context)
        {
            InitializeComponent();
            DataContext = new
            {
                airline = Context
            };
            View.Menu.Main.init.ButtonsGrid.IsEnabled = false;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Airline.Text))
            {
                MessageBox.Show("Не указано Название авиакомпании!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (string.IsNullOrEmpty(Country.Text))
            {
                MessageBox.Show("Не указана Страна!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else
            {
                Binding binding = new Binding("airline.OnSave");
                ((Button)sender).SetBinding(Button.CommandProperty, binding);
            }
        }
    }
}
