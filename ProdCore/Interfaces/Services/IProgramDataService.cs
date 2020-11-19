namespace ProdCore.Interfaces
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines the <see cref="IProgramDataService" />.
    /// </summary>
    public interface IProgramDataService
    {
        /// <summary>
        /// Gets or sets the ProgramList.
        /// </summary>
        ObservableCollection<IProgramData> ProgramList { get; set; }

        /// <summary>
        /// Gets or sets the SelectedProgramData.
        /// </summary>
        IProgramData? SelectedProgramData { get; set; }

        /// <summary>
        /// Gets a value indicating whether CanCancel......
        /// </summary>
        bool CanCancel { get; }

        /// <summary>
        /// Gets a value indicating whether CanConfirm......
        /// </summary>
        bool CanConfirm { get; }

        /// <summary>
        /// The ProgramSelectClose.
        /// </summary>
        void ProgramSelectClose();

        /// <summary>
        /// The SetSelectedProgramAsCurrent.
        /// </summary>
        void SetSelectedProgramAsCurrent();
    }
}
