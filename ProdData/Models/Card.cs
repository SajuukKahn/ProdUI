namespace ProdData.Models
{
    using System.Collections.Generic;
    using System.Windows.Media;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <inheritdoc/>
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
        public Card(IChronometerFactory chronometerFactory)
        {
            _cardTime = chronometerFactory.Create();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public int SubStepCount
        {
            get
            {
                return CardSubSteps?.Count ?? 0;
            }
        }

        /// <inheritdoc/>
        public void StartCard()
        {
            IsActiveStep = true;
            StepStatus = "Running";
            CardTime.Start();
        }

        /// <inheritdoc/>
        public void RetryCard()
        {
            CardStepIndex = 0;
            CardTime.Reset();
            CardTime.Start();
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            StepStatus = "Waiting";
            StepComplete = false;
            IsActiveStep = false;
            CardTime.Reset();
            CardStepIndex = 0;
        }

        /// <inheritdoc/>
        public bool IterateSubStep()
        {
            if (CardStepIndex < CardSubSteps?.Count - 1)
            {
                CardStepIndex++;
                return true;
            }

            StepStatus = "Completed";
            StepComplete = true;
            IsActiveStep = false;
            CardTime.Pause();
            return false;
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
