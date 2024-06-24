using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseProject.ViewModel
{
    public class VM_Planes : INotifyPropertyChanged
    {
        public ObservableCollection<Context.PlanesContext> Planes { get; set; }

        public Classes.RelayCommand NewPlanes
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    Context.PlanesContext newModel = new Context.PlanesContext(true);
                    Planes.Add(newModel);
                    View.Menu.Main.init.frame.Navigate(new View.Planes.Add(newModel));
                });
            }
        }

        public VM_Planes() =>
            Planes = Context.PlanesContext.AllPlanes();

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
