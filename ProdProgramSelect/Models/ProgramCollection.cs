namespace ProdProgramSelect.Models
{
    using System.Collections.ObjectModel;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ProgramCollection" />.
    /// </summary>
    public class ProgramCollection : BindableBase, IProgramCollection
    {
        /// <summary>
        /// Defines the _programList.
        /// </summary>
        private ObservableCollection<IProgramData>? _programList = new ObservableCollection<IProgramData>();

        /// <summary>
        /// Gets or sets the ProgramList.
        /// </summary>
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
