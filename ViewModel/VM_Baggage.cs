﻿using CourseProject.Context;
using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseProject.ViewModel
{
    public class VM_Baggage : INotifyPropertyChanged
    {
        public ObservableCollection<Context.BaggageContext> Baggage { get; set; }

        public Classes.RelayCommand NewBaggage
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    Context.BaggageContext newModel = new Context.BaggageContext(true);
                    Baggage.Add(newModel);
                    View.Menu.Main.init.frame.Navigate(new View.Baggage.Add(newModel));
                });
            }
        }

        public VM_Baggage(string Filter) =>
            Baggage = Context.BaggageContext.AllBaggage(Filter);

        public Classes.RelayCommand ReportBaggage
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    Context.BaggageContext.ReportBaggage(Baggage);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
