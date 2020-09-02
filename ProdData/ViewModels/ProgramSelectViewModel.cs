using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProductionCore.Events;
using ProductionCore.Interfaces;

namespace ProdData.ViewModels
{
    internal class ProgramSelectViewModel : BindableBase, IProgramSelectViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _canCancel;
        private bool _canConfirm;
        private IProgramData? _oldSelectedProgramData;
        private IProgramCollection? _programList;
        private bool _programReqestOpen = false;
        private bool _requestAwaiting = true;
        private IProgramData? _selectedProgramData;

        public ProgramSelectViewModel(IEventAggregator eventAggregator, IProgramCollection? programCollection)
        {
            _programList = programCollection;
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

        public IProgramCollection? ProgramList
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

        public IProgramData? SelectedProgramData
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
            CleanInstance();
        }

        private void CleanInstance()
        {
            CanConfirm = false;
            CanCancel = false;
            ProgramRequestOpen = false;
            _oldSelectedProgramData = null;
        }

        private void ConfirmProgramChange()
        {
            _eventAggregator.GetEvent<ProgramSelectResponse>().Publish(SelectedProgramData);
            _eventAggregator.GetEvent<ProgramDataRequest>().Publish(SelectedProgramData);
            CleanInstance();
        }

        private void HandleProgramNamesResponse()
        {
            RequestAwaiting = false;
            ProgramList = _programList;
        }

        private void HandleProgramSelectRequest(IProgramData? oldProgramData)
        {
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
            if (_programList?.ProgramList == null || _programList?.ProgramList?.Count < 1)
            {
                _eventAggregator.GetEvent<ProgramNamesRequest>().Publish();
            }
        }

        private void SetCanConfirm()
        {
            CanConfirm = true;
        }
    }
}