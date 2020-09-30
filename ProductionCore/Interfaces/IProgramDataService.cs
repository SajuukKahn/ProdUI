namespace ProductionCore.Interfaces
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines the <see cref="IProgramDataService" />.
    /// </summary>
    public interface IProgramDataService
    {

        /// <summary>
        /// Defines the ProgramRequestOpenChanged.
        /// </summary>
        public event EventHandler ProgramRequestOpenChanged;

        /// <summary>
        /// Gets or sets the ProgramList
        /// Defines the ProgramList............
        /// </summary>
        public ObservableCollection<IProgramData> ProgramList { get; set; }

        /// <summary>
        /// Gets or sets the SelectedProgramData.
        /// </summary>
        public IProgramData? SelectedProgramData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ProgramRequestOpen.
        /// </summary>
        public bool ProgramRequestOpen { get; set; }

        /// <summary>
        /// The ProgramSelectRequest.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ProgramSelectRequest();

        /// <summary>
        /// The ProgramSelectClose.
        /// </summary>
        public void ProgramSelectClose();

        /// <summary>
        /// The Program.
        /// </summary>
        /// <returns>A new <see cref="IProgramData"/>.</returns>
        public IProgramData CreateProgram();

        /// <summary>
        /// The CreateBarcode.
        /// </summary>
        /// <returns>The <see cref="IBarcode"/>.</returns>
        public IBarcode CreateBarcode();
    }
}
