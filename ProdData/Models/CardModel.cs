using ProdData.Interfaces;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Media;

namespace ProdData.Models
{
    public class CardModel : BindableBase, IProdCard
    {
        public CardModel(ImageSource imageSource = null,
                                    bool isActiveStep = false,
                                    bool stepComplete = false,
                                    string stepTitle = "No Data")
        {
            StepImage = imageSource;
            IsActiveStep = isActiveStep;
            StepComplete = stepComplete;
            StepTitle = stepTitle;
            StepStatus = CardStepStatus.Waiting;
        }

        private List<CardSubStepModel> _cardSubSteps = new List<CardSubStepModel>();
        public List<CardSubStepModel> CardSubSteps
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
        
        private Timer _cardTime = new Timer();
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

        private CardStepStatus _stepStatus;
        public CardStepStatus StepStatus
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

        private string _stepTitle;
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

        private ImageSource _stepImage;
        public ImageSource StepImage
        {
            get
            {
                return _stepImage;
            }
            set
            {
                SetProperty(ref _stepImage , value);
            }
        }

        private bool _stepComplete;
        public bool StepComplete
        {
            get
            {
                return _stepComplete;
            }
            set
            {
                SetProperty(ref _stepComplete , value);
            }
        }

        private bool _isActiveStep;
        public bool IsActiveStep
        {
            get
            {
                return _isActiveStep;
            }
            set
            {
                SetProperty(ref _isActiveStep , value);
            }
        }

        private bool _stepPassed;
        public bool StepPassed
        {
            get
            {
                return _stepPassed;
            }
            set
            {
                SetProperty(ref _stepPassed , value);
            }
        }

        private bool _breakOnError;
        public bool BreakOnError
        {
            get
            {
                return _breakOnError;
            }
            set
            {
                SetProperty(ref _breakOnError , value);
            }
        }
    }
}
