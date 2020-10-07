namespace ProdTestGenerator.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;
    using System;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="TestGeneratorViewModel" />.
    /// </summary>
    public class TestGeneratorViewModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestGeneratorViewModel"/> class.
        /// </summary>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        public TestGeneratorViewModel(IPlaybackService playbackService, IFileService fileService, IModalService modalService)
        {
            StartButton = new DelegateCommand(() => playbackService.Play()).ObservesCanExecute(() => playbackService.PlayAvailable);
            PauseButton = new DelegateCommand(() => playbackService.Pause()).ObservesCanExecute(() => playbackService.PauseAvailable);
            ChangeProgram = new DelegateCommand(() => fileService.RetrieveProgram(null));
            ChangeProcessImage = new DelegateCommand(() => { playbackService.ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute)); });            
            ThrowCardError = new DelegateCommand(() => playbackService.RaiseError());
            ThrowError = new DelegateCommand(() => playbackService.RaiseError(modalService.CreateModalData()));
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
