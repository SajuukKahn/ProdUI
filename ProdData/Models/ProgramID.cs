using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProdData.Models
{
    public class ProgramID
    {
        public BitmapImage? ProductImage { get; set; }

        public string ProgramName { get; set; }

        public string ProductRelationship { get; set; }

        public string ProgramCreator { get; set; }

        public ProgramID(BitmapImage? productImage, string programName, string productRelationship, string programCreator)
        {
            ProductImage = productImage;
            ProgramName = programName;
            ProductRelationship = productRelationship;
            ProgramCreator = programCreator;
        }
    }
}
