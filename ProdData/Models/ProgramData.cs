using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProdData.Models
{
    public class ProgramData
    {
        public ProgramData(BitmapImage? productImage, string programName, string productRelationship, string programCreator)
        {
            ProductImage = productImage;
            ProgramName = programName;
            ProductRelationship = productRelationship;
            ProgramCreator = programCreator;
        }

        public BitmapImage? ProductImage { get; set; }

        public string ProductRelationship { get; set; }
        public string ProgramCreator { get; set; }
        public string ProgramName { get; set; }
        public Rect Dimensions { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public long HistoricalCycles { get; set; }
        public double AverageCycleTime { get; set; }
        public string[] ToolsUsed { get; set; }
        public string[] Attributes { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsShared { get; set; }
    }
}