using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProductionCore.Interfaces
{
    public interface IProgramCollection
    {
        ObservableCollection<IProgramData>? ProgramList { get; set; }
    }
}
