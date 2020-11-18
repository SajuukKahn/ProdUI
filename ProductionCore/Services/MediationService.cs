namespace ProductionCore.Services
{
    using System;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="MediationService" />.
    /// </summary>
    public class MediationService : BindableBase, IMediationService
    {
        /// <summary>
        /// Defines the _programRequestShow.
        /// </summary>
        private bool _programRequestShow;

        /// <summary>
        /// Defines the _currentProgram.
        /// </summary>
        private IProgramData? _currentProgram;

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
                RaisePropertyChanged(nameof(ProgramRequestShow));
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
                RaisePropertyChanged(nameof(CurrentProgram));
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
