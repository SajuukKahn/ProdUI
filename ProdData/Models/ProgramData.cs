namespace ProdData.Models
{
    using System;
    using System.Drawing;
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ProgramData" />.
    /// </summary>
    public class ProgramData : BindableBase, IProgramData
    {
        /// <summary>
        /// Defines the _barcode.
        /// </summary>
        private IBarcode? _barcode;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramData"/> class.
        /// </summary>
        /// <param name="barcode">The barcode<see cref="IBarcode?"/>.</param>
        public ProgramData(IBarcode? barcode)
        {
            _barcode = barcode;
        }

        /// <summary>
        /// Gets or sets a value indicating whether AutoStartPlayback.
        /// </summary>
        public bool AutoStartPlayback { get; set; }

        /// <summary>
        /// Gets or sets the AverageCycleTime.
        /// </summary>
        public TimeSpan AverageCycleTime { get; set; }

        /// <summary>
        /// Gets or sets the Barcode.
        /// </summary>
        public IBarcode? Barcode
        {
            get
            {
                return _barcode;
            }

            set
            {
                SetProperty(ref _barcode, value);
            }
        }

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Dimensions.
        /// </summary>
        public Size Dimensions { get; set; }

        /// <summary>
        /// Gets or sets the HistoricalCycles.
        /// </summary>
        public long HistoricalCycles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsFavorite.
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Gets or sets the LastEditDate.
        /// </summary>
        public DateTime LastEditDate { get; set; }

        /// <summary>
        /// Gets or sets the ProductImage.
        /// </summary>
        public BitmapImage? ProductImage { get; set; }

        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the ProgramCreator.
        /// </summary>
        public string? ProgramCreator { get; set; }

        /// <summary>
        /// Gets or sets the ProgramName.
        /// </summary>
        public string? ProgramName { get; set; }

        /// <summary>
        /// Gets or sets the ToolsUsed.
        /// </summary>
        public string[]? ToolsUsed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether UserCanStartPlayback.
        /// </summary>
        public bool UserCanStartPlayback { get; set; }

        /// <summary>
        /// The UpdateAverageCycleTime.
        /// </summary>
        /// <param name="newestCycleTime">The newestCycleTime<see cref="TimeSpan"/>.</param>
        public void UpdateAverageCycleTime(TimeSpan newestCycleTime)
        {
            AverageCycleTime = default(TimeSpan).Add(AverageCycleTime.Add(newestCycleTime)).Divide(2);
        }

        /// <summary>
        /// The UpdateLastEditDate.
        /// </summary>
        public void UpdateLastEditDate()
        {
            LastEditDate = DateTime.Now;
        }
    }
}