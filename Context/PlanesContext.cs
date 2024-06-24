using CourseProject.Classes.Common;
using CourseProject.Classes;
using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System.Net.Mail;

namespace CourseProject.Context
{
    public class PlanesContext : Planes
    {
        private bool isNew = false;
        public PlanesContext(bool save = false)
        {
            if (save)
            {
                Save(true);
                isNew = true;
            }
        }

        public static ObservableCollection<PlanesContext> AllPlanes()
        {
            ObservableCollection<PlanesContext> allPlanes = new ObservableCollection<PlanesContext>();
            SqlConnection connection;
            SqlDataReader dataPlanes = Connection.Query("SELECT * FROM planes", out connection);
            while (dataPlanes.Read())
            {
                allPlanes.Add(new PlanesContext()
                {
                    Id_plane = dataPlanes.GetInt32(0),
                    Year_of_manufacture = dataPlanes.GetInt32(1),
                    Carrying = dataPlanes.GetInt32(2),
                    Seats = dataPlanes.GetInt32(3)
                });
            }
            Connection.CloseConnection(connection);
            return allPlanes;
        }

        public void Save(bool New = false)
        {
            SqlConnection connection;
            if (New)
            {
                SqlDataReader dataPlanes = Connection.Query("INSERT INTO " +
                    "[dbo].[planes] (" +
                    "Year_of_manufacture, Carrying, Seats) " +
                    "OUTPUT Inserted.Id_plane " +
                    $"VALUES (" +
                    $"{this.Year_of_manufacture}, {this.Carrying}, {this.Seats})", out connection);
                dataPlanes.Read();
                this.Id_plane = dataPlanes.GetInt32(0);
            }
            else
            {
                Connection.Query("UPDATE [dbo].[planes] " +
                    "SET " +
                    $"Year_of_manufacture = {this.Year_of_manufacture}, " +
                    $"Carrying = {this.Carrying}, " +
                    $"Seats = {this.Seats}" +
                    $"WHERE " +
                    $"Id_plane = {this.Id_plane}", out connection);
            }
            Connection.CloseConnection(connection);
            isNew = false;
            View.Menu.Main.init.frame.Navigate(View.Menu.Main.init.PlanesMain);
            View.Planes.Main.init.ReloadPage();
        }

        public void Delete()
        {
            SqlConnection connection;
            Connection.Query("DELETE FROM [dbo].[planes] " +
                "WHERE " +
                $"Id_plane = {this.Id_plane}", out connection);
            Connection.CloseConnection(connection);
        }

        public static void ReportPlanes(ObservableCollection<PlanesContext> planes)
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

                    worksheet.Cells[1, 1] = "Код самолета";
                    worksheet.Cells[1, 2] = "Год выпуска";
                    worksheet.Cells[1, 3] = "Грузоподъемность";
                    worksheet.Cells[1, 4] = "Кол-во пассажирских мест";

                    int row = 2;
                    foreach (var plane in planes)
                    {
                        worksheet.Cells[row, 1] = plane.Id_plane;
                        worksheet.Cells[row, 2] = plane.Year_of_manufacture;
                        worksheet.Cells[row, 3] = plane.Carrying;
                        worksheet.Cells[row, 4] = plane.Seats;
                        row++;
                    }
                    worksheet.Columns.AutoFit();
                    worksheet.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;

                    workbook.SaveAs(sfd.FileName);
                    workbook.Close();
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
                    View.Menu.Main.init.frame.Navigate(new View.Planes.Add(this));
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
                    (View.Menu.Main.init.PlanesMain.DataContext as ViewModel.VM_Planes).Planes.Remove(this);
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
                        (View.Menu.Main.init.PlanesMain.DataContext as ViewModel.VM_Planes).Planes.Remove(this);
                        View.Menu.Main.init.frame.Navigate(View.Menu.Main.init.PlanesMain);
                        View.Planes.Main.init.ReloadPage();
                    }
                    else if (!isNew)
                    {
                        View.Menu.Main.init.frame.Navigate(View.Menu.Main.init.PlanesMain);
                        View.Planes.Main.init.ReloadPage();
                    }
                    View.Menu.Main.init.ButtonsGrid.IsEnabled = true;
                });
            }
        }
    }
}
