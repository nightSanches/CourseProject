using CourseProject.Classes.Common;
using CourseProject.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject.View.Login
{
    public partial class Login : Page
    {
        public View.Menu.Main MainMenu;
        public Login()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == "" || tbPassword.Password == "")
            {
                labelError.Content = "Введите логин и пароль!";
                labelError.Visibility = Visibility.Visible;
            }
            else if (tbLogin.Text != "" && tbPassword.Password != "")
            {
               SqlLogin();
            }
        }

        public void SqlLogin()
        {
            //SqlConnection connection;
            //SqlDataReader data = Connection.Query($"SELECT * FROM users WHERE Login = '{tbLogin.Text}' AND Password = '{tbPassword.Text}'", out connection);

            SqlConnection connection;
            connection = Connection.OpenConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM users WHERE Login = @login AND Password = @password", connection);
            command.Parameters.AddWithValue("@login", tbLogin.Text);
            command.Parameters.AddWithValue("@password", tbPassword.Password);
            SqlDataReader data = command.ExecuteReader();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    MainWindow.init.curUser.Login = data.GetString(data.GetOrdinal("Login"));
                    MainWindow.init.curUser.Password = data.GetString(data.GetOrdinal("Password"));
                    MainWindow.init.curUser.Role = data.GetString(data.GetOrdinal("Role"));
                }
                MainMenu = new View.Menu.Main();
                MainWindow.init.frame.Navigate(MainMenu);
            }
            else
            {
                labelError.Content = "Неправильный логин или пароль!";
                labelError.Visibility = Visibility.Visible;
            }
            data.Close();
            Connection.CloseConnection(connection);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            labelError.Visibility = Visibility.Hidden;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            labelError.Visibility = Visibility.Hidden;
        }
    }
}
