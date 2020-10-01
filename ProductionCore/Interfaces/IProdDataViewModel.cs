namespace ProductionCore.Interfaces
{
    using System.Collections.ObjectModel;
    using System.Windows.Media.Imaging;
    using global::ProductionCore.Concrete;

    /// <summary>
    /// Defines the <see cref="IProdDataViewModel" />.
    /// </summary>
    public interface IProdDataViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether AllowProgramChange.
        /// </summary>
        public bool AllowProgramChange { get; set; }

        /// <summary>
        /// Gets or sets the CardCollection.
        /// </summary>
        public ObservableCollection<Card?> CardCollection { get; set; }

        /// <summary>
        /// Gets or sets the CurrentCard.
        /// </summary>
        public Card? CurrentCard { get; set; }

        /// <summary>
        /// Gets or sets the CurrentCardIndex.
        /// </summary>
        public int CurrentCardIndex { get; set; }

        /// <summary>
        /// Gets or sets the CycleTime.
        /// </summary>
        public Chronometer CycleTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PauseAvailable.
        /// </summary>
        public bool PauseAvailable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PlayAvailable.
        /// </summary>
        public bool PlayAvailable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PlayBackRunning.
        /// </summary>
        public bool PlayBackRunning { get; set; }

        /// <summary>
        /// Gets or sets the ProductImage.
        /// </summary>
        public BitmapImage? ProductImage { get; set; }

        /// <summary>
        /// The HandleAdvanceStep.
        /// </summary>
        public void HandleAdvanceStep();

        /// <summary>
        /// The IterateSubStep.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IterateSubStep();

        /// <summary>
        /// The PauseCard.
        /// </summary>
        public void PauseCard();

        /// <summary>
        /// The RetryStep.
        /// </summary>
        public void RetryStep();

        /// <summary>
        /// The StartCard.
        /// </summary>
        public void StartCard();
    }
}
