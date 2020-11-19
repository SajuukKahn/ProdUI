namespace ProdProgramSelect.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
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
        /// Defines the _cycles.
        /// </summary>
        private long _cycles;

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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public long Cycles
        {
            get
            {
                return _cycles;
            }

            set
            {
                SetProperty(ref _cycles, value);
            }
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void UpdateAverageCycleTime(TimeSpan newestCycleTime)
        {
            AverageCycleTime = default(TimeSpan).Add(AverageCycleTime.Add(newestCycleTime)).Divide(2);
        }

        /// <inheritdoc/>
        public void UpdateLastEditDate()
        {
            LastEditDate = DateTime.Now;
        }
    }
}
