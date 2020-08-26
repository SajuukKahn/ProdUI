using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProdData.Models
{
    public class ProgramData
    {
        public TimeSpan AverageCycleTime { get; set; }
        public Barcode Barcode { get; set; }
        public DateTime CreatedDate { get; set; }
        public Size Dimensions { get; set; }
        public long HistoricalCycles { get; set; }
        public DateTime LastEditDate { get; set; }
        public BitmapImage? ProductImage { get; set; }
        public string ProductName { get; set; }
        public string ProgramCreator { get; set; }
        public string ProgramName { get; set; }
        public string[] ToolsUsed { get; set; }
        public bool UserCanStartPlayback { get; set; }
        public bool AutoStartPlayback { get; set; }
        public bool IsFavorite { get; set; }

        public void UpdateAverageCycleTime(TimeSpan newestCycleTime)
        {
            AverageCycleTime = new TimeSpan().Add(AverageCycleTime.Add(newestCycleTime)).Divide(2);
        }

        public void UpdateLastEditeDate()
        {
            LastEditDate = DateTime.Now;
        }
    }
}