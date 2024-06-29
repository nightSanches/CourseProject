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
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System.Windows;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace CourseProject.Context
{
    public class FlightsContext : Flights
    {
        private bool isNew = false;
        public FlightsContext(bool save = false)
        {
            if (save)
            {
                Save(true);
                isNew = true;
            }
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
                SqlDataReader dataFlights = Connection.Query("SELECT * FROM flights WHERE Departure LIKE N'%" + Filter1 + "%'", out connection);
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
                SqlDataReader dataFlights = Connection.Query("SELECT * FROM flights WHERE Destination LIKE N'%" + Filter2 + "%'", out connection);
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
                SqlDataReader dataFlights = Connection.Query("SELECT * FROM flights WHERE Departure LIKE N'%" + Filter1 + "%' AND Destination LIKE N'%" + Filter2 + "%'", out connection);
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
                    $"N'{this.Departure}', N'{this.Destination}', N'{this.Date_departure}', N'{this.Time_departure}', N'{this.Time_destination}')", out connection);
                dataFlights.Read();
                this.Id_flight = dataFlights.GetInt32(0);
            }
            else
            {
                Connection.Query("UPDATE [dbo].[flights] " +
                    "SET " +
                    $"Id_airline = {this.Id_airline.Id_airline}, " +
                    $"Id_plane = {this.Id_plane.Id_plane}, " +
                    $"Departure = N'{this.Departure}', " +
                    $"Destination = N'{this.Destination}', " +
                    $"Date_departure = N'{this.Date_departure}', " +
                    $"Time_departure = N'{this.Time_departure}', " +
                    $"Time_destination = N'{this.Time_destination}' " +
                    $"WHERE " +
                    $"Id_flight = {this.Id_flight}", out connection);
            }
            Connection.CloseConnection(connection);
            isNew = false;
            View.Menu.Main.init.frame.Navigate(View.Menu.Main.init.FlightsMain);
            View.Flights.Main.init.ReloadPage();
        }

        public void Delete()
        {
            SqlConnection connection;
            Connection.Query("DELETE FROM [dbo].[flights] " +
                "WHERE " +
                $"Id_flight = {this.Id_flight}", out connection);
            Connection.CloseConnection(connection);
        }

        public static void ReportFlights(ObservableCollection<FlightsContext> flights)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                Filter = "Excel (*.xlsx)|*.xlsx"
            };
            sfd.ShowDialog();
            if (sfd.FileName != "")
            {
                Application excelApp = new Application();
                try
                {
                    excelApp.Visible = false;
                    Workbook workbook = excelApp.Workbooks.Add();
                    Worksheet worksheet = (Worksheet)workbook.Sheets[1];

                    worksheet.Cells[1, 1] = "Код рейса";
                    worksheet.Cells[1, 2] = "Авиакомпания";
                    worksheet.Cells[1, 3] = "Самолет";
                    worksheet.Cells[1, 4] = "Место отправления";
                    worksheet.Cells[1, 5] = "Место назначения";
                    worksheet.Cells[1, 6] = "Дата отправления";
                    worksheet.Cells[1, 7] = "Время отправления";
                    worksheet.Cells[1, 8] = "Время прибытия";

                    int row = 2;
                    foreach (var flight in flights)
                    {
                        worksheet.Cells[row, 1] = flight.Id_flight;
                        worksheet.Cells[row, 2] = flight.Id_airline.Airline_name;
                        worksheet.Cells[row, 3] = flight.Id_plane.Id_plane;
                        worksheet.Cells[row, 4] = flight.Departure;
                        worksheet.Cells[row, 5] = flight.Destination;
                        worksheet.Cells[row, 6] = flight.Date_departure;
                        worksheet.Cells[row, 7] = flight.Time_departure;
                        worksheet.Cells[row, 8] = flight.Time_destination;
                        row++;
                    }
                    worksheet.Columns.AutoFit();
                    worksheet.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;

                    workbook.SaveAs(sfd.FileName);
                    workbook.Close();
                    MessageBox.Show("Экспорт данных был выполнен успешно!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { };
                excelApp.Quit();
            }
        }

        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    View.Menu.Main.init.frame.Navigate(new View.Flights.Add(this));
                });
            }
        }

        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    View.Menu.Main.init.ButtonsGrid.IsEnabled = true;
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
                    (View.Menu.Main.init.FlightsMain.DataContext as ViewModel.VM_Flights).Flights.Remove(this);
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
                        (View.Menu.Main.init.FlightsMain.DataContext as ViewModel.VM_Flights).Flights.Remove(this);
                        View.Menu.Main.init.frame.Navigate(View.Menu.Main.init.FlightsMain);
                        View.Flights.Main.init.ReloadPage();
                    }
                    else if (!isNew)
                    {
                        View.Menu.Main.init.frame.Navigate(View.Menu.Main.init.FlightsMain);
                        View.Flights.Main.init.ReloadPage();
                    }
                    View.Menu.Main.init.ButtonsGrid.IsEnabled = true;
                });
            }
        }
    }
}
