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
