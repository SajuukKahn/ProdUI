namespace ProdData.Models
{
    using System;
    using System.Diagnostics;
    using System.Windows.Threading;
    using Prism.Mvvm;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
    public class Chronometer : BindableBase, IChronometer
    {
        /// <summary>
        /// Defines the _stopwatch.
        /// </summary>
        private readonly Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// Defines the _dispatcherTimer.
        /// </summary>
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        /// <summary>
        /// Defines the _timeSpan.
        /// </summary>
        private TimeSpan _timeSpan = default;

        /// <summary>
        /// Defines the _elapsedTime.
        /// </summary>
        private string? _elapsedTime;

        /// <summary>
        /// Defines the _startTime.
        /// </summary>
        private DateTime? _startTime;

        /// <summary>
        /// Defines the _timeDifference.
        /// </summary>
        private TimeSpan _timeDifference;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chronometer"/> class.
        /// </summary>
        public Chronometer()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                () =>
            {
                _dispatcherTimer.Tick += Timer_Tick;
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            }, DispatcherPriority.Normal);
        }

        /// <inheritdoc/>
        public string? ElapsedTime
        {
            get
            {
                return _elapsedTime;
            }

            set
            {
                SetProperty(ref _elapsedTime, value);
            }
        }

        /// <inheritdoc/>
        public TimeSpan TimeSpan
        {
            get
            {
                return _timeSpan;
            }

            set
            {
                SetProperty(ref _timeSpan, value);
            }
        }

        /// <inheritdoc/>
        public DateTime? StartTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                if (!_startTime.HasValue)
                {
                    _startTime = value;
                    _timeDifference = DateTime.Now.Subtract(_startTime!.Value);
                    System.Windows.Application.Current.Dispatcher.Invoke(
                        () =>
                    {
                        _stopwatch.Start();
                        _dispatcherTimer.Start();
                    }, DispatcherPriority.Normal);
                }
                else if (_startTime.HasValue && value == null)
                {
                    _stopwatch.Stop();
                    _startTime = value;
                }
                else if (_startTime.HasValue && value != null)
                {
                    _startTime = value;
                    _timeDifference = DateTime.Now.Subtract(_startTime!.Value);
                }

                RaisePropertyChanged(nameof(StartTime));
            }
        }

        /// <inheritdoc/>
        public void Pause()
        {
            _stopwatch.Stop();
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _stopwatch.Reset();
            _timeDifference = new TimeSpan(0);
            ElapsedTime = new TimeSpan(0).ToString("hh\\:mm\\:ss\\.ff");
        }

        /// <inheritdoc/>
        public void Start()
        {
            _stopwatch.Start();
            StartTime = DateTime.Now;
        }

        /// <summary>
        /// The Timer_Tick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_stopwatch.IsRunning)
            {
                _timeSpan = _timeDifference + _stopwatch.Elapsed;
                ElapsedTime = _timeSpan.ToString("hh\\:mm\\:ss\\.ff");
            }
        }
    }
}
