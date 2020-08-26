using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace ProdData.Models
{
    public class Timer : BindableBase
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private TimeSpan _timeSpan = new TimeSpan();
        private string _elapsedTime;

        private DateTime? _startTime;

        private TimeSpan _timeDifference;

        public Timer()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                _dispatcherTimer.Tick += Timer_Tick;
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            }, DispatcherPriority.Normal);
        }

        public string ElapsedTime
        {
            get
            {
                return _elapsedTime;
            }
            set
            {
                SetProperty(ref _elapsedTime, value);
                //_elapsedTime = value;
                //RaisePropertyChanged(nameof(ElapsedTime));
            }
        }

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
                    _timeDifference = DateTime.Now.Subtract(StartTime.Value);
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
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
                    _timeDifference = DateTime.Now.Subtract(StartTime.Value);
                }
                RaisePropertyChanged(nameof(StartTime));
            }
        }

        public void Pause()
        {
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
            _timeDifference = new TimeSpan(0);
            ElapsedTime = new TimeSpan(0).ToString("hh\\:mm\\:ss\\.ff");
        }

        public void Start()
        {
            _stopwatch.Start();
            StartTime = DateTime.Now;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_stopwatch.IsRunning)
            {
                _timeSpan = _timeDifference + _stopwatch.Elapsed;
                ElapsedTime = _timeSpan.ToString("hh\\:mm\\:ss\\.ff");
                //TimeElapsed = String.Format("{0:0} {1:00}:{2:00}:{3:00}.{4:00}", total.Days, total.Hours, total.Minutes, total.Seconds, total.Milliseconds);
            }
        }
    }
}