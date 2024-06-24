using CourseProject.Classes.Common;
using CourseProject.Classes;
using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Linq;

namespace CourseProject.Context
{
    public class FlightsContext : Flights
    {
        public FlightsContext(bool save = false)
        {
            if (save) Save(true);
            Id_airline = new Airlines();
            Id_plane = new Planes();
        }

        public static ObservableCollection<FlightsContext> AllFlights(string Filter1 = "", string Filter2 = "")
        {
            ObservableCollection<FlightsContext> allFlights = new ObservableCollection<FlightsContext>();
            ObservableCollection<AirlinesContext> allAirlines = AirlinesContext.AllAirlines();
            ObservableCollection<PlanesContext> allPlanes = PlanesContext.AllPlanes();
            SqlConnection connection;
            if (Filter1 == "" && Filter2 == "")
            {
                SqlDataReader dataFlights = Connection.Query("SELECT * FROM flights", out connection);
                while (dataFlights.Read())
                {
                    allFlights.Add(new FlightsContext()
                    {
                        Id_flight = dataFlights.GetInt32(0),
                        Id_airline = dataFlights.IsDBNull(1) ? null : allAirlines.Where(x => x.Id_airline == dataFlights.GetInt32(1)).First(),
                        Id_plane = dataFlights.IsDBNull(2) ? null : allPlanes.Where(x => x.Id_plane == dataFlights.GetInt32(2)).First(),
                        Departure = dataFlights.GetString(3),
                        Destination = dataFlights.GetString(4),
                        Date_departure = dataFlights.GetString(5),
                        Time_departure = dataFlights.GetString(6),
                        Time_destination = dataFlights.GetString(7)
                    });
                }
                Connection.CloseConnection(connection);
            }
            else if(Filter1 != "" && Filter2 == "")
            {
                SqlDataReader dataFlights = Connection.Query("SELECT * FROM flights WHERE Departure LIKE '%" + Filter1 + "%'", out connection);
                while (dataFlights.Read())
                {
                    allFlights.Add(new FlightsContext()
                    {
                        Id_flight = dataFlights.GetInt32(0),
                        Id_airline = dataFlights.IsDBNull(1) ? null : allAirlines.Where(x => x.Id_airline == dataFlights.GetInt32(1)).First(),
                        Id_plane = dataFlights.IsDBNull(2) ? null : allPlanes.Where(x => x.Id_plane == dataFlights.GetInt32(2)).First(),
                        Departure = dataFlights.GetString(3),
                        Destination = dataFlights.GetString(4),
                        Date_departure = dataFlights.GetString(5),
                        Time_departure = dataFlights.GetString(6),
                        Time_destination = dataFlights.GetString(7)
                    });
                }
                Connection.CloseConnection(connection);
            }
            else if(Filter1 == "" && Filter2 != "")
            {
                SqlDataReader dataFlights = Connection.Query("SELECT * FROM flights WHERE Destination LIKE '%" + Filter2 + "%'", out connection);
                while (dataFlights.Read())
                {
                    allFlights.Add(new FlightsContext()
                    {
                        Id_flight = dataFlights.GetInt32(0),
                        Id_airline = dataFlights.IsDBNull(1) ? null : allAirlines.Where(x => x.Id_airline == dataFlights.GetInt32(1)).First(),
                        Id_plane = dataFlights.IsDBNull(2) ? null : allPlanes.Where(x => x.Id_plane == dataFlights.GetInt32(2)).First(),
                        Departure = dataFlights.GetString(3),
                        Destination = dataFlights.GetString(4),
                        Date_departure = dataFlights.GetString(5),
                        Time_departure = dataFlights.GetString(6),
                        Time_destination = dataFlights.GetString(7)
                    });
                }
                Connection.CloseConnection(connection);
            }
            else if(Filter1 != "" && Filter2 !="")
            {
                SqlDataReader dataFlights = Connection.Query("SELECT * FROM flights WHERE Departure LIKE '%" + Filter1 + "%' AND Destination LIKE '%" + Filter2 + "%'", out connection);
                while (dataFlights.Read())
                {
                    allFlights.Add(new FlightsContext()
                    {
                        Id_flight = dataFlights.GetInt32(0),
                        Id_airline = dataFlights.IsDBNull(1) ? null : allAirlines.Where(x => x.Id_airline == dataFlights.GetInt32(1)).First(),
                        Id_plane = dataFlights.IsDBNull(2) ? null : allPlanes.Where(x => x.Id_plane == dataFlights.GetInt32(2)).First(),
                        Departure = dataFlights.GetString(3),
                        Destination = dataFlights.GetString(4),
                        Date_departure = dataFlights.GetString(5),
                        Time_departure = dataFlights.GetString(6),
                        Time_destination = dataFlights.GetString(7)
                    });
                }
                Connection.CloseConnection(connection);
            }
            return allFlights;
        }

        public void Save(bool New = false)
        {
            SqlConnection connection;
            if (New)
            {
                SqlDataReader dataFlights = Connection.Query("INSERT INTO " +
                    "[dbo].[flights] (" +
                    "Departure, Destination, Date_departure, Time_departure, Time_destination) " +
                    "OUTPUT Inserted.Id_flight " +
                    $"VALUES (" +
                    $"'{this.Departure}', '{this.Destination}', '{this.Date_departure}', '{this.Time_departure}', '{this.Time_destination}')", out connection);
                dataFlights.Read();
                this.Id_flight = dataFlights.GetInt32(0);
            }
            else
            {
                Connection.Query("UPDATE [dbo].[flights] " +
                    "SET " +
                    $"Id_airline = {this.Id_airline.Id_airline}, " +
                    $"Id_plane = {this.Id_plane.Id_plane}, " +
                    $"Departure = '{this.Departure}', " +
                    $"Destination = '{this.Destination}', " +
                    $"Date_departure = '{this.Date_departure}', " +
                    $"Time_departure = '{this.Time_departure}', " +
                    $"Time_destination = '{this.Time_destination}' " +
                    $"WHERE " +
                    $"Id_flight = {this.Id_flight}", out connection);
            }
            Connection.CloseConnection(connection);
            MainWindow.init.frame.Navigate(MainWindow.init.FlightsMain);
        }

        public void Delete()
        {
            SqlConnection connection;
            Connection.Query("DELETE FROM [dbo].[flights] " +
                "WHERE " +
                $"Id_flight = {this.Id_flight}", out connection);
            Connection.CloseConnection(connection);
        }

        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    MainWindow.init.frame.Navigate(new View.Flights.Add(this));
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
                    Id_airline = AirlinesContext.AllAirlines().Where(x => x.Id_airline == this.Id_airline.Id_airline).First();
                    Id_plane = PlanesContext.AllPlanes().Where(x => x.Id_plane == this.Id_plane.Id_plane).First();
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
                    (MainWindow.init.FlightsMain.DataContext as ViewModel.VM_Flights).Flights.Remove(this);
                });
            }
        }
    }
}
