using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseProject.ViewModel
{
    public class VM_Passengers : INotifyPropertyChanged
    {
        public ObservableCollection<Context.PassengersContext> Passengers { get; set; }

        public Classes.RelayCommand NewPassengers
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    Context.PassengersContext newModel = new Context.PassengersContext(true);
                    Passengers.Add(newModel);
                    //MainWindow.init.frame.Navigate(new View.Passengers.Add(newModel));
                });
            }
        }

        public VM_Passengers(string Filter) =>
            Passengers = Context.PassengersContext.AllPassengers(Filter);

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
