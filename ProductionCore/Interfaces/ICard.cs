using ProductionCore.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using Unity;

namespace ProductionCore.Interfaces
{
    public interface ICard
    {
        public bool BreakOnError { get; set; }
        public int CardStepIndex { get; set; }
        public List<CardSubStep> CardSubSteps { get; set; }
        public Timer CardTime { get; set;}
        public bool IsActiveStep { get; set;}
        public bool StepComplete { get; set; }
        public ImageSource? StepImage { get; set; }
        public ModalData? StepModalData { get; set; }
        public StepStatus StepStatus { get; set; }
        public string? StepTitle { get; set; }
        public int SubStepCount { get; }
        public void Initialize();
        public string? ToString();

    }
}
