﻿namespace ProdTestGenerator.Services
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
        /// Defines the _mediationService.
        /// </summary>
        private readonly IMediationService _mediationService;

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

        public ControllerService(IMediationService mediationService)
        {
            _mediationService = mediationService;
        }

        /// <summary>
        /// The BeginExecution.
        /// </summary>
        public void BeginExecution()
        {
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
        /// The AcceptPause.
        /// </summary>
        private void RespondToPause()
        {
            _mediationService.ExecutionPausedConfirmation();
        }

        /// <summary>
        /// The SendAdvance.
        /// </summary>
        private void SendAdvance()
        {
            _mediationService.AdvanceStep();
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

                        if (!_mediationService.PlaybackPaused)
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(2, 5) + new Random().NextDouble()));
                            if (!_mediationService.PlaybackPaused)
                            {
                                SendAdvance();
                            }
                        }
                    }
                }, _programCancelToken);
        }
    }
}
