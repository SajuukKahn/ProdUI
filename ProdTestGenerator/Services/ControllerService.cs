namespace ProdTestGenerator.Services
{
    using System;
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
        /// Defines the _programIsInProgress.
        /// </summary>
        private bool _programIsInProgress;

        /// <summary>
        /// Defines the _programPaused.
        /// </summary>
        private bool _programPaused;

        /// <summary>
        /// Defines the _modalRaised.
        /// </summary>
        private bool _modalRaised;

        /// <summary>
        /// Defines the _pauseRequestResponded.
        /// </summary>
        private bool _pauseRequestResponded;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerService"/> class.
        /// </summary>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        public ControllerService(IPlaybackService playbackService)
        {
            _playbackService = playbackService;
        }

        /// <summary>
        /// The AcceptPause.
        /// </summary>
        public void AcceptPause()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The AcceptPlay.
        /// </summary>
        public void AcceptPlay()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The SendAdvance.
        /// </summary>
        public void SendAdvance()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The SendPause.
        /// </summary>
        public void SendPause()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The SendPlay.
        /// </summary>
        public void SendPlay()
        {
            throw new System.NotImplementedException();
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
                    _programIsInProgress = true;
                    while (true)
                    {
                        if (_programCancelToken.IsCancellationRequested)
                        {
                            _programIsInProgress = false;
                            return;
                        }

                        if (_programPaused && !_pauseRequestResponded)
                        {
                            AcceptPause();
                            _pauseRequestResponded = true;
                        }

                        if (!_programPaused)
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(2, 5) + new Random().NextDouble()));
                            if (!_programPaused && !_modalRaised)
                            {
                                _pauseRequestResponded = false;
                                SendAdvance();
                            }
                        }
                    }
                }, _programCancelToken);
        }
    }
}
