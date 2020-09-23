namespace ProductionCore.Concrete
{
    using System.Collections.Generic;
    using System.Windows.Media;
    using global::ProductionCore.Interfaces;
    using Prism.Mvvm;

    /// <summary>
    /// Defines the <see cref="Card" />.
    /// </summary>
    public class Card : BindableBase, ICard
    {
        /// <summary>
        /// Defines the _breakOnError.
        /// </summary>
        private bool _breakOnError;

        /// <summary>
        /// Defines the _cardStepIndex.
        /// </summary>
        private int _cardStepIndex;

        /// <summary>
        /// Defines the _cardSubSteps.
        /// </summary>
        private List<CardSubStep> _cardSubSteps = new List<CardSubStep>();

        /// <summary>
        /// Defines the _cardTime.
        /// </summary>
        private Timer _cardTime = new Timer();

        /// <summary>
        /// Defines the _isActiveStep.
        /// </summary>
        private bool _isActiveStep;

        /// <summary>
        /// Defines the _stepComplete.
        /// </summary>
        private bool _stepComplete;

        /// <summary>
        /// Defines the _stepImage.
        /// </summary>
        private ImageSource? _stepImage;

        /// <summary>
        /// Defines the _stepModalData.
        /// </summary>
        private ModalData? _stepModalData;

        /// <summary>
        /// Defines the _currentSubStep.
        /// </summary>
        private CardSubStep? _currentSubStep;

        /// <summary>
        /// Defines the _stepStatus.
        /// </summary>
        private StepStatus _stepStatus = StepStatus.Waiting;

        /// <summary>
        /// Defines the _stepTitle.
        /// </summary>
        private string? _stepTitle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="imageSource">The imageSource<see cref="ImageSource"/>.</param>
        /// <param name="isActiveStep">The isActiveStep<see cref="bool"/>.</param>
        /// <param name="stepComplete">The stepComplete<see cref="bool"/>.</param>
        /// <param name="stepTitle">The stepTitle<see cref="string"/>.</param>
        /// <param name="stepStatus">The stepStatus<see cref="StepStatus"/>.</param>
        public Card(ImageSource? imageSource = null, bool isActiveStep = false, bool stepComplete = false, string stepTitle = "No Data", StepStatus? stepStatus = null)
        {
            StepImage = imageSource;
            IsActiveStep = isActiveStep;
            StepComplete = stepComplete;
            StepTitle = stepTitle;
            StepStatus = stepStatus ?? StepStatus.Waiting;
        }

        /// <summary>
        /// Gets or sets a value indicating whether BreakOnError.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the CurrentSubStep.
        /// </summary>
        public CardSubStep? CurrentSubStep
        {
            get
            {
                return _currentSubStep;
            }

            set
            {
                SetProperty(ref _currentSubStep, value, SetCardStepIndex);
            }
        }

        /// <summary>
        /// Gets or sets the CardStepIndex.
        /// </summary>
        public int CardStepIndex
        {
            get
            {
                return _cardStepIndex;
            }

            set
            {
                SetProperty(ref _cardStepIndex, value, SetCurrentSubstep);
            }
        }

        /// <summary>
        /// Gets or sets the CardSubSteps.
        /// </summary>
        public List<CardSubStep> CardSubSteps
        {
            get
            {
                return _cardSubSteps;
            }

            set
            {
                SetProperty(ref _cardSubSteps, value, SetCardStepIndex);
            }
        }

        /// <summary>
        /// Gets or sets the CardTime.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether IsActiveStep.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether StepComplete.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the StepImage.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the StepModalData.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the StepStatus.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the StepTitle.
        /// </summary>
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

        /// <summary>
        /// Gets the SubStepCount.
        /// </summary>
        public int SubStepCount
        {
            get
            {
                return CardSubSteps.Count;
            }
        }

        /// <summary>
        /// The StartCard.
        /// </summary>
        public void StartCard()
        {
            IsActiveStep = true;
            StepStatus = StepStatus.Running;
            CardTime.Start();
            if (StepModalData?.IsError == false)
            {
                // TODO Need a ModalData Service here to raise any modal interaction required
            }
        }

        /// <summary>
        /// The RetryCard.
        /// </summary>
        public void RetryCard()
        {
            CardStepIndex = 0;
            CardTime.Reset();
            CardTime.Start();
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        public void Initialize()
        {
            StepStatus = StepStatus.Waiting;
            StepComplete = false;
            IsActiveStep = false;
            CardTime.Reset();
            CardStepIndex = 0;
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            string s = string.Empty;
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

        /// <summary>
        /// The SetCurrentSubstep.
        /// </summary>
        private void SetCurrentSubstep()
        {
            CurrentSubStep = _cardSubSteps[_cardStepIndex];
        }

        /// <summary>
        /// The SetCardStepIndex.
        /// </summary>
        private void SetCardStepIndex()
        {
            CardStepIndex = _cardSubSteps.IndexOf(_currentSubStep ?? _cardSubSteps[0]);
        }
    }
}
