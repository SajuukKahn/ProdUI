using Prism.Mvvm;
using ProdData.Interfaces;
using System.Collections.Generic;
using System.Windows.Media;

namespace ProdData.Models
{
    public class Card : BindableBase, ICard
    {
        private bool _breakOnError;

        private List<CardSubStep> _cardSubSteps = new List<CardSubStep>();

        private Timer _cardTime = new Timer();

        private bool _isActiveStep;

        private bool _stepComplete;

        private ImageSource _stepImage;

        private bool _stepPassed;

        private StepStatus _stepStatus;

        private string _stepTitle;

        public Card(ImageSource? imageSource = null, bool isActiveStep = false, bool stepComplete = false, string stepTitle = "No Data")
        {
            StepImage = imageSource;
            IsActiveStep = isActiveStep;
            StepComplete = stepComplete;
            StepTitle = stepTitle;
            StepStatus = StepStatus.Waiting;
        }

        public bool BreakOnError
        {
            get
            {
                return _breakOnError;
            }
            set
            {
                SetProperty(ref _breakOnError, value);
            }
        }

        public List<CardSubStep> CardSubSteps
        {
            get
            {
                return _cardSubSteps;
            }
            set
            {
                SetProperty(ref _cardSubSteps, value);
            }
        }

        public Timer CardTime
        {
            get
            {
                return _cardTime;
            }
            set
            {
                SetProperty(ref _cardTime, value);
            }
        }

        public bool IsActiveStep
        {
            get
            {
                return _isActiveStep;
            }
            set
            {
                SetProperty(ref _isActiveStep, value);
            }
        }

        public bool StepComplete
        {
            get
            {
                return _stepComplete;
            }
            set
            {
                SetProperty(ref _stepComplete, value);
            }
        }

        public ImageSource? StepImage
        {
            get
            {
                return _stepImage;
            }
            set
            {
                SetProperty(ref _stepImage, value);
            }
        }

        public bool StepPassed
        {
            get
            {
                return _stepPassed;
            }
            set
            {
                SetProperty(ref _stepPassed, value);
            }
        }

        public StepStatus StepStatus
        {
            get
            {
                return _stepStatus;
            }
            set
            {
                SetProperty(ref _stepStatus, value);
            }
        }

        public string StepTitle
        {
            get
            {
                return _stepTitle;
            }
            set
            {
                SetProperty(ref _stepTitle, value);
            }
        }
    }
}