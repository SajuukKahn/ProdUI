using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProdData.Events;
using ProdData.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ProdData.ViewModels
{
    internal class ProgramSelectViewModel : BindableBase
    {
        private bool _canCancel;
        private bool _canConfirm;
        private bool _debugEnabled = true;
        private IEventAggregator _eventAggregator;
        private ProgramData? _oldSelectedProgramData;
        private ObservableCollection<ProgramData> _programList = new ObservableCollection<ProgramData>();
        private bool _programReqestOpen;

        private bool _requestAwaiting = true;
        private ProgramData _selectedProgramData;

        public ProgramSelectViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ProgramNamesResponse>().Subscribe(HandleProgramNamesResponse);
            _eventAggregator.GetEvent<ProgramSelectRequest>().Subscribe(HandleProgramSelectRequest);
            ConfirmButton = new DelegateCommand(ConfirmProgramChange);
            CancelButton = new DelegateCommand(CancelProgramChange);
        }

        public bool CanCancel
        {
            get
            {
                return _canCancel;
            }
            set
            {
                SetProperty(ref _canCancel, value);
            }
        }

        public DelegateCommand CancelButton { get; set; }

        public bool CanConfirm
        {
            get
            {
                return _canConfirm;
            }
            set
            {
                SetProperty(ref _canConfirm, value);
            }
        }

        public DelegateCommand ConfirmButton { get; set; }

        public ObservableCollection<ProgramData> ProgramList
        {
            get
            {
                return _programList;
            }
            set
            {
                SetProperty(ref _programList, value);
            }
        }

        public bool ProgramRequestOpen
        {
            get
            {
                return _programReqestOpen;
            }
            set
            {
                SetProperty(ref _programReqestOpen, value, RequestPrograms);
            }
        }

        public bool RequestAwaiting
        {
            get
            {
                return _requestAwaiting;
            }
            set
            {
                SetProperty(ref _requestAwaiting, value);
            }
        }

        public ProgramData SelectedProgramData
        {
            get
            {
                return _selectedProgramData;
            }
            set
            {
                SetProperty(ref _selectedProgramData, value, SetCanConfirm);
            }
        }

        private void CancelProgramChange()
        {
            DebugLogCaller();
            CleanInstance();
        }

        private void CleanInstance()
        {
            DebugLogCaller();
            CanConfirm = false;
            CanCancel = false;
            ProgramRequestOpen = false;
            _oldSelectedProgramData = null;
            SelectedProgramData = null;
        }

        private void ConfirmProgramChange()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<ProgramSelectResponse>().Publish(SelectedProgramData);
            _eventAggregator.GetEvent<ProgramDataRequest>().Publish(SelectedProgramData);
            CleanInstance();
        }

        private void DebugLogCaller([CallerMemberName] string caller = null)
        {
            if (!_debugEnabled)
            {
                return;
            }
            Debug.WriteLine(this.ToString() + "\t|\t" + caller);
        }

        private void HandleProgramNamesResponse(ObservableCollection<ProgramData> publishedProgramList)
        {
            DebugLogCaller();
            _programList.Clear();
            ProgramList = publishedProgramList;
            RequestAwaiting = false;
        }

        private void HandleProgramSelectRequest(ProgramData oldProgramData)
        {
            DebugLogCaller();
            _oldSelectedProgramData = oldProgramData;
            if (_oldSelectedProgramData != null)
            {
                CanCancel = true;
                RequestAwaiting = false;
            }
            ProgramRequestOpen = true;
        }

        private void RequestPrograms()
        {
            DebugLogCaller();
            if (_programList.Count < 1)
            {
                _eventAggregator.GetEvent<ProgramNamesRequest>().Publish();
            }
        }

        private void SetCanConfirm()
        {
            CanConfirm = true;
        }

        private void VerifyChange()
        {
            DebugLogCaller();
        }
    }
}