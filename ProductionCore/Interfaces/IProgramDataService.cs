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
        /// Gets or sets the ProgramList
        /// Defines the ProgramList.........................
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
        /// Gets or sets the ProgramRequestShow
        /// Gets or sets a value indicating whether ProgramRequestShow......
        /// </summary>
        bool ProgramRequestShow { get; set; }

        /// <summary>
        /// Gets the CanCancel
        /// Gets a value indicating whether CanCancel......
        /// </summary>
        bool CanCancel { get; }

        /// <summary>
        /// Gets the CanConfirm
        /// Gets a value indicating whether CanConfirm......
        /// </summary>
        bool CanConfirm { get; }

        /// <summary>
        /// Gets or sets a value indicating whether AllowProgramChange.
        /// </summary>
        bool AllowProgramChange { get; set; }

        /// <summary>
        /// The UpdateProgramCycleTime.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        void IterateProgramCycles(IProgramData? program);

        /// <summary>
        /// The UpdateProgramAverageCycleTime.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData?"/>.</param>
        /// <param name="cycleTime">The cycleTime<see cref="TimeSpan"/>.</param>
        void UpdateProgramAverageCycleTime(IProgramData? program, TimeSpan cycleTime);

        /// <summary>
        /// The ProgramSelectClose.
        /// </summary>
        void ProgramSelectClose();

        /// <summary>
        /// The SetSelectedProgramAsCurrent.
        /// </summary>
        void SetSelectedProgramAsCurrent();

        /// <summary>
        /// The LoadProgram.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        void LoadProgram(IProgramData? program = null);

        /// <summary>
        /// The SaveProgram.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        void SaveProgram(IProgramData? program = null);

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
