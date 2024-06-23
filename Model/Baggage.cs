using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseProject.Model
{
    public class Baggage : INotifyPropertyChanged
    {
        // Код багажа
        private int id_baggage;
        public int Id_baggage
        {
            get { return id_baggage; }
            set
            {
                id_baggage = value;
                OnPropertyChanged("Id_baggage");
            }
        }

        // Код пассажира
        private Passengers id_passenger;
        public Passengers Id_passenger
        {
            get { return id_passenger; }
            set
            {
                id_passenger = value;
                OnPropertyChanged("Id_passenger");
            }
        }

        // Вес багажа
        private int weight;
        public int Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
