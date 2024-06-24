using CourseProject.Classes.Common;
using CourseProject.Classes;
using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Win32;
using Microsoft.Office.Interop.Excel;
using System.Windows;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace CourseProject.Context
{
    public class AirlinesContext : Airlines
    {
        private bool isNew = false; 
        public AirlinesContext(bool save = false)
        {
            if (save)
            {
                Save(true);
                isNew = true;
            }
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
            isNew = false;
            View.Menu.Main.init.frame.Navigate(View.Menu.Main.init.AirlinesMain);
            View.Airlines.Main.init.ReloadPage();
        }

        public void Delete()
        {
            SqlConnection connection;
            Connection.Query("DELETE FROM [dbo].[airlines] " +
                "WHERE " +
                $"Id_airline = {this.Id_airline}", out connection);
            Connection.CloseConnection(connection);
        }

        public static void ReportAirlines(ObservableCollection<AirlinesContext> airlines)
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

                    worksheet.Cells[1, 1] = "Код авиакомпании";
                    worksheet.Cells[1, 2] = "Наимнование авикомпании";
                    worksheet.Cells[1, 3] = "Страна регистрации";

                    int row = 2;
                    foreach (var airline in airlines)
                    {
                        worksheet.Cells[row, 1] = airline.Id_airline;
                        worksheet.Cells[row, 2] = airline.Airline_name;
                        worksheet.Cells[row, 3] = airline.Country;
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
                    View.Menu.Main.init.frame.Navigate(new View.Airlines.Add(this));
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
                    (View.Menu.Main.init.AirlinesMain.DataContext as ViewModel.VM_Airlines).Airlines.Remove(this);
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
                        (View.Menu.Main.init.AirlinesMain.DataContext as ViewModel.VM_Airlines).Airlines.Remove(this);
                        View.Menu.Main.init.frame.Navigate(View.Menu.Main.init.AirlinesMain);
                        View.Airlines.Main.init.ReloadPage();
                    }
                    else if(!isNew)
                    {
                        View.Menu.Main.init.frame.Navigate(View.Menu.Main.init.AirlinesMain);
                        View.Airlines.Main.init.ReloadPage();
                    }
                    View.Menu.Main.init.ButtonsGrid.IsEnabled = true;
                });
            }
        }
    }
}
