using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseProject.Model
{
    public class Flights : INotifyPropertyChanged
    {
        // Код рейса
        private int id_flight;
        public int Id_flight
        {
            get { return id_flight; }
            set
            {
                id_flight = value;
                OnPropertyChanged("Id_flight");
            }
        }

        // Код авиакомпании
        private int id_airline;
        public int Id_airline
        {
            get { return id_airline; }
            set
            {
                id_airline = value;
                OnPropertyChanged("Id_airline");
            }
        }

        // Код самолета
        private int id_plane;
        public int Id_plane
        {
            get { return id_plane; }
            set
            {
                id_plane = value;
                OnPropertyChanged("Id_plane");
            }
        }

        // Место отправления
        private string departure;
        public string Departure
        {
            get { return departure; }
            set
            {
                departure = value;
                OnPropertyChanged("Departure");
            }
        }

        // Место назначения
        private string destination;
        public string Destination
        {
            get { return destination; }
            set
            {
                destination = value;
                OnPropertyChanged("Destination");
            }
        }

        // Дата отправления
        private DateTime date_departure;
        public DateTime Date_departure
        {
            get { return date_departure; }
            set
            {
                date_departure = value;
                OnPropertyChanged("Date_departure");
            }
        }

        // Время отправления
        private string time_departure;
        public string Time_departure
        {
            get { return time_departure; }
            set
            {
                time_departure = value;
                OnPropertyChanged("Time_departure");
            }
        }

        // Время прибытия
        private string time_destination;
        public string Time_destination
        {
            get { return time_destination; }
            set
            {
                time_destination = value;
                OnPropertyChanged("Time_destination");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
