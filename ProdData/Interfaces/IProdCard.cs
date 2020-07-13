using ProdData.Models;
using System.Collections.Generic;
using System.Windows.Media;

namespace ProdData.Interfaces
{
    interface IProdCard
    {
        public string StepTitle { get; set; }
        public List<CardSubStepModel> CardSubSteps { get; set; }
        public CardStepStatus StepStatus { get; set; }
        public ImageSource StepImage { get; set; }
        public bool StepComplete { get; set; }
        public bool IsActiveStep { get; set; }
        public bool StepPassed { get; set; }
        public bool BreakOnError { get; set; }
    }
}
