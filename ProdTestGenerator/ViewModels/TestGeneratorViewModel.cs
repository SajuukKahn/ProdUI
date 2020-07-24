using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ProdData.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

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

        private bool _debugEnabled = true;

        private void DebugLogCaller([CallerMemberName] string caller = null)
        {
            if (!_debugEnabled)
            {
                return;
            }
            Debug.WriteLine(this.ToString() + "\t|\t" + caller);
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
            DebugLogCaller();
            _eventAggregator.GetEvent<ProcessDisplayChangeRequest>().Publish();
        }

        private void ChangeProgramPressed()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<ProgramDataRequest>().Publish();
        }

        private void PauseButtonPressed()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<PauseRequest>().Publish();
        }

        private void StartButtonPressed()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<StartRequest>().Publish();
        }

        private void ThrowErrorPressed()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<RaiseError>().Publish();
        }
    }
}