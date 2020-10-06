namespace ProdData.Models
{
    using Prism.Mvvm;
    using ProductionCore.Interfaces;
    using System.Collections.Generic;
    using System.Windows.Media;

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
        private List<ICardSubStep?>? _cardSubSteps = new List<ICardSubStep?>();

        /// <summary>
        /// Defines the _cardTime.
        /// </summary>
        private IChronometer _cardTime;

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
        private IModalData? _stepModalData;

        /// <summary>
        /// Defines the _currentSubStep.
        /// </summary>
        private ICardSubStep? _currentSubStep;

        /// <summary>
        /// Defines the _stepStatus.
        /// </summary>
        private string _stepStatus = "Waiting";

        /// <summary>
        /// Defines the _stepTitle.
        /// </summary>
        private string? _stepTitle;


        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="chronometerFactory">The chronometerFactory<see cref="IChronometerFactory"/>.</param>
        /// <param name="cardSubStepFactory">The cardSubStepFactory<see cref="ICardSubStepFactory"/>.</param>
        public Card(IChronometerFactory chronometerFactory)
        {
            _cardTime = chronometerFactory.Create();
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
        public ICardSubStep? CurrentSubStep
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
        public List<ICardSubStep?>? CardSubSteps
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
        public IChronometer CardTime
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
        public IModalData? StepModalData
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
        public string StepStatus
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
                return CardSubSteps?.Count ?? 0;
            }
        }

        /// <summary>
        /// The StartCard.
        /// </summary>
        public void StartCard()
        {
            IsActiveStep = true;
            StepStatus = "Running";
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
            StepStatus = "Waiting";
            StepComplete = false;
            IsActiveStep = false;
            CardTime.Reset();
            CardStepIndex = 0;
        }

        /// <summary>
        /// The SetCurrentSubstep.
        /// </summary>
        private void SetCurrentSubstep()
        {
            if (_cardSubSteps != null)
            {
                CurrentSubStep = _cardSubSteps[_cardStepIndex];
            }
        }

        /// <summary>
        /// The SetCardStepIndex.
        /// </summary>
        private void SetCardStepIndex()
        {
            if (_cardSubSteps != null)
            {
                CardStepIndex = _cardSubSteps.IndexOf(_currentSubStep ?? _cardSubSteps[0]);
            }
            else
            {
                CardStepIndex = 0;
            }
        }
    }
}
