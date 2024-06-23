using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseProject.Model
{
    public class Planes
    {
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
        private int year_of_manufacture;
        public int Year_of_manufacture
        {
            get { return year_of_manufacture; }
            set
            {
                year_of_manufacture = value;
                OnPropertyChanged("Year_of_manufacture");
            }
        }

        private int carrying;
        public int Carrying
        {
            get { return carrying; }
            set
            {
                carrying = value;
                OnPropertyChanged("Carrying");
            }
        }

        private int seats;
        public int Seats
        {
            get { return seats; }
            set
            {
                seats = value;
                OnPropertyChanged("Seats");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
