using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseProject.ViewModel
{
    public class VM_Airlines : INotifyPropertyChanged
    {
        public ObservableCollection<Context.AirlinesContext> Airlines { get; set; }

        public Classes.RelayCommand NewAirlines
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    Context.AirlinesContext newModel = new Context.AirlinesContext(true);
                    Airlines.Add(newModel);
                    View.Menu.Main.init.frame.Navigate(new View.Airlines.Add(newModel));
                });
            }
        }

        public VM_Airlines(string Filter) =>
            Airlines = Context.AirlinesContext.AllAirlines(Filter);

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
