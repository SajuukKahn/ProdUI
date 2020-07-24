using ProdData.Models;
using System.Collections.Generic;
using System.Windows.Media;

namespace ProdData.Interfaces
{
    internal interface ICard
    {
        public bool BreakOnError { get; set; }
        public List<CardSubStep> CardSubSteps { get; set; }
        public bool IsActiveStep { get; set; }
        public bool StepComplete { get; set; }
        public ImageSource StepImage { get; set; }
        public StepStatus StepStatus { get; set; }
        public string StepTitle { get; set; }
    }
}