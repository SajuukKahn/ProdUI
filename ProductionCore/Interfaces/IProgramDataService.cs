namespace ProductionCore.Interfaces
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines the <see cref="IProgramDataService" />.
    /// </summary>
    public interface IProgramDataService
    {
        /// <summary>
        /// Gets or sets the ProgramList
        /// Defines the ProgramList.....................
        /// </summary>
        ObservableCollection<IProgramData> ProgramList { get; set; }

        /// <summary>
        /// Gets or sets the SelectedProgramData.
        /// </summary>
        IProgramData? SelectedProgramData { get; set; }

        /// <summary>
        /// Gets or sets the CurrentProgram.
        /// </summary>
        IProgramData? CurrentProgram { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ProgramRequestShow..
        /// </summary>
        bool ProgramRequestShow { get; set; }

        /// <summary>
        /// Gets a value indicating whether CanCancel..
        /// </summary>
        bool CanCancel { get; }

        /// <summary>
        /// Gets a value indicating whether CanConfirm..
        /// </summary>
        bool CanConfirm { get; }

        /// <summary>
        /// The UpdateProgramCycleTime.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        /// <param name="cycles">The cycles<see cref="long"/>.</param>
        void UpdateProgramCycleTime(IProgramData? program, long cycles);

        /// <summary>
        /// The ProgramSelectClose.
        /// </summary>
        void ProgramSelectClose();

        /// <summary>
        /// The SetSelectedProgramAsCurrent.
        /// </summary>
        void SetSelectedProgramAsCurrent();

        /// <summary>
        /// The Program.
        /// </summary>
        /// <returns>A new <see cref="IProgramData"/>.</returns>
        IProgramData CreateProgram();

        /// <summary>
        /// The CreateBarcode.
        /// </summary>
        /// <returns>The <see cref="IBarcode"/>.</returns>
        IBarcode CreateBarcode();
    }
}
