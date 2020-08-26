using Prism.Events;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;

namespace ProdData.Models
{
    public class ModalData
    {
        public ModalData(bool canAbort = true,
                         bool canContinue = false,
                         bool canRetry = false,
                         bool isError = true,
                         string? instructions = null,
                         BitmapImage? instructionImage = null,
                         Card? card = null,
                         RadGlyph? customButtonGlyph = null,
                         string? customButtonText = null)
        {
            CustomButtonGlyph = customButtonGlyph;
            CustomButtonText = customButtonText;
            Instructions = instructions;
            CanAbort = canAbort;
            CanContinue = canContinue;
            CanRetry = canRetry;
            InstructionImage = instructionImage;
            Card = card;
            IsError = isError;
        }
        public bool IsError { get; set; }
        public bool CanAbort { get; set; }
        public bool CanContinue { get; set; }
        public bool CanRetry { get; set; }
        public Card? Card { get; set; }
        public RadGlyph? CustomButtonGlyph { get; set; }
        public string? CustomButtonText { get; set; }
        public BitmapImage? InstructionImage { get; set; }
        public string Instructions { get; set; }
    }
}