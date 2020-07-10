using Prism.Commands;
using Prism.Mvvm;
using ProdCards.Models;
using System.Collections.ObjectModel;

namespace ProdCards.ViewModels
{
    public class ProdCardsViewModel : BindableBase
    {

        private ObservableCollection<ProdCardModel> _prodCards;
        public ObservableCollection<ProdCardModel> ProdCards
        {
            get
            {
                return _prodCards;
            }
            set
            {
                SetProperty(ref _prodCards, value);
            }
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                SetProperty(ref _currentIndex ,NextCard(value));
            }
        }

        private int _currentSubStepIndex;
        public int CurrentSubStepIndex
        {
            get
            {
                return _currentSubStepIndex;
            }
            set
            {
                SetProperty(ref _currentSubStepIndex, value);
            }
        }

        public int NextCard(int value, bool StepPassed = true, bool StepComplete = true)
        {
            if (value > -1 && CurrentIndex > -1 && CurrentIndex < _prodCards.Count)
            {

                bool setCompleted = false;
                int i = value, ceiling = CurrentIndex;

                // need to add in some stuff here... changing the "status" of the card, for instance

                if (CurrentIndex < value)
                {
                    setCompleted = true;
                    i = CurrentIndex;
                    ceiling = value;
                }

                for (int j = i; j <= ceiling; j++)
                {
                    if (setCompleted)
                    {
                        _prodCards[j].StepPassed = StepPassed;
                        _prodCards[j].StepComplete = StepComplete;
                    }
                    else
                    {
                        _prodCards[j].StepPassed = false;
                        _prodCards[j].StepComplete = false;
                    }
                    _prodCards[j].IsActiveStep = false;
                }

                _prodCards[value].IsActiveStep = true;
                _prodCards[value].StepComplete = false;
            }

            return value;
        }

        public ProdCardsViewModel()
        {
            _prodCards = new ObservableCollection<ProdCardModel>();
        }
    }
}
