using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProductionCore.Concrete;
using ProductionCore.Events;
using System;

namespace ProdTestGenerator.ViewModels
{
    public class TestGeneratorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public TestGeneratorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            StartButton = new DelegateCommand(() => _eventAggregator.GetEvent<StartRequest>().Publish());
            PauseButton = new DelegateCommand(() => _eventAggregator.GetEvent<PauseRequest>().Publish());
            ChangeProgram = new DelegateCommand(() => _eventAggregator.GetEvent<ProgramDataRequest>().Publish(new ProgramData()));
            ChangeProcessImage = new DelegateCommand(() => _eventAggregator.GetEvent<ProductImageChangeRequest>().Publish());
            ThrowCardError = new DelegateCommand(() => _eventAggregator.GetEvent<RaiseError>().Publish());
            ThrowError = new DelegateCommand(() => _eventAggregator.GetEvent<ModalEvent>().Publish(new ModalData() { CanAbort = true, Instructions = "Generic Error Raised!" + Environment.NewLine + "Must Abort Program." }));
        }

        public DelegateCommand ChangeProcessImage { get; set; }

        public DelegateCommand ChangeProgram { get; set; }

        public DelegateCommand PauseButton { get; set; }

        public DelegateCommand RunProgram { get; set; }

        public DelegateCommand StartButton { get; set; }

        public DelegateCommand ThrowError { get; set; }
        public DelegateCommand ThrowCardError { get; set; }
    }
}