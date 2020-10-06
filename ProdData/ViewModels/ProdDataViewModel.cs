namespace ProdData.ViewModels
{
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using ProductionCore.Concrete;
    using ProductionCore.Events;
    using ProductionCore.Interfaces;
    using System.Collections.ObjectModel;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="ProdDataViewModel" />.
    /// </summary>
    public class ProdDataViewModel : BindableBase, IProdDataViewModel
    {
        /// <summary>
        /// Defines the _eventAggregator.
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Defines the _programDataService.
        /// </summary>
        private readonly IProgramDataService _programDataService;

        /// <summary>
        /// Defines the _playbackService.
        /// </summary>
        private readonly IPlaybackService _playbackService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdDataViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="programDataService">The programDataService<see cref="IProgramDataService"/>.</param>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        public ProdDataViewModel(IEventAggregator eventAggregator, IProgramDataService programDataService, IPlaybackService playbackService)
        {
            _eventAggregator = eventAggregator;
            _programDataService = programDataService;
            _playbackService = playbackService;
            PlayButton = new DelegateCommand(() => PlaybackService.Play()).ObservesCanExecute(() => PlaybackService.PlayAvailable);
            PauseButton = new DelegateCommand(() => PlaybackService.Pause()).ObservesCanExecute(() => PlaybackService.PauseAvailable);
            OpenProgramSelect = new DelegateCommand(() => { ProgramDataService.ProgramRequestShow = true; }).ObservesCanExecute(() => AllowProgramChange);
            ////_eventAggregator.GetEvent<ProgramDataResponse>().Subscribe(HandleProgramDataResponse);
            _eventAggregator.GetEvent<StartRequest>().Subscribe(HandleStartRequest);
            _eventAggregator.GetEvent<ProgramPaused>().Subscribe(HandlePauseConfirmation);
            _eventAggregator.GetEvent<PauseRequest>().Subscribe(HandlePauseRequest);
            _eventAggregator.GetEvent<ProgramDataSaveResponse>().Subscribe(HandleProgramDataSaveResponse);
            _eventAggregator.GetEvent<ProductImageChangeResponse>().Subscribe(HandleProductImageChangeResponse);
            _eventAggregator.GetEvent<RaiseError>().Subscribe(HandleRaiseError);
            ////_eventAggregator.GetEvent<AdvanceStep>().Subscribe(HandleAdvanceStep);
            _eventAggregator.GetEvent<ProgramHaltRequest>().Subscribe(HandleProgramHaltRequest);
            _eventAggregator.GetEvent<ModalResponse>().Subscribe(HandleModalAbortRequest, ThreadOption.BackgroundThread, false, modalData => modalData.Equals(ModalResponseData.Abort));
            _eventAggregator.GetEvent<ModalResponse>().Subscribe(HandleModalContinueRequest, ThreadOption.BackgroundThread, false, modalData => modalData.Equals(ModalResponseData.Continue));
            _eventAggregator.GetEvent<ModalResponse>().Subscribe(HandleModalRetryRequest, ThreadOption.BackgroundThread, false, modalData => modalData.Equals(ModalResponseData.Retry));
        }

        /// <summary>
        /// Gets the ProgramDataService.
        /// </summary>
        public IProgramDataService ProgramDataService
        {
            get
            {
                return _programDataService;
            }
        }

        /// <summary>
        /// Gets the PlaybackService.
        /// </summary>
        public IPlaybackService PlaybackService
        {
            get
            {
                return _playbackService;
            }
        }

        /// <summary>
        /// Gets or sets the OpenProgramSelect.
        /// </summary>
        public DelegateCommand OpenProgramSelect { get; set; }

        /// <summary>
        /// Gets or sets the PauseButton.
        /// </summary>
        public DelegateCommand PauseButton { get; set; }

        /// <summary>
        /// Gets or sets the PlayButton.
        /// </summary>
        public DelegateCommand PlayButton { get; set; }

        /// <summary>
        /// The HandleModalAbortRequest.
        /// </summary>
        /// <param name="obj">The obj<see cref="ModalResponseData"/>.</param>
        private void HandleModalAbortRequest(ModalResponseData obj)
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
            _eventAggregator.GetEvent<ProgramDataSaveRequest>().Publish();
        }

        /// <summary>
        /// The HandleModalContinueRequest.
        /// </summary>
        /// <param name="obj">The obj<see cref="ModalResponseData"/>.</param>
        private void HandleModalContinueRequest(ModalResponseData obj)
        {
            // PlaybackStart();
            IterateSubStep();
        }

        /// <summary>
        /// The HandleModalCustomRequest.
        /// </summary>
        private void HandleModalCustomRequest()
        {
        }

        /// <summary>
        /// The HandleModalRetryRequest.
        /// </summary>
        /// <param name="obj">The obj<see cref="ModalResponseData"/>.</param>
        private void HandleModalRetryRequest(ModalResponseData obj)
        {
            PlaybackStart();
            RetryStep();
        }

        /// <summary>
        /// The HandlePauseConfirmation.
        /// </summary>
        private void HandlePauseConfirmation()
        {
            PauseCard();
        }

        /// <summary>
        /// The HandlePauseRequest.
        /// </summary>
        private void HandlePauseRequest()
        {
            PlayBackRunning = false;
            PlayAvailable = true;
            PauseAvailable = false;
            AllowProgramChange = true;
        }

        /// <summary>
        /// The HandleProductImageChangeResponse.
        /// </summary>
        /// <param name="image">The image<see cref="BitmapImage"/>.</param>
        private void HandleProductImageChangeResponse(BitmapImage? image)
        {
            ProductImage = image;
        }

        /// <summary>
        /// The HandleProgramDataResponse.
        /// </summary>
        /// <param name="publishedCardCollection">The publishedCardCollection<see cref="ObservableCollection{Card}"/>.</param>
        private void HandleProgramDataResponse(ObservableCollection<Card?> publishedCardCollection)
        {

        }

        /// <summary>
        /// The HandleProgramDataSaveResponse.
        /// </summary>
        private void HandleProgramDataSaveResponse()
        {

        }

        /// <summary>
        /// The HandleProgramHaltRequest.
        /// </summary>
        private void HandleProgramHaltRequest()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
        }

        /// <summary>
        /// The HandleRaiseError.
        /// </summary>
        private void HandleRaiseError()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
            if (_cardCollection[CurrentCardIndex]!.StepModalData == null)
            {
                _eventAggregator.GetEvent<ModalEvent>().Publish(new ModalData()
                {
                    CanAbort = true,
                    IsError = true,
                    Card = CurrentCard,
                });
            }
            else
            {
                _eventAggregator.GetEvent<ModalEvent>().Publish(_cardCollection[CurrentCardIndex]!.StepModalData!);
            }
        }

        /// <summary>
        /// The HandleStartRequest.
        /// </summary>
        private void HandleStartRequest()
        {
            if (PlayBackRunning == false && _programDataService.SelectedProgramData != null)
            {
                PlaybackStart();
            }
        }

        /// <summary>
        /// The PausePressed.
        /// </summary>
        private void PausePressed()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
        }

        /// <summary>
        /// The PlaybackStart.
        /// </summary>
        private void PlaybackStart()
        {
            PlayAvailable = false;
            PauseAvailable = true;
            CycleTime.Start();
            StartCard();
            PlayBackRunning = true;
            AllowProgramChange = false;
        }
    }
}
