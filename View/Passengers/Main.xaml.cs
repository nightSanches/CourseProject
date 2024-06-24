using CourseProject.ViewModel;
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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public static Main init;
        public Main()
        {
            InitializeComponent();
            this.DataContext = new VM_Passengers(tbName.Text);
            init = this;
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }
        private void SearchPassengers(object sender, KeyEventArgs e)
        {
            Filter();
        }
        public void ReloadPage()
        {
            this.DataContext = new VM_Passengers(tbName.Text);
        }

        public void Filter()
        {
            this.DataContext = new VM_Passengers(tbName.Text);
        }

    }
}
