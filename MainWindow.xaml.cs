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
    public partial class MainWindow : Window
    {
        public class User
        {
            public string Login;
            public string Password;
            public string Role;
        }
        public User curUser = new User();
        public View.Login.Login Login;
        public static MainWindow init;
        public MainWindow()
        {
            InitializeComponent();
            init = this;
            Login = new View.Login.Login();
            frame.Navigate(Login);
        }
    }
}
