using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseProject.Model
{
    public class Passengers : INotifyPropertyChanged
    {
        // Код пассажира
        private int id_passenger;
        public int Id_passenger
        {
            get { return id_passenger; }
            set
            {
                id_passenger = value;
                OnPropertyChanged("Id_passenger");
            }
        }

        // Фамилия
        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }

        // Имя
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        // Отчество
        private string patronymic;
        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }

        // Пасспорт
        private string passport;
        public string Passport
        {
            get { return passport; }
            set
            {
                passport = value;
                OnPropertyChanged("Passport");
            }
        }

        // Код рейса
        private Flights id_flight;
        public Flights Id_flight
        {
            get { return id_flight; }
            set
            {
                id_flight = value;
                OnPropertyChanged("Id_flight");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
