using ProdCards.Models;
using System.Collections.Generic;
using System.Windows.Media;

namespace ProdCards.Interfaces
{
    interface IProdCard
    {
        public string StepTitle { get; set; }
        public List<ProdCardSubStepModel> CardSubSteps { get; set; }
        public ProdCardStepStatus StepStatus { get; set; }
        public ImageSource StepImage { get; set; }
        public bool StepComplete { get; set; }
        public bool IsActiveStep { get; set; }
        public bool StepPassed { get; set; }
        public bool BreakOnError { get; set; }
    }
}
