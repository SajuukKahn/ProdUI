using Prism.Mvvm;
using ProductionCore.Interfaces;
using System.Collections.Generic;
using System.Windows.Media;

namespace ProductionCore.Concrete
{
    public class Card : BindableBase, ICard
    {
        private bool _breakOnError;

        private int _cardStepIndex;
        private List<CardSubStep> _cardSubSteps = new List<CardSubStep>();

        private Timer _cardTime = new Timer();

        private bool _isActiveStep;

        private bool _stepComplete;
        private ImageSource? _stepImage;
        private ModalData? _stepModalData;

        private StepStatus _stepStatus = StepStatus.Waiting;

        private string? _stepTitle;

        public Card(ImageSource? imageSource = null, bool isActiveStep = false, bool stepComplete = false, string stepTitle = "No Data", StepStatus? stepStatus = null)
        {
            StepImage = imageSource;
            IsActiveStep = isActiveStep;
            StepComplete = stepComplete;
            StepTitle = stepTitle;
            StepStatus = stepStatus ?? StepStatus.Waiting;
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

        public int CardStepIndex
        {
            get
            {
                return _cardStepIndex;
            }
            set
            {
                SetProperty(ref _cardStepIndex, value);
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

        public ModalData? StepModalData
        {
            get
            {
                return _stepModalData;
            }
            set
            {
                SetProperty(ref _stepModalData, value);
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

        public string? StepTitle
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

        public int SubStepCount
        {
            get
            {
                return CardSubSteps.Count;
            }
        }

        public void Initialize()
        {
            StepStatus = StepStatus.Waiting;
            StepComplete = false;
            IsActiveStep = false;
            CardTime.Reset();
            CardStepIndex = 0;
        }

        public override string ToString()
        {
            string s = "";
            s += StepTitle + ",";
            s += CardTime.ElapsedTime + ",";
            s += StepStatus + ",";

            foreach (CardSubStep sub in CardSubSteps)
            {
                s += sub.SubStepName;
                s += "(" + string.Join(' ', sub.SubStepData) + ")";
                s += "  ";
            }

            return s;
        }
    }
}