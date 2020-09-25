namespace ProdProgramSelect.Models
{
    using System;
    using System.Collections.ObjectModel;
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
        /// Defines the _autoStartPlayback.
        /// </summary>
        private bool _autoStartPlayback;

        /// <summary>
        /// Defines the _averageCycleTime.
        /// </summary>
        private TimeSpan _averageCycleTime;

        /// <summary>
        /// Defines the _createdDate.
        /// </summary>
        private DateTime _createdDate;

        /// <summary>
        /// Defines the _dimensions.
        /// </summary>
        private Size _dimensions;

        /// <summary>
        /// Defines the _historicalCycles.
        /// </summary>
        private long _historicalCycles;

        /// <summary>
        /// Defines the _isFavorite.
        /// </summary>
        private bool _isFavorite;

        /// <summary>
        /// Defines the _lastEditDate.
        /// </summary>
        private DateTime _lastEditDate;

        /// <summary>
        /// Defines the _productImage.
        /// </summary>
        private BitmapImage? _productImage;

        /// <summary>
        /// Defines the _productName.
        /// </summary>
        private string? _productName;

        /// <summary>
        /// Defines the _programCreator.
        /// </summary>
        private string? _programCreator;

        /// <summary>
        /// Defines the _programName.
        /// </summary>
        private string? _programName;

        /// <summary>
        /// Defines the _toolsUsed.
        /// </summary>
        private ObservableCollection<string> _toolsUsed = new ObservableCollection<string>();

        /// <summary>
        /// Defines the _userCanStartPlayback.
        /// </summary>
        private bool _userCanStartPlayback;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramData"/> class.
        /// </summary>
        /// <param name="barcode">The barcode<see cref="IBarcode"/>.</param>
        public ProgramData(IBarcode? barcode)
        {
            _barcode = barcode;
        }

        /// <summary>
        /// Gets or sets a value indicating whether AutoStartPlayback.
        /// </summary>
        public bool AutoStartPlayback
        {
            get
            {
                return _autoStartPlayback;
            }

            set
            {
                SetProperty(ref _autoStartPlayback, value);
            }
        }

        /// <summary>
        /// Gets or sets the AverageCycleTime.
        /// </summary>
        public TimeSpan AverageCycleTime
        {
            get
            {
                return _averageCycleTime;
            }

            set
            {
                SetProperty(ref _averageCycleTime, value);
            }
        }

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
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }

            set
            {
                SetProperty(ref _createdDate, value);
            }
        }

        /// <summary>
        /// Gets or sets the Dimensions.
        /// </summary>
        public Size Dimensions
        {
            get
            {
                return _dimensions;
            }

            set
            {
                SetProperty(ref _dimensions, value);
            }
        }

        /// <summary>
        /// Gets or sets the HistoricalCycles.
        /// </summary>
        public long HistoricalCycles
        {
            get
            {
                return _historicalCycles;
            }

            set
            {
                SetProperty(ref _historicalCycles, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsFavorite.
        /// </summary>
        public bool IsFavorite
        {
            get
            {
                return _isFavorite;
            }

            set
            {
                SetProperty(ref _isFavorite, value);
            }
        }

        /// <summary>
        /// Gets or sets the LastEditDate.
        /// </summary>
        public DateTime LastEditDate
        {
            get
            {
                return _lastEditDate;
            }

            set
            {
                SetProperty(ref _lastEditDate, value);
            }
        }

        /// <summary>
        /// Gets or sets the ProductImage.
        /// </summary>
        public BitmapImage? ProductImage
        {
            get
            {
                return _productImage;
            }

            set
            {
                SetProperty(ref _productImage, value);
            }
        }

        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        public string? ProductName
        {
            get
            {
                return _productName;
            }

            set
            {
                SetProperty(ref _productName, value);
            }
        }

        /// <summary>
        /// Gets or sets the ProgramCreator.
        /// </summary>
        public string? ProgramCreator
        {
            get
            {
                return _programCreator;
            }

            set
            {
                SetProperty(ref _programCreator, value);
            }
        }

        /// <summary>
        /// Gets or sets the ProgramName.
        /// </summary>
        public string? ProgramName
        {
            get
            {
                return _programName;
            }

            set
            {
                SetProperty(ref _programName, value);
            }
        }

        /// <summary>
        /// Gets or sets the ToolsUsed.
        /// </summary>
        public ObservableCollection<string> ToolsUsed
        {
            get
            {
                return _toolsUsed;
            }

            set
            {
                SetProperty(ref _toolsUsed, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether UserCanStartPlayback.
        /// </summary>
        public bool UserCanStartPlayback
        {
            get
            {
                return _userCanStartPlayback;
            }

            set
            {
                SetProperty(ref _userCanStartPlayback, value);
            }
        }

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
