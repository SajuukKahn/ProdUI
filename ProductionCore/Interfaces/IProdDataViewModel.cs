using ProductionCore.Concrete;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace ProductionCore.Interfaces
{
    public interface IProdDataViewModel
    {
        public bool AllowProgramChange { get; set; }
        public ObservableCollection<Card?> CardCollection { get; set; }
        public Card? CurrentCard { get; set; }
        public int CurrentCardIndex { get; set; }
        public long CycleCount { get; set; }
        public Timer CycleTime { get; set; }
        public bool PauseAvailable { get; set; }
        public bool PlayAvailable { get; set; }
        public bool PlayBackRunning { get; set; }
        public BitmapImage? ProductImage { get; set; }
        public IProgramData? SelectedProgramData { get; set; }

        public void HandleAdvanceStep();

        public bool IterateSubStep();

        public void PauseCard();

        public void RetryStep();

        public void StartCard();
    }
}