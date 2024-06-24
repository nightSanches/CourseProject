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

namespace CourseProject.View.Passengers
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
                passengers = Context,
                flights = new ViewModel.VM_Flights("", "")
            };
            View.Menu.Main.init.ButtonsGrid.IsEnabled = false;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Surname.Text) || !Classes.Common.CheckRegex.Match("^[а-яА-Я]+$", Surname.Text))
            {
                MessageBox.Show("Неправильно указана Фамилия!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (string.IsNullOrEmpty(Name.Text) || !Classes.Common.CheckRegex.Match("^[а-яА-Я]+$", Name.Text))
            {
                MessageBox.Show("Неправильно указано Имя!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (string.IsNullOrEmpty(Patronymic.Text) || !Classes.Common.CheckRegex.Match("^[а-яА-Я]+$", Patronymic.Text))
            {
                MessageBox.Show("Неправильно указано Отчество!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (string.IsNullOrEmpty(Passport.Text) || !Classes.Common.CheckRegex.Match(@"^\d{4}\s\d{6}$", Passport.Text))
            {
                MessageBox.Show("Неправильно указан Паспорт (0123 456789)!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else if (Flight.SelectedItem == null)
            {
                MessageBox.Show("Не выбран Рейс!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
            else
            {
                Binding binding = new Binding("passengers.OnSave");
                ((Button)sender).SetBinding(Button.CommandProperty, binding);
            }
        }
    }
}
