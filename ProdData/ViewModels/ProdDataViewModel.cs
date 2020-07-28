using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProdData.Events;
using ProdData.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace ProdData.ViewModels
{
    public class ProdDataViewModel : BindableBase
    {
        private bool _allowProgramChange = true;

        private ObservableCollection<Card> _cardCollection = new ObservableCollection<Card>();
        private Card _currentCard;
        private int _currentCardIndex;
        private long _cycleCount;
        private Timer _cycleTime = new Timer();
        private bool _debugEnabled = true;
        private IEventAggregator _eventAggregator;
        private bool _pauseAvailable;
        private bool _playAvailable;
        private bool _playBackRunning;
        private BitmapImage? _productImage;
        private Card _retainedCard;
        private int _retainedSubStep;
        private ProgramData _selectedProgramData;
        private int _subStep;

        public ProdDataViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            PlayButton = new DelegateCommand(PlayPressed).ObservesCanExecute(() => PlayAvailable);
            PauseButton = new DelegateCommand(PausePressed).ObservesCanExecute(() => PauseAvailable);
            OpenProgramSelect = new DelegateCommand(RequestProgramSelect).ObservesCanExecute(() => AllowProgramChange);
            _eventAggregator.GetEvent<ProgramDataResponse>().Subscribe(HandleProgramDataResponse);
            _eventAggregator.GetEvent<StartRequest>().Subscribe(FulfillStartRequest);
            _eventAggregator.GetEvent<ProgramPaused>().Subscribe(PauseCardTimer);
            _eventAggregator.GetEvent<PauseRequest>().Subscribe(FulfillPauseRequest);
            _eventAggregator.GetEvent<ProgramSelectResponse>().Subscribe(HandleProgramSelectResponse);
            _eventAggregator.GetEvent<ProductImageChangeResponse>().Subscribe(HandleProductImageChangeResponse);
            _eventAggregator.GetEvent<RaiseError>().Subscribe(HandleError);
            _eventAggregator.GetEvent<AdvanceStep>().Subscribe(Next);
            AllowProgramChange = true;
        }

        private void RequestProgramSelect()
        {
            _eventAggregator.GetEvent<ProgramSelectRequest>().Publish(SelectedProgramData);
        }

        private void HandleProgramSelectResponse(ProgramData programData)
        {
            SelectedProgramData = programData;
            
        }

        public bool AllowProgramChange
        {
            get
            {
                return _allowProgramChange;
            }
            set
            {
                SetProperty(ref _allowProgramChange, value);
            }
        }

        public DelegateCommand OpenProgramSelect { get; set; }


        public ObservableCollection<Card> CardCollection
        {
            get
            {
                return _cardCollection;
            }
            set
            {
                SetProperty(ref _cardCollection, value);
            }
        }


        public Card CurrentCard
        {
            get
            {
                return _currentCard;
            }
            set
            {
                SetProperty(ref _currentCard, value, RetainStep);
            }
        }

        public int CurrentCardIndex
        {
            get
            {
                return _currentCardIndex;
            }
            set
            {
                SetProperty(ref _currentCardIndex, value);
            }
        }

        public long CycleCount
        {
            get
            {
                return _cycleCount;
            }
            set
            {
                SetProperty(ref _cycleCount, value);
            }
        }

        public Timer CycleTime
        {
            get
            {
                return _cycleTime;
            }
            set
            {
                SetProperty(ref _cycleTime, value);
            }
        }


        public bool PauseAvailable
        {
            get
            {
                return _pauseAvailable;
            }
            set
            {
                SetProperty(ref _pauseAvailable, value);
            }
        }

        public DelegateCommand PauseButton { get; set; }

        public bool PlayAvailable
        {
            get
            {
                return _playAvailable;
            }
            set
            {
                SetProperty(ref _playAvailable, value);
            }
        }

        public bool PlayBackRunning
        {
            get
            {
                return _playBackRunning;
            }
            set
            {
                SetProperty(ref _playBackRunning, value);
            }
        }

        public DelegateCommand PlayButton { get; set; }

        public BitmapImage? ProductImage
        {
            get
            {
                return _productImage;
            }
            set
            {
                SetProperty(ref _productImage, value);
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
                SetProperty(ref _selectedProgramData, value, UpdateProductImage);
            }
        }

        private void UpdateProductImage()
        {
            ProductImage = SelectedProgramData?.ProductImage;
        }

        public int SubStep
        {
            get
            {
                return _subStep;
            }
            set
            {
                SetProperty(ref _subStep, value, RetainSubStep);
            }
        }

        private void PlaybackStart()
        {
            DebugLogCaller();
            PlayAvailable = false;
            PauseAvailable = true;
            StartCardTimer();
            CurrentCard = _retainedCard;
            SubStep = _retainedSubStep;
            CycleTime.Start();
            PlayBackRunning = true;
            AllowProgramChange = false;
        }

        private void PlaybackStop()
        {
            DebugLogCaller();
            PlayBackRunning = false;
            AllowProgramChange = true;
            CycleTime.Reset();
            PlayAvailable = true;
            PauseAvailable = false;
        }

        public void LoadProductionDeck()
        {
            DebugLogCaller();
            RequestCards();
            _retainedCard = CurrentCard;
            _retainedSubStep = SubStep;
        }

        public void Next()
        {
            DebugLogCaller();
            if (SubStep < CurrentCard.CardSubSteps?.Count)
            {
                SubStep++;
            }
            else if (_currentCardIndex < _cardCollection?.Count)
            {
                SubStep = 0;

                CurrentCard.StepStatus = StepStatus.Completed;
                CurrentCard.StepComplete = true;
                CurrentCard.IsActiveStep = false;
                PauseCardTimer();
                CurrentCard = _cardCollection[_cardCollection.IndexOf(CurrentCard) + 1];
                CurrentCard.IsActiveStep = true;
                CurrentCard.StepStatus = StepStatus.Running;
                StartCardTimer();
            }
            else
            {
                SubStep = 0;
                _eventAggregator.GetEvent<PauseRequest>().Publish();

                foreach (var Card in _cardCollection)
                {
                    Card.StepStatus = StepStatus.Waiting;
                    Card.StepComplete = false;
                    Card.IsActiveStep = false;
                }
                CurrentCard = _cardCollection[0];
            }
        }

        private void DebugLogCaller([CallerMemberName] string caller = null)
        {
            if (!_debugEnabled)
            {
                return;
            }
            Debug.WriteLine(this.ToString() + "\t|\t" + caller);
        }

        private void FulfillPauseRequest()
        {
            DebugLogCaller();
            PlayBackRunning = false;
            PlayAvailable = true;
            PauseAvailable = false;
        }

        private void FulfillStartRequest()
        {
            if (PlayBackRunning == false)
            {
                PlaybackStart();
            }
        }

        private void StartCardTimer()
        {
            CurrentCard.CardTime.Start();
        }

        private void HandleError()
        {
            PlaybackStop();
            DebugLogCaller();
        }

        private void HandleProductImageChangeResponse(BitmapImage? image)
        {
            DebugLogCaller();
            ProductImage = image;
        }

        private void HandleProgramDataResponse(ObservableCollection<Card> publishedCardCollection)
        {
            DebugLogCaller();
            _cardCollection.Clear();
            CardCollection = publishedCardCollection;
            CurrentCard = _cardCollection?[0];
        }

        private void PauseCardTimer()
        {
            CurrentCard.CardTime.Pause();
        }

        private void PausePressed()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<PauseRequest>().Publish();
        }

        private void PlayPressed()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<StartRequest>().Publish();
        }

        private void RequestCards()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<ProgramDataRequest>().Publish(SelectedProgramData);
        }

        private void RetainStep()
        {
            DebugLogCaller();
            if (PlayBackRunning)
            {
                CurrentCardIndex = _cardCollection.IndexOf(CurrentCard);
                _retainedCard = CurrentCard;
            }
            SubStep = 0;
        }

        private void RetainSubStep()
        {
            DebugLogCaller();
            if (PlayBackRunning)
            {
                _retainedSubStep = _subStep;
            }
        }

    }
}