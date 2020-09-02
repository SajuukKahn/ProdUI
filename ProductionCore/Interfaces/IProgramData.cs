using System;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ProductionCore.Interfaces
{
    public interface IProgramData
    {
        bool AutoStartPlayback { get; set; }
        TimeSpan AverageCycleTime { get; set; }
        IBarcode? Barcode { get; set; }
        DateTime CreatedDate { get; set; }
        Size Dimensions { get; set; }
        long HistoricalCycles { get; set; }
        bool IsFavorite { get; set; }
        DateTime LastEditDate { get; set; }
        BitmapImage? ProductImage { get; set; }
        string? ProductName { get; set; }
        string? ProgramCreator { get; set; }
        string? ProgramName { get; set; }
        string[]? ToolsUsed { get; set; }
        bool UserCanStartPlayback { get; set; }

        void UpdateAverageCycleTime(TimeSpan newestCycleTime);

        void UpdateLastEditDate();
    }
}