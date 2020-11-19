namespace ProdCore.Interfaces
{
    using System.Collections.Generic;
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="ICard" />.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Gets or sets a value indicating whether BreakOnError.
        /// </summary>
        public bool BreakOnError { get; set; }

        /// <summary>
        /// Gets or sets the CurrentSubStep.
        /// </summary>
        public ICardSubStep? CurrentSubStep { get; set; }

        /// <summary>
        /// Gets or sets the CardStepIndex.
        /// </summary>
        public int CardStepIndex { get; set; }

        /// <summary>
        /// Gets or sets the CardSubSteps.
        /// </summary>
        public List<ICardSubStep?>? CardSubSteps { get; set; }

        /// <summary>
        /// Gets or sets the CardTime.
        /// </summary>
        public IChronometer CardTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsActiveStep.
        /// </summary>
        public bool IsActiveStep { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether StepComplete.
        /// </summary>
        public bool StepComplete { get; set; }

        /// <summary>
        /// Gets or sets the StepImage.
        /// </summary>
        public ImageSource? StepImage { get; set; }

        /// <summary>
        /// Gets or sets the StepModalData.
        /// </summary>
        public IModalData? StepModalData { get; set; }

        /// <summary>
        /// Gets or sets the StepStatus.
        /// </summary>
        public string StepStatus { get; set; }

        /// <summary>
        /// Gets or sets the StepTitle.
        /// </summary>
        public string? StepTitle { get; set; }

        /// <summary>
        /// Gets the SubStepCount.
        /// </summary>
        public int SubStepCount { get; }

        /// <summary>
        /// The StartCard.
        /// </summary>
        void StartCard();

        /// <summary>
        /// The RetryCard.
        /// </summary>
        void RetryCard();

        /// <summary>
        /// The Initialize.
        /// </summary>
        public void Initialize();

        /// <summary>
        /// The IterateSubStep.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IterateSubStep();
    }
}
