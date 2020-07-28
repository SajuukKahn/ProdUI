using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProdData.Events;
using ProdData.Models;

namespace ProdTestGenerator.ViewModels
{
    public class TestGeneratorViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        public TestGeneratorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            StartButton = new DelegateCommand(() => _eventAggregator.GetEvent<StartRequest>().Publish());
            PauseButton = new DelegateCommand(() => _eventAggregator.GetEvent<PauseRequest>().Publish());
            ChangeProgram = new DelegateCommand(() => _eventAggregator.GetEvent<ProgramDataRequest>().Publish(new ProgramData(null, null, null, null)));
            ChangeProcessImage = new DelegateCommand(() => _eventAggregator.GetEvent<ProductImageChangeRequest>().Publish());
            ThrowError = new DelegateCommand(() => _eventAggregator.GetEvent<RaiseError>().Publish());
        }

        public DelegateCommand ChangeProcessImage { get; set; }

        public DelegateCommand ChangeProgram { get; set; }

        public DelegateCommand GlyphClose { get; set; }

        public DelegateCommand PauseButton { get; set; }

        public DelegateCommand RunProgram { get; set; }

        public DelegateCommand StartButton { get; set; }

        public DelegateCommand ThrowError { get; set; }
    }
}