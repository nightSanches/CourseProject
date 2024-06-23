using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseProject.Model
{
    public class Airlines : INotifyPropertyChanged
    {
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
        private string airline_name;
        public string Airline_name
        {
            get { return airline_name; }
            set
            {
                airline_name = value;
                OnPropertyChanged("Airline_name");
            }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
