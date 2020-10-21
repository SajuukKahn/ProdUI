namespace ProductionCore.Interfaces
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="IPlaybackService" />.
    /// </summary>
    public interface IPlaybackService
    {
        /// <summary>
        /// Defines the PauseInitiated.
        /// </summary>
        event Action PauseInitiated;

        /// <summary>
        /// Defines the PlayBackInitiated.
        /// </summary>
        event Action PlaybackInitiated;

        /// <summary>
        /// Defines the HaltInitiated.
        /// </summary>
        event Action HaltInitiated;

        /// <summary>
        /// Gets or sets the ProgramSteps.
        /// </summary>
        ObservableCollection<ICard?> ProgramSteps { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether AllowProgramChange..
        /// </summary>
        bool AllowProgramChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ProgramPaused..
        /// </summary>
        bool ProgramPaused { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PauseAvailable..
        /// </summary>
        bool PauseAvailable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PlayAvailable..
        /// </summary>
        bool PlayAvailable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PlaybackRunning..
        /// </summary>
        bool PlaybackRunning { get; set; }

        /// <summary>
        /// Gets or sets the CurrentCard.
        /// </summary>
        ICard? CurrentCard { get; set; }

        /// <summary>
        /// Gets or sets the CurrentCardIndex.
        /// </summary>
        int CurrentCardIndex { get; set; }

        /// <summary>
        /// Gets or sets the CycleTime.
        /// </summary>
        IChronometer? CycleTime { get; set; }

        /// <summary>
        /// Gets or sets the ProductImage.
        /// </summary>
        BitmapImage? ProductImage { get; set; }

        /// <summary>
        /// The Abort.
        /// </summary>
        void Abort();

        /// <summary>
        /// The AdvanceStep.
        /// </summary>
        void AdvanceStep();

        /// <summary>
        /// The ModalEvent.
        /// </summary>
        /// <param name="modalData">The modalData<see cref="IModalData"/>.</param>
        void ModalEvent(IModalData modalData);

        /// <summary>
        /// The Pause.
        /// </summary>
        void Pause();

        /// <summary>
        /// Defines the RunningStepPaused.
        /// </summary>
        void RunningStepPaused();

        /// <summary>
        /// The Play.
        /// </summary>
        void Play();

        /// <summary>
        /// The ReuqestProgramChange.
        /// </summary>
        void RequestProgramChange();

        /// <summary>
        /// The RaiseError.
        /// </summary>
        void RaiseError();

        /// <summary>
        /// The RetryCard.
        /// </summary>
        void RetryCard();
    }
}
