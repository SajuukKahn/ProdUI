﻿namespace ProdCards.Models
{
    using Prism.Mvvm;
    using System;
    using System.Diagnostics;
    using System.Windows.Threading;

    public class ProdCardTimer : BindableBase
    {
        private DispatcherTimer _timer;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private TimeSpan _timeDifference;

        public ProdCardTimer()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                _timer = new DispatcherTimer();
                _timer.Tick += Timer_Tick;
                _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            }, DispatcherPriority.Normal);
        }

        public void Reset()
        {
            _stopwatch.Reset();
            _timeDifference = new TimeSpan(0);
            TimeLapsed = new TimeSpan(0).ToString("hh\\:mm\\:ss\\.ff");
        }

        public void Pause()
        {
            _stopwatch.Stop();
        }

        public void Start()
        {
            _stopwatch.Start();
            StartTime = DateTime.Now;
        }

        private DateTime? _startTime;
        public DateTime? StartTime
        {
            get { return _startTime; }
            set
            {
                if (!_startTime.HasValue)
                {
                    _startTime = value;
                    _timeDifference = DateTime.Now.Subtract(StartTime.Value);
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        _stopwatch.Start();
                        _timer.Start();
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

        private string _timeLapsed;
        public string TimeLapsed
        {
            get
            {
                return _timeLapsed;
            }
            set
            {
                _timeLapsed = value;
                RaisePropertyChanged(nameof(TimeLapsed));
            }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (_stopwatch.IsRunning)
            {
                TimeSpan total = _timeDifference + _stopwatch.Elapsed;
                TimeLapsed = total.ToString("hh\\:mm\\:ss\\.ff");
                //TimeElapsed = String.Format("{0:0} {1:00}:{2:00}:{3:00}.{4:00}", total.Days, total.Hours, total.Minutes, total.Seconds, total.Milliseconds);
            }
        }
    }
}
