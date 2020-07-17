using ProdData.Interfaces;
using System.Windows.Media.Imaging;

namespace ProdData.Models
{
    public class ProgramID : IIndexAware
    {
        public ProgramID(BitmapImage? productImage, string programName, string productRelationship, string programCreator)
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
        public int Ordinal { get; set; }
    }
}