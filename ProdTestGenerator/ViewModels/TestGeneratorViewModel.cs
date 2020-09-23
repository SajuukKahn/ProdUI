namespace ProdTestGenerator.ViewModels
{
    using System;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using ProductionCore.Concrete;
    using ProductionCore.Events;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="TestGeneratorViewModel" />.
    /// </summary>
    public class TestGeneratorViewModel : BindableBase
    {
        /// <summary>
        /// Defines the _eventAggregator.
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestGeneratorViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="programDataFactory">The programDataFactory<see cref="IProgramDataFactory"/>.</param>
        public TestGeneratorViewModel(IEventAggregator eventAggregator, IProgramDataFactory programDataFactory)
        {
            _eventAggregator = eventAggregator;
            StartButton = new DelegateCommand(() => _eventAggregator.GetEvent<StartRequest>().Publish());
            PauseButton = new DelegateCommand(() => _eventAggregator.GetEvent<PauseRequest>().Publish());
            ChangeProgram = new DelegateCommand(() => _eventAggregator.GetEvent<ProgramDataRequest>().Publish(programDataFactory.Create()));
            ChangeProcessImage = new DelegateCommand(() => _eventAggregator.GetEvent<ProductImageChangeRequest>().Publish());
            ThrowCardError = new DelegateCommand(() => _eventAggregator.GetEvent<RaiseError>().Publish());
            ThrowError = new DelegateCommand(() => _eventAggregator.GetEvent<ModalEvent>().Publish(new ModalData() { CanAbort = true, Instructions = "Generic Error Raised!" + Environment.NewLine + "Must Abort Program." }));
        }

        /// <summary>
        /// Gets or sets the ChangeProcessImage.
        /// </summary>
        public DelegateCommand ChangeProcessImage { get; set; }

        /// <summary>
        /// Gets or sets the ChangeProgram.
        /// </summary>
        public DelegateCommand ChangeProgram { get; set; }

        /// <summary>
        /// Gets or sets the PauseButton.
        /// </summary>
        public DelegateCommand PauseButton { get; set; }

        /// <summary>
        /// Gets or sets the StartButton.
        /// </summary>
        public DelegateCommand StartButton { get; set; }

        /// <summary>
        /// Gets or sets the ThrowCardError.
        /// </summary>
        public DelegateCommand ThrowCardError { get; set; }

        /// <summary>
        /// Gets or sets the ThrowError.
        /// </summary>
        public DelegateCommand ThrowError { get; set; }
    }
}
