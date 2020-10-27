namespace ProdTestGenerator.Services
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ControllerService" />.
    /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether ExecutionPaused.
        /// </summary>
        public bool ExecutionPaused { get; set; }

        /// <summary>
        /// The BeginExecution.
        /// </summary>
        public void BeginExecution()
        {
            ExecutionPaused = false;
            if (_programIsInProgress == false)
            {
                RunProg();
            }
        }

        /// <summary>
        /// The EndExecution.
        /// </summary>
        public void EndExecution()
        {
            Debug.WriteLine("End Execution");
            _programIsInProgress = false;
        }

        /// <summary>
        /// The PauseExecution.
        /// </summary>
        public void PauseExecution()
        {
            ExecutionPaused = true;
        }

        /// <summary>
        /// The RunProg.
        /// </summary>
        private void RunProg()
        {
            int iterate = 0;
            foreach (ICard? card in _playbackService.ProgramSteps)
            {
                iterate += card!.CardSubSteps!.Count;
            }

            Debug.WriteLine(iterate.ToString() + "total substeps");

            _programIsInProgress = true;
            Task.Run(
                () =>
                {
                    int i = 0;
                    Debug.WriteLine("Anonymous Task started");
                    while (EvaluateProgramPosition() && _programIsInProgress)
                    {
                        if (ExecutionPaused && !_pauseComplete && _programIsInProgress)
                        {

                            Application.Current.Dispatcher.Invoke(() => _playbackService.RunningStepPaused());
                            _pauseComplete = true;
                        }

                        if (!ExecutionPaused && _programIsInProgress)
                        {
                            _pauseComplete = false;
                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(2, 5) + new Random().NextDouble()));
                            if (!ExecutionPaused && _programIsInProgress)
                            {
                                Application.Current.Dispatcher.Invoke(() => _playbackService.AdvanceStep());
                                i++;
                            }
                        }
                    }

                    if (_programIsInProgress)
                    {
                        Application.Current.Dispatcher.Invoke(() => _playbackService.AdvanceStep());
                        _programIsInProgress = false;
                    }

                    Debug.WriteLine("Anonymous Task complete");
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

            Debug.WriteLine(i + " | " + j);

            return j != i;
        }
    }
}
