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
    public class BaggageContext : Baggage
    {
        private bool isNew = false;
        public BaggageContext(bool save = false)
        {
            if (save) Save(true);
            Id_passenger = new Passengers();
            isNew = true;
        }

        public static ObservableCollection<BaggageContext> AllBaggage(string Filter = "")
        {
            ObservableCollection<BaggageContext> allBaggage = new ObservableCollection<BaggageContext>();
            ObservableCollection<PassengersContext> allPassengers = PassengersContext.AllPassengers();
            SqlConnection connection;
            if (Filter == "")
            {
                SqlDataReader dataBaggage = Connection.Query("SELECT * FROM baggage", out connection);
                while (dataBaggage.Read())
                {
                    allBaggage.Add(new BaggageContext()
                    {
                        Id_baggage = dataBaggage.GetInt32(0),
                        Id_passenger = dataBaggage.IsDBNull(1) ? null : allPassengers.Where(x => x.Id_passenger == dataBaggage.GetInt32(1)).First(),
                        Weight = dataBaggage.GetInt32(2)
                    });
                }
                Connection.CloseConnection(connection);
            }
            else
            {
                SqlDataReader dataBaggage = Connection.Query("SELECT * FROM baggage WHERE Id_passenger LIKE '%" + Filter + "%'", out connection);
                while (dataBaggage.Read())
                {
                    allBaggage.Add(new BaggageContext()
                    {
                        Id_baggage = dataBaggage.GetInt32(0),
                        Id_passenger = dataBaggage.IsDBNull(1) ? null : allPassengers.Where(x => x.Id_passenger == dataBaggage.GetInt32(1)).First(),
                        Weight = dataBaggage.GetInt32(2)
                    });
                }
                Connection.CloseConnection(connection);
            }
            return allBaggage;
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
            isNew = false;
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
                    //MainWindow.init.frame.Navigate(new View.Baggage.Add(this));
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
                    (MainWindow.init.BaggageMain.DataContext as ViewModel.VM_Baggage).Baggage.Remove(this);
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
                        (MainWindow.init.BaggageMain.DataContext as ViewModel.VM_Baggage).Baggage.Remove(this);
                        MainWindow.init.frame.Navigate(MainWindow.init.BaggageMain);
                    }
                    else if (!isNew)
                    {
                        MainWindow.init.frame.Navigate(MainWindow.init.BaggageMain);
                        View.Baggage.Main.init.ReloadPage();
                    }
                });
            }
        }
    }
}
