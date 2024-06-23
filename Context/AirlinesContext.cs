using CourseProject.Classes.Common;
using CourseProject.Classes;
using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

namespace CourseProject.Context
{
    public class AirlinesContext : Airlines
    {
        public AirlinesContext(bool save = false)
        {
            if (save) Save(true);
        }

        public static ObservableCollection<AirlinesContext> AllAirlines(string Filter = "")
        {
            ObservableCollection<AirlinesContext> allAirlines = new ObservableCollection<AirlinesContext>();
            SqlConnection connection;
            if (Filter == "")
            {
                SqlDataReader dataAirlines = Connection.Query("SELECT * FROM airlines", out connection);
                while (dataAirlines.Read())
                {
                    allAirlines.Add(new AirlinesContext()
                    {
                        Id_airline = dataAirlines.GetInt32(0),
                        Airline_name = dataAirlines.GetString(1),
                        Country = dataAirlines.GetString(2)
                    });
                }
                Connection.CloseConnection(connection);
            }
            else
            {
                SqlDataReader dataAirlines = Connection.Query("SELECT * FROM airlines WHERE Airline_name LIKE '%" + Filter + "%'", out connection);
                while (dataAirlines.Read())
                {
                    allAirlines.Add(new AirlinesContext()
                    {
                        Id_airline = dataAirlines.GetInt32(0),
                        Airline_name = dataAirlines.GetString(1),
                        Country = dataAirlines.GetString(2)
                    });
                }
                Connection.CloseConnection(connection);
            }
            return allAirlines;
        }

        public void Save(bool New = false)
        {
            SqlConnection connection;
            if (New)
            {
                SqlDataReader dataAirlines = Connection.Query("INSERT INTO " +
                    "[dbo].[airlines] (" +
                    "Airline_name, " +
                    "Country) " +
                    "OUTPUT Inserted.Id_airline " +
                    $"VALUES (" +
                    $"'{this.Airline_name}', '{this.Country}')", out connection);
                dataAirlines.Read();
                this.Id_airline = dataAirlines.GetInt32(0);
            }
            else
            {
                Connection.Query("UPDATE [dbo].[airlines] " +
                    "SET " +
                    $"Airline_name = '{this.Airline_name}', " +
                    $"Country = '{this.Country}' " +
                    $"WHERE " +
                    $"Id_airline = {this.Id_airline}", out connection);
            }
            Connection.CloseConnection(connection);
            MainWindow.init.frame.Navigate(MainWindow.init.AirlinesMain);
        }

        public void Delete()
        {
            SqlConnection connection;
            Connection.Query("DELETE FROM [dbo].[airlines] " +
                "WHERE " +
                $"Id_airline = {this.Id_airline}", out connection);
            Connection.CloseConnection(connection);
        }

        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    MainWindow.init.frame.Navigate(new View.Airlines.Add(this));
                });
            }
        }

        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Save();
                });
            }
        }

        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Delete();
                    (MainWindow.init.AirlinesMain.DataContext as ViewModel.VM_Airlines).Airlines.Remove(this);
                });
            }
        }
    }
}
