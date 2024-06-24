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
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public View.Menu.Main MainMenu;
        public Login()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            SqlLogin();
        }

        public void SqlLogin()
        {
            //SqlConnection connection;
            //SqlDataReader data = Connection.Query($"SELECT * FROM users WHERE Login = '{tbLogin.Text}' AND Password = '{tbPassword.Text}'", out connection);

            SqlConnection connection;
            connection = Connection.OpenConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM users WHERE Login = @login AND Password = @password", connection);
            command.Parameters.AddWithValue("@login", tbLogin.Text);
            command.Parameters.AddWithValue("@password", tbPassword.Text);
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
                MessageBox.Show("Неправильный логин или пароль!");
            }
            data.Close();
            Connection.CloseConnection(connection);
        }
    }
}
