namespace ProdTestGenerator.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
    public class ControllerService : IControllerService
    {
        /// <summary>
        /// Defines the _playbackService.
        /// </summary>
        private readonly IPlaybackService _playbackService;

        /// <summary>
        /// Defines the _pauseComplete.
        /// </summary>
        private bool _pauseComplete;

        /// <summary>
        /// Defines the _programIsInProgress.
        /// </summary>
        private bool _programIsInProgress;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerService"/> class.
        /// </summary>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        public ControllerService(IPlaybackService playbackService)
        {
            _playbackService = playbackService;
            _playbackService.AbortInitiated += new Action(EndExecution);
            _playbackService.PauseInitiated += new Action(PauseExecution);
            _playbackService.PlaybackInitiated += new Action(BeginExecution);
        }

        /// <inheritdoc/>
        public bool ExecutionPaused { get; set; }

        /// <inheritdoc/>
        public void BeginExecution()
        {
            ExecutionPaused = false;
            if (_programIsInProgress == false)
            {
                RunProg();
            }
        }

        /// <inheritdoc/>
        public void EndExecution()
        {
            _programIsInProgress = false;
        }

        /// <inheritdoc/>
        public void PauseExecution()
        {
            ExecutionPaused = true;
        }

        /// <summary>
        /// The RunProg.
        /// </summary>
        private void RunProg()
        {
            _programIsInProgress = true;
            Task.Run(
                () =>
                {
                    while (EvaluateProgramPosition() && _programIsInProgress)
                    {
                        if (ExecutionPaused && !_pauseComplete && _programIsInProgress)
                        {
                            Application.Current.Dispatcher.Invoke(() => _playbackService.RunningStepPaused());
                            _pauseComplete = true;
                        }

                        if (!ExecutionPaused && _programIsInProgress)
                        {
                            if (!_pauseComplete)
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(2, 5) + new Random().NextDouble()));
                            }

                            _pauseComplete = false;
                            if (!ExecutionPaused && _programIsInProgress)
                            {
                                Application.Current.Dispatcher.Invoke(() => _playbackService.AdvanceStep());
                            }
                        }
                    }

                    if (_programIsInProgress)
                    {
                        Application.Current.Dispatcher.Invoke(() => _playbackService.AdvanceStep());
                        _programIsInProgress = false;
                    }
                });
        }

        private bool EvaluateProgramPosition()
        {
            int i = 0, j = 0;
            foreach (ICard? card in _playbackService.ProgramSteps)
            {
                foreach (ICardSubStep? subStep in card!.CardSubSteps!)
                {
                    i++;
                    if (card! == _playbackService.CurrentCard && subStep! == _playbackService.CurrentCard!.CardSubSteps![_playbackService.CurrentCard!.CardStepIndex])
                    {
                        j = i;
                    }
                }
            }

            return j != i;
        }
    }
}
