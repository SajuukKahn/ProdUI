using Prism.Commands;
using Prism.Events;
using ProdData.Events;
using Prism.Mvvm;

namespace ProdTestGenerator.ViewModels
{
    public class TestGeneratorViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        public TestGeneratorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            StartButton = new DelegateCommand(StartButtonPressed);
            PauseButton = new DelegateCommand(PauseButtonPressed);
            ChangeProgram = new DelegateCommand(ChangeProgramPressed);
            ChangeProcessImage = new DelegateCommand(ChangeProcessImagePressed);
            ThrowError = new DelegateCommand(ThrowErrorPressed);
        }

        public DelegateCommand ChangeProcessImage { get; set; }

        public DelegateCommand ChangeProgram { get; set; }

        public DelegateCommand GlyphClose { get; set; }

        public DelegateCommand PauseButton { get; set; }

        public DelegateCommand RunProgram { get; set; }

        public DelegateCommand StartButton { get; set; }

        public DelegateCommand ThrowError { get; set; }

        private void ChangeProcessImagePressed()
        {
            _eventAggregator.GetEvent<ProcessDisplayChangeRequest>().Publish();
        }

        private void ChangeProgramPressed()
        {
            _eventAggregator.GetEvent<ProgramDataRequest>().Publish();
        }

        private void PauseButtonPressed()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
        }

        private void StartButtonPressed()
        {
            _eventAggregator.GetEvent<StartRequest>().Publish();
        }

        private void ThrowErrorPressed()
        {
            _eventAggregator.GetEvent<RaiseError>().Publish();
        }
    }
}