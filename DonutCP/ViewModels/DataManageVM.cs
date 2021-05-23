using DonutCP.Model.DataServices;
using DonutCP.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace DonutCP.ViewModels
{
    class DataManageVM : INotifyPropertyChanged
    {

        private List<High_Lights> allHightLights = DataServices.GetAllHightLights();
        public List<High_Lights> AllHightLights
        {
            get { return allHightLights; }
            set
            {
                allHightLights = value;
                NotifyPropertyChanged("AllHightLights");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        //Методы открытия окон
        
    }
}
