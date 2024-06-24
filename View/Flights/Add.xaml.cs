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

namespace CourseProject.View.Flights
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
                flights = Context,
                airline = new ViewModel.VM_Airlines(""),
                plane = new ViewModel.VM_Planes()
            };
            View.Menu.Main.init.ButtonsGrid.IsEnabled = false;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Departure.Text) || !Classes.Common.CheckRegex.Match(@"^[а-яА-ЯёЁ\-]+$", Departure.Text))
            {
                MessageBox.Show("Неправильно указано Место отправления!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (string.IsNullOrEmpty(Destination.Text) || !Classes.Common.CheckRegex.Match(@"^[а-яА-ЯёЁ\-]+$", Destination.Text))
            {
                MessageBox.Show("Неправильно указана Место назначения!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (cbAirline.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана Авиакомпания!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (cbPlane.SelectedItem == null)
            {
                MessageBox.Show("Не выбран Самолет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (string.IsNullOrEmpty(Date.Text) || !Classes.Common.CheckRegex.Match(@"^(0[1-9]|1[0-9]|2[0-9]|3[01])\.(0[1-9]|1[012])\.\d{4}$", Date.Text))
            {
                MessageBox.Show("Неправильно указана Дата (01.01.0001)!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (string.IsNullOrEmpty(TimeDep.Text) || !Classes.Common.CheckRegex.Match(@"^\b(?:[01][0-9]|2[0-3]):[0-5][0-9]\b$", TimeDep.Text))
            {
                MessageBox.Show("Неправильно указано Время отправления (00:00)!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (string.IsNullOrEmpty(TimeDes.Text) || !Classes.Common.CheckRegex.Match(@"^\b(?:[01][0-9]|2[0-3]):[0-5][0-9]\b$", TimeDes.Text))
            {
                MessageBox.Show("Неправильно указано Время прибытия (00:00)!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else
            {
                Binding binding = new Binding("flights.OnSave");
                ((Button)sender).SetBinding(Button.CommandProperty, binding);
            }
        }
    }
}
