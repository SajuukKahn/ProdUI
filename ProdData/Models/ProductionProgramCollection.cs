using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ProdData.Models
{
    public class ProductionProgramCollection : ObservableCollection<ProdDataProgramModel>, INotifyPropertyChanged
    {
        private List<ProdDataProgramModel> _productionProgramList = new List<ProdDataProgramModel>();
        public List<ProdDataProgramModel> ProductionProgramList
        {
            get
            {
                return _productionProgramList;
            }
            set
            { 
                _productionProgramList = value;
            }
        }


        public ProductionProgramCollection()
        {
            //RandomProgramGenerator randomProgramGenerator = new RandomProgramGenerator();
            //randomProgramGenerator.GenerateRandom(this);
        }
    }
}
