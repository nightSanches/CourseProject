using CourseProject.Classes.Common;
using CourseProject.Classes;
using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace CourseProject.Context
{
    public class PassengersContext : Passengers
    {
        private bool isNew = false;
        public PassengersContext(bool save = false)
        {
            if (save)
            {
                Save(true);
                isNew = true;
            }
            Id_flight = new Flights();
        }

        public static ObservableCollection<PassengersContext> AllPassengers(string Filter = "")
        {
            ObservableCollection<PassengersContext> allPassengers = new ObservableCollection<PassengersContext>();
            ObservableCollection<FlightsContext> allFlights = FlightsContext.AllFlights();
            SqlConnection connection;
            if (Filter == "")
            {
                SqlDataReader dataPassengers = Connection.Query("SELECT * FROM passengers", out connection);
                while (dataPassengers.Read())
                {
                    allPassengers.Add(new PassengersContext()
                    {
                        Id_passenger = dataPassengers.GetInt32(0),
                        Surname = dataPassengers.GetString(1),
                        Name = dataPassengers.GetString(2),
                        Patronymic = dataPassengers.GetString(3),
                        Passport = dataPassengers.GetString(4),
                        Id_flight = dataPassengers.IsDBNull(5) ? null : allFlights.Where(x => x.Id_flight == dataPassengers.GetInt32(5)).First()
                    });
                }
                Connection.CloseConnection(connection);
            }
            else
            {
                SqlDataReader dataPassengers = Connection.Query("SELECT * FROM passengers WHERE Name LIKE '%" + Filter + "%' OR Surname LIKE '%" + Filter + "%' OR Patronymic LIKE '%" + Filter + "%'", out connection);
                while (dataPassengers.Read())
                {
                    allPassengers.Add(new PassengersContext()
                    {
                        Id_passenger = dataPassengers.GetInt32(0),
                        Surname = dataPassengers.GetString(1),
                        Name = dataPassengers.GetString(2),
                        Patronymic = dataPassengers.GetString(3),
                        Passport = dataPassengers.GetString(4),
                        Id_flight = dataPassengers.IsDBNull(5) ? null : allFlights.Where(x => x.Id_flight == dataPassengers.GetInt32(5)).First()
                    });
                }
                Connection.CloseConnection(connection);
            }
            return allPassengers;
        }

        public void Save(bool New = false)
        {
            SqlConnection connection;
            if (New)
            {
                SqlDataReader dataPassengers = Connection.Query("INSERT INTO " +
                    "[dbo].[passengers] (" +
                    "Surname, Name, Patronymic, Passport) " +
                    "OUTPUT Inserted.Id_passenger " +
                    $"VALUES ('{this.Surname}', '{this.Name}', '{this.Patronymic}', '{this.Passport}')", out connection);
                dataPassengers.Read();
                this.Id_passenger = dataPassengers.GetInt32(0);
            }
            else
            {
                Connection.Query("UPDATE [dbo].[passengers] " +
                    "SET " +
                    $"Surname = '{this.Surname}', " +
                    $"Name = '{this.Name}', " +
                    $"Patronymic = '{this.Patronymic}', " +
                    $"Passport = '{this.Passport}'," +
                    $"Id_flight = {this.Id_flight.Id_flight} " +
                    $"WHERE " +
                    $"Id_passenger = {this.Id_passenger}", out connection);
            }
            Connection.CloseConnection(connection);
            isNew = false;
            MainWindow.init.frame.Navigate(MainWindow.init.PassengersMain);
            View.Passengers.Main.init.ReloadPage();
        }

        public void Delete()
        {
            SqlConnection connection;
            Connection.Query("DELETE FROM [dbo].[passengers] " +
                "WHERE " +
                $"Id_passenger = {this.Id_passenger}", out connection);
            Connection.CloseConnection(connection);
        }

        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    MainWindow.init.frame.Navigate(new View.Passengers.Add(this));
                });
            }
        }

        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    MainWindow.init.ButtonsGrid.IsEnabled = true;
                    Id_flight = FlightsContext.AllFlights().Where(x => x.Id_flight == this.Id_flight.Id_flight).First();
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
                    (MainWindow.init.PassengersMain.DataContext as ViewModel.VM_Passengers).Passengers.Remove(this);
                });
            }
        }
        public RelayCommand OnCancel
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (isNew)
                    {
                        Delete();
                        (MainWindow.init.PassengersMain.DataContext as ViewModel.VM_Passengers).Passengers.Remove(this);
                        MainWindow.init.frame.Navigate(MainWindow.init.PassengersMain);
                        View.Passengers.Main.init.ReloadPage();
                    }
                    else if (!isNew)
                    {
                        MainWindow.init.frame.Navigate(MainWindow.init.PassengersMain);
                        View.Passengers.Main.init.ReloadPage();
                    }
                    MainWindow.init.ButtonsGrid.IsEnabled = true;
                });
            }
        }
    }
}
