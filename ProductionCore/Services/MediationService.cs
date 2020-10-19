namespace ProductionCore.Services
{
    using System;
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
        /// Defines the _pauseComplete.
        /// </summary>
        private bool _pauseComplete;

        /// <summary>
        /// Defines the _beginExecute.
        /// </summary>
        private bool _beginExecute;

        /// <summary>
        /// Defines the _programRequestShow.
        /// </summary>
        private bool _programRequestShow;

        /// <summary>
        /// Defines the _endExecute.
        /// </summary>
        private bool _endExecute;

        /// <summary>
        /// Defines the _currentProgram.
        /// </summary>
        private IProgramData? _currentProgram;

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
        /// Gets or sets a value indicating whether PauseComplete.
        /// </summary>
        public bool PauseComplete
        {
            get
            {
                return _pauseComplete;
            }

            set
            {
                SetProperty(ref _pauseComplete, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether BeginExecute.
        /// </summary>
        public bool BeginExecute
        {
            get
            {
                return _beginExecute;
            }

            set
            {
                SetProperty(ref _beginExecute, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether EndExecute.
        /// </summary>
        public bool EndExecute
        {
            get
            {
                return _endExecute;
            }

            set
            {
                SetProperty(ref _endExecute, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ProgramRequestShow.
        /// </summary>
        public bool ProgramRequestShow
        {
            get
            {
                return _programRequestShow;
            }

            set
            {
                SetProperty(ref _programRequestShow, value);
            }
        }

        /// <summary>
        /// Gets or sets the CurrentProgram.
        /// </summary>
        public IProgramData? CurrentProgram
        {
            get
            {
                return _currentProgram;
            }

            set
            {
                SetProperty(ref _currentProgram, value);
            }
        }

        /// <summary>
        /// The UpdateProgramInformation.
        /// </summary>
        /// <param name="programSuccessful">The programSuccessful<see cref="bool"/>.</param>
        /// <param name="cycleTime">The cycleTime<see cref="IChronometer"/>.</param>
        public void SaveProgram(bool programSuccessful, IChronometer? cycleTime)
        {
            if (!programSuccessful || cycleTime == null)
            {
                return;
            }

            CurrentProgram!.Cycles++;
            CurrentProgram.AverageCycleTime = default(TimeSpan).Add(CurrentProgram.AverageCycleTime.Add(cycleTime.TimeSpan)).Divide(2);
        }
    }
}
