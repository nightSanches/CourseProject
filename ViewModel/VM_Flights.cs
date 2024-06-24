using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseProject.ViewModel
{
    public class VM_Flights : INotifyPropertyChanged
    {
        public ObservableCollection<Context.FlightsContext> Flights { get; set; }

        public Classes.RelayCommand NewFlights
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    Context.FlightsContext newModel = new Context.FlightsContext(true);
                    Flights.Add(newModel);
                    MainWindow.init.frame.Navigate(new View.Flights.Add(newModel));
                });
            }
        }

        public VM_Flights(string Filter1, string Filter2) =>
            Flights = Context.FlightsContext.AllFlights(Filter1, Filter2);

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
