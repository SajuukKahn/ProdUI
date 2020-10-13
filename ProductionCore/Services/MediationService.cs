namespace ProductionCore.Services
{
    using global::ProductionCore.Interfaces;
    using Prism.Mvvm;

    /// <summary>
    /// Defines the <see cref="MediationService" />.
    /// </summary>
    public class MediationService : BindableBase, IMediationService
    {
        /// <summary>
        /// Defines the _playbackPaused.
        /// </summary>
        private bool _playbackPaused;

        /// <summary>
        /// Gets or sets a value indicating whether PlaybackPaused.
        /// </summary>
        public bool PlaybackPaused
        {
            get
            {
                return _playbackPaused;
            }

            set
            {
                SetProperty(ref _playbackPaused, value);
            }
        }

        /// <summary>
        /// The AdvanceStep.
        /// </summary>
        public void AdvanceStep()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The BeginExecution.
        /// </summary>
        public void BeginExecution()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The EndExecution.
        /// </summary>
        public void EndExecution()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The ExecutionPausedConfirmation.
        /// </summary>
        public void ExecutionPausedConfirmation()
        {
            throw new System.NotImplementedException();
        }
    }
}
