using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using ProdData.Events;
using ProdData.Models;
using ProdData.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProdTestGenerator
{
    public class TestGenerator : IModule
    {
        public TestGenerator(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ProgramNamesRequest>().Subscribe(RequestProgramNamesReceived);
            _eventAggregator.GetEvent<ProgramDataRequest>().Subscribe(RequestProgramDataRecieved);
        }

        private void RequestProgramDataRecieved()
        {
            _eventAggregator.GetEvent<ProgramDataResponse>().Publish(GenerateRandom(new ObservableCollection<Card>()));
        }

        private void RequestProgramNamesReceived()
        {
            _eventAggregator.GetEvent<ProgramNamesResponse>().Publish(GenerateRandom(new ObservableCollection<ProgramID>()));
        }

        private string[] RandomCoordinates()
        {
            return new string[] { new Random().Next(-20000, 200000).ToString(), new Random().Next(-20000, 200000).ToString(), new Random().Next(-9000, 100000).ToString() };
        }

        public ObservableCollection<Card> GenerateRandom(ObservableCollection<Card> cardDeck)
        {
            string[] TitleArray =
            {
                    "PolyLine 3D",
                    "Area",
                    "Line",
                    "PolyLine",
                    "Arc",
                    "Spiral",
                    "Dot",
                    "Part Presense Check"
            };

            BitmapImage? image;//= new BitmapImage(new Uri("/TestImages/PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.Relative));
            image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB5.bmp", UriKind.Relative));

            List<CardSubStep> fidList = new List<CardSubStep>
                {
                    new CardSubStep("Fiducial A", RandomCoordinates()),
                    new CardSubStep("Fiducial B", RandomCoordinates())
                };
            Card firstCardModel = new Card()
            {
                CardSubSteps = fidList,
                StepTitle = "Fiducial Check",
                StepImage = image
            };

            cardDeck.Add(firstCardModel);
            int randSize = new Random().Next(4, 30);
            List<CardSubStep> surfaceList = new List<CardSubStep>();
            for (int i = 0; i < randSize; i++)
            {
                surfaceList.Add(new CardSubStep("Surface " + (i + 1), RandomCoordinates()));
            }
            Card secondCardModel = new Card()
            {
                CardSubSteps = surfaceList,
                StepTitle = "Surface Height Check",
                StepImage = image
            };
            cardDeck.Add(secondCardModel);

            randSize = new Random().Next(15, 200);
            for (int i = 0; i < randSize; i++)
            {
                string stepTitle = TitleArray[new Random().Next(TitleArray.Length)];
                int randSteps = new Random().Next(1, 12);
                List<CardSubStep> randomSteps = new List<CardSubStep>();

                for (int j = 0; j < randSteps; j++)
                {
                    randomSteps.Add(new CardSubStep(stepTitle + (j + 1), RandomCoordinates()));
                }

                Card cardModel = new Card()
                {
                    CardSubSteps = randomSteps,
                    StepTitle = stepTitle,
                };

                if (new Random().Next(0, 3) == 1)
                {
                    cardModel.StepImage = image;
                }

                cardDeck.Add(cardModel);
            }

            return cardDeck;
        }

        public ObservableCollection<ProgramID> GenerateRandom(ObservableCollection<ProgramID> productionPrograms)
        {
            int randsize = new Random().Next(15, 215);

            string[] makernames =
            {
                    "Frank Briggs", "Alfred Travis", "Clarence Wallace", "Oscar Stanley", "Allan Gregory", "Randy Wyatt", "Terrence Hall", "Martin Charles", "Daniel Frazier", "Kevin Phillips",
                    "Vernon Merrill", "Gene Nolan", "Kent Kirk", "Don Webb", "Eric Wilcox", "Edward Garrett", "Douglas Jacobs", "Reginald Booth", "Dustin Mathis", "Mason Campbell",
                    "Bonnie Willis", "Thelma Frye", "Geneva Fowler", "Emma English", "Martha Harrison", "Jan Livingston", "Krista Hensley", "Yolanda Maxwell", "Elizabeth Lucas", "Whitney Powers",
                    "Marion Evans", "Julia Cain", "Ellen O'Neill", "Martha Conrad", "Vicky Harvey", "Kathy Finley", "Tracie Gentry", "Kelsey Workman", "Natalie Petersen", "Whitney Lane"
                };

            string[] relations =
            {
                    "Redshift Bundler", "Anode Booster", "Photon Slicer", "Cathode Estimator", "Cell Arranger", "Ptolemy Modifier", "Oersted Perceiver", "Hertz Constructor", "Ito Controller", "Brandt Clamper", "Axion Mixer",
                    "Electric Surger", "Exothermic Wrencher", "Rotation Compressor", "Harmonic Migrator", "Exothermic Repeller", "de Fermat Surger", "Broglie Arranger", "Malpighi Estimator", "Mesmer Energizer", "Gamma Bundler",
                    "Liquid Sterilizer", "Collision Morpher", "Pressure Sterilizer", "Electric Fuser", "Brongniart Mixer", "Nobel Merger", "Foucault Forger", "Heisenberg Pauser", "Ising Detector", "Plasma Transformer", "Anode Converter",
                    "Flexibility Merger", "Gradient Reflector", "Collision Multiplier", "Kapitsa Handler", "Pavlov Retriever", "Heisenberg Compressor", "Marconi Transformer", "Raman Twister"
                };

            for (int i = 0; i < randsize; i++)
            {
                string progname = ((char)new Random().Next(65, 90)).ToString() + ((char)new Random().Next(65, 90)).ToString() + new Random().Next(1, 102).ToString() + " Rev_" + (char)new Random().Next(65, 90);
               
                BitmapImage? image =  new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));

                productionPrograms.Add(new ProgramID(image, progname, relations[new Random().Next(relations.Length)], makernames[new Random().Next(makernames.Length)]));
            }

            return productionPrograms;
        }

        #region MechanicalTurk

        private CancellationTokenSource _randomProgramCancellationTokenSource;
        private CancellationToken _randomProgramCancelToken;

        private bool _programPaused;
        private IEventAggregator _eventAggregator;

        private void LoadRandomProgram(bool reUseProgram = false)
        {
            if (!reUseProgram)
            {
                //ProcessDisplay = new BitmapImage(new Uri("/TestImages/PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.Relative));
                //cardDeck.Clear();
                //ProductionCards = GenerateRandom(_productionCardCollection);
            }
            if (_randomProgramCancellationTokenSource == null)
            {
                _randomProgramCancellationTokenSource = new CancellationTokenSource();
                _randomProgramCancelToken = _randomProgramCancellationTokenSource.Token;
            }
            else
            {
                _randomProgramCancellationTokenSource.Cancel();
                _randomProgramCancellationTokenSource.Dispose();
                _randomProgramCancellationTokenSource = new CancellationTokenSource();
                _randomProgramCancelToken = _randomProgramCancellationTokenSource.Token;
            }

            var task = Task.Run(async () =>
            {
                while (true)
                {
                    if (_randomProgramCancelToken.IsCancellationRequested)
                    {
                        return;
                    }

                    if (!_programPaused)
                    {
                        await Task.Delay((TimeSpan.FromSeconds(new Random().Next(1, 5) + new Random().NextDouble())));
                        if (!_programPaused)
                        {
                            //Next()
                        }
                    }
                }
            }, _randomProgramCancelToken);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<TestGenerator>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<TestGenerator>();
        }

        #endregion

    }
}
