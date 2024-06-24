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

namespace CourseProject.View.Baggage
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
                baggage = Context,
                passengers = new ViewModel.VM_Passengers("")
            };
            View.Menu.Main.init.ButtonsGrid.IsEnabled = false;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Weight.Text) || !Classes.Common.CheckRegex.Match("^[0-9]+$", Weight.Text))
            {
                MessageBox.Show("Неправильно указан Вес!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (cbPassenger.SelectedItem == null)
            {
                MessageBox.Show("Не выбран Пассажир!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else
            {
                Binding binding = new Binding("baggage.OnSave");
                ((Button)sender).SetBinding(Button.CommandProperty, binding);
            }
        }
    }
}
