using Prism.Commands;
using Prism.Mvvm;

namespace ProdCards.ViewModels
{
    public class ProdCardsViewModel : BindableBase
    {

        public DelegateCommand ButtonCommand { get; private set; }

        public ProdCardsViewModel()
        {
            ButtonCommand = new DelegateCommand(Click).ObservesCanExecute(() => CanExecute);
        }

        private void Click()
        {
            Title = "I got clicked";
        }

        private string _title = "Whatup brah"; 
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _canExecute;
        public bool CanExecute
        {
            get { return _canExecute; }
            set { SetProperty(ref _canExecute, value); }
        }

    }
}
