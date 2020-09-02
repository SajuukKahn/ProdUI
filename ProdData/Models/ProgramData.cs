using Prism.Mvvm;
using ProductionCore.Interfaces;
using System;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ProdData.Models
{
    public class ProgramData : BindableBase, IProgramData
    {
        public bool AutoStartPlayback { get; set; }
        public TimeSpan AverageCycleTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public Size Dimensions { get; set; }
        public long HistoricalCycles { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime LastEditDate { get; set; }
        public BitmapImage? ProductImage { get; set; }
        public string? ProductName { get; set; }
        public string? ProgramCreator { get; set; }
        public string? ProgramName { get; set; }
        public string[]? ToolsUsed { get; set; }
        public bool UserCanStartPlayback { get; set; }

        private IBarcode? _barcode;
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

        public ProgramData(IBarcode? barcode)
        {
            _barcode = barcode;
        }

        public void UpdateAverageCycleTime(TimeSpan newestCycleTime)
        {
            AverageCycleTime = new TimeSpan().Add(AverageCycleTime.Add(newestCycleTime)).Divide(2);
        }

        public void UpdateLastEditDate()
        {
            LastEditDate = DateTime.Now;
        }
    }
}