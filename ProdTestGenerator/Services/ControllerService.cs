namespace ProdTestGenerator.Services
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
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
        /// Defines the _programCancellationTokenSource.
        /// </summary>
        private CancellationTokenSource? _programCancellationTokenSource;

        /// <summary>
        /// Defines the _programCancelToken.
        /// </summary>
        private CancellationToken _programCancelToken;

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
            Debug.WriteLine("Begin Execute called");
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
            if (_programCancellationTokenSource != null)
            {
                _programCancellationTokenSource.Cancel();
            }
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
            if (_programCancellationTokenSource == null)
            {
                _programCancellationTokenSource = new CancellationTokenSource();
                _programCancelToken = _programCancellationTokenSource.Token;
            }
            else
            {
                _programCancellationTokenSource.Cancel();
                _programCancellationTokenSource.Dispose();
                _programCancellationTokenSource = new CancellationTokenSource();
                _programCancelToken = _programCancellationTokenSource.Token;
            }

            var task = Task.Run(
                () =>
                {
                    Debug.WriteLine("Task started");
                    _programIsInProgress = true;
                    while (true)
                    {
                        if (_programCancelToken.IsCancellationRequested)
                        {
                            _programIsInProgress = false;
                            return;
                        }

                        if (ExecutionPaused && !_pauseComplete)
                        {
                            _playbackService.RunningStepPaused();
                            _pauseComplete = true;
                            Debug.WriteLine("Pause Complete");
                        }

                        if (!ExecutionPaused)
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(2, 5) + new Random().NextDouble()));
                            if (!ExecutionPaused)
                            {
                                Debug.WriteLine("Send Advance");
                                _playbackService.AdvanceStep();
                            }
                        }
                    }
                }, _programCancelToken);
        }
    }
}
