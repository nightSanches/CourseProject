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
        public PassengersContext(bool save = false)
        {
            if (save) Save(true);
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
                SqlDataReader dataBaggage = Connection.Query("INSERT INTO " +
                    "[dbo].[baggage] (" +
                    "Weight) " +
                    "OUTPUT Inserted.Id_baggage " +
                    $"VALUES (" +
                    $"{this.Weight})", out connection);
                dataBaggage.Read();
                this.Id_baggage = dataBaggage.GetInt32(0);
            }
            else
            {
                Connection.Query("UPDATE [dbo].[baggage] " +
                    "SET " +
                    $"Id_passenger = {this.Id_passenger.Id_passenger}, " +
                    $"Weight = {this.Weight} " +
                    $"WHERE " +
                    $"Id_baggage = {this.Id_baggage}", out connection);
            }
            Connection.CloseConnection(connection);
            MainWindow.init.frame.Navigate(MainWindow.init.BaggageMain);
        }

        public void Delete()
        {
            SqlConnection connection;
            Connection.Query("DELETE FROM [dbo].[baggage] " +
                "WHERE " +
                $"Id_baggage = {this.Id_baggage}", out connection);
            Connection.CloseConnection(connection);
        }

        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    MainWindow.init.frame.Navigate(new View.Baggage.Add(this));
                });
            }
        }

        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Id_passenger = PassengersContext.AllPassengers().Where(x => x.Id_passenger == this.Id_passenger.Id_passenger).First();
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
                    (MainWindow.init.BaggageMain.DataContext as ViewModel.VM_Baggage).Baggages.Remove(this);
                });
            }
        }
    }
}
