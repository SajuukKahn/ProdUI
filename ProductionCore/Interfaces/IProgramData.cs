namespace ProductionCore.Interfaces
{
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="IProgramData" />.
    /// </summary>
    public interface IProgramData
    {
        /// <summary>
        /// Gets or sets a value indicating whether AutoStartPlayback.
        /// </summary>
        bool AutoStartPlayback { get; set; }

        /// <summary>
        /// Gets or sets the AverageCycleTime.
        /// </summary>
        TimeSpan AverageCycleTime { get; set; }

        /// <summary>
        /// Gets or sets the Barcode.
        /// </summary>
        IBarcode? Barcode { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Dimensions.
        /// </summary>
        Size Dimensions { get; set; }

        /// <summary>
        /// Gets or sets the HistoricalCycles.
        /// </summary>
        long HistoricalCycles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsFavorite.
        /// </summary>
        bool IsFavorite { get; set; }

        /// <summary>
        /// Gets or sets the LastEditDate.
        /// </summary>
        DateTime LastEditDate { get; set; }

        /// <summary>
        /// Gets or sets the ProductImage.
        /// </summary>
        BitmapImage? ProductImage { get; set; }

        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the ProgramCreator.
        /// </summary>
        string? ProgramCreator { get; set; }

        /// <summary>
        /// Gets or sets the ProgramName.
        /// </summary>
        string? ProgramName { get; set; }

        /// <summary>
        /// Gets or sets the ToolsUsed.
        /// </summary>
        ObservableCollection<string> ToolsUsed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether UserCanStartPlayback.
        /// </summary>
        bool UserCanStartPlayback { get; set; }

        /// <summary>
        /// The UpdateAverageCycleTime.
        /// </summary>
        /// <param name="newestCycleTime">The newestCycleTime<see cref="TimeSpan"/>.</param>
        void UpdateAverageCycleTime(TimeSpan newestCycleTime);

        /// <summary>
        /// The UpdateLastEditDate.
        /// </summary>
        void UpdateLastEditDate();
    }
}
