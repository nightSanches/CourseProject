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

namespace CourseProject.View.Baggage.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        public Item()
        {
            InitializeComponent();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить информацию о Багаже?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Информация о Багаже успешно удалена!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                Binding binding = new Binding("OnDelete");
                ((Button)sender).SetBinding(Button.CommandProperty, binding);
            }
            else
            {
                BindingOperations.ClearBinding((Button)sender, Button.CommandProperty);
            }
        }
    }
}
