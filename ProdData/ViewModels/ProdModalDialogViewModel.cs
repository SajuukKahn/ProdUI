using Prism.Events;
using Prism.Mvvm;
using ProductionCore.Events;
using ProductionCore.Concrete;
using System;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;

namespace ProdData.ViewModels
{
    internal class ProdModalDialogViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _canAbort;
        private bool _canContinue;
        private bool _canCustom;
        private bool _canRetry;
        private RadGlyph? _customGlyph;
        private string? _customText;
        private BitmapImage? _imageSource;
        private string? _modalInstructions = "An error was thrown";
        private bool _modalOpenRequested;

        public ProdModalDialogViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ModalEvent>().Subscribe(HandleModalRequest);
            DelegateAbort = new Prism.Commands.DelegateCommand(RaiseAbort).ObservesCanExecute(() => CanAbort);
            DelegateContinue = new Prism.Commands.DelegateCommand(RaiseContinue).ObservesCanExecute(() => CanContinue);
            DelegateRetry = new Prism.Commands.DelegateCommand(RaiseRetry).ObservesCanExecute(() => CanRetry);
            DelegateCustom = new Prism.Commands.DelegateCommand(RaiseCustom).ObservesCanExecute(() => CanCustom);
        }

        public bool CanAbort
        {
            get
            {
                return _canAbort;
            }
            set
            {
                SetProperty(ref _canAbort, value);
            }
        }

        public bool CanContinue
        {
            get
            {
                return _canContinue;
            }
            set
            {
                SetProperty(ref _canContinue, value);
            }
        }

        public bool CanCustom
        {
            get
            {
                return _canCustom;
            }
            set
            {
                SetProperty(ref _canCustom, value);
            }
        }

        public bool CanRetry
        {
            get
            {
                return _canRetry;
            }
            set
            {
                SetProperty(ref _canRetry, value);
            }
        }

        public RadGlyph? CustomGlyph
        {
            get
            {
                return _customGlyph;
            }
            set
            {
                SetProperty(ref _customGlyph, value);
            }
        }

        public string? CustomText
        {
            get
            {
                return _customText;
            }
            set
            {
                SetProperty(ref _customText, value);
            }
        }

        public Prism.Commands.DelegateCommand DelegateAbort { get; set; }

        public Prism.Commands.DelegateCommand DelegateContinue { get; set; }

        public Prism.Commands.DelegateCommand DelegateCustom { get; set; }

        public Prism.Commands.DelegateCommand DelegateRetry { get; set; }

        public BitmapImage? ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                SetProperty(ref _imageSource, value);
            }
        }

        public string? ModalInstructions
        {
            get
            {
                return _modalInstructions;
            }
            set
            {
                SetProperty(ref _modalInstructions, value);
            }
        }

        public bool ModalOpenRequested
        {
            get
            {
                return _modalOpenRequested;
            }
            set
            {
                SetProperty(ref _modalOpenRequested, value);
            }
        }

        private void HandleModalRequest(ModalData modalData)
        {
            if(modalData == null)
            {
                return;
            }
            ModalOpenRequested = true;
            if(modalData.IsError)
            {
                _eventAggregator.GetEvent<ProgramHaltRequest>().Publish();
            }
            if (modalData.Card != null)
            {
                ModalInstructions = "The Step \"" + modalData.Card.StepTitle + "\" Failed at step #" + (modalData.Card.CardStepIndex + 1).ToString()
                    + Environment.NewLine + "Select an action below:";
                ImageSource = (BitmapImage)modalData.Card.StepImage! ?? null;
            }
            else
            {
                ImageSource = modalData.InstructionImage;
                ModalInstructions = modalData.Instructions;
            }

            if (modalData.CustomButtonGlyph != null && modalData.CustomButtonText != null)
            {
                CanCustom = true;
                CustomGlyph = modalData.CustomButtonGlyph;
                CustomGlyph = new RadGlyph() { Glyph = "&#xe50f" };
                CustomText = modalData.CustomButtonText;
            }
            CanAbort = modalData.CanAbort;
            CanRetry = modalData.CanRetry;
            CanContinue = modalData.CanContinue;
        }

        private void RaiseAbort()
        {
            _eventAggregator.GetEvent<ModalResponse>().Publish(ModalResponseData.Abort);
            ModalOpenRequested = false;
        }

        private void RaiseContinue()
        {
            _eventAggregator.GetEvent<ModalResponse>().Publish(ModalResponseData.Continue);
            ModalOpenRequested = false;
        }

        private void RaiseCustom()
        {
            _eventAggregator.GetEvent<ModalResponse>().Publish(ModalResponseData.Custom);
            ModalOpenRequested = false;
        }

        private void RaiseRetry()
        {
            _eventAggregator.GetEvent<ModalResponse>().Publish(ModalResponseData.Retry);
            ModalOpenRequested = false;
        }
    }
}