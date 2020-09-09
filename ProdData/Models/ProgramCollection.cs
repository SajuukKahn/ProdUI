using Prism.Mvvm;
using ProductionCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace ProdData.Models
{
    public class ProgramCollection : BindableBase, IProgramCollection
    { 
        //TODO Is this the best way to handle a collection, or should the base class inherit from a collection Interface?
        private ObservableCollection<IProgramData>? _programList = new ObservableCollection<IProgramData>();

        public ObservableCollection<IProgramData>? ProgramList
        {
            get
            {
                return _programList;
            }
            set
            {
                SetProperty(ref _programList, value);
            }
        }
    }
}
