using Prism.Events;
using ProdData.Events;
using ProdData.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProdTestGenerator
{
    public class TestGeneratorSingleton
    {
        private bool _debugEnabled = true;
        private IEventAggregator _eventAggregator;
        private CancellationTokenSource _programCancellationTokenSource;
        private CancellationToken _programCancelToken;
        private bool _programIsInProgress;

        private bool _programPaused;

        public TestGeneratorSingleton(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<StartRequest>().Subscribe(FulfillStartRequest);
            _eventAggregator.GetEvent<PauseRequest>().Subscribe(FulfillPauseRequest);
            _eventAggregator.GetEvent<ProductImageChangeRequest>().Subscribe(FulfillProcessDisplayChangeRequest);
            _eventAggregator.GetEvent<ProgramNamesRequest>().Subscribe(RequestProgramNamesReceived);
            _eventAggregator.GetEvent<ProgramDataRequest>().Subscribe(RequestProgramDataReceived);
        }

        public ObservableCollection<Card> GenerateRandom(ObservableCollection<Card> cardDeck)
        {
            DebugLogCaller();
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

            BitmapImage image;//= new BitmapImage(new Uri("/TestImages/PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.Relative));
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

            randSize = new Random().Next(15, 75);
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

        public ObservableCollection<ProgramData> GenerateRandom(ObservableCollection<ProgramData> productionPrograms)
        {
            DebugLogCaller();
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

                BitmapImage? image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));

                productionPrograms.Add(new ProgramData(image, progname, relations[new Random().Next(relations.Length)], makernames[new Random().Next(makernames.Length)]));
            }

            return productionPrograms;
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
            _programPaused = true;
        }

        private void FulfillProcessDisplayChangeRequest()
        {
            DebugLogCaller();
            GenerateProcessDisplayChangeResponse();
        }

        private void GenerateProcessDisplayChangeResponse()
        {
            DebugLogCaller();
            BitmapImage image = ChooseRandomImage();
            _eventAggregator.GetEvent<ProductImageChangeResponse>().Publish(image);
        }

        private BitmapImage ChooseRandomImage()
        {
            BitmapImage? image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
            return image;
        }

        private void FulfillStartRequest()
        {
            DebugLogCaller();
            _programPaused = false;
            if (_programIsInProgress == false)
            {
                RunProg();
            }
        }

        private string[] RandomCoordinates()
        {
            return new string[] { new Random().Next(-20000, 200000).ToString(), new Random().Next(-20000, 200000).ToString(), new Random().Next(-9000, 100000).ToString() };
        }

        private void RequestProgramDataReceived(ProgramData programData)
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<ProgramDataResponse>().Publish(GenerateRandom(new ObservableCollection<Card>()));
        }

        private void RequestProgramNamesReceived()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<ProgramNamesResponse>().Publish(GenerateRandom(new ObservableCollection<ProgramData>()));
        }

        private void RunProg()
        {
            DebugLogCaller();
            if (_programCancellationTokenSource == null)
            {
                _programCancellationTokenSource = new CancellationTokenSource();
                _programCancelToken = _programCancellationTokenSource.Token;
            }
            else
            {
                _programCancellationTokenSource.Cancel();
                _programCancellationTokenSource.Dispose();
                _programCancellationTokenSource = new CancellationTokenSource();
                _programCancelToken = _programCancellationTokenSource.Token;
            }

            var task = Task.Run(async () =>
            {
                bool pauseRequestResponded = false;
                _programIsInProgress = true;
                while (true)
                {
                    if (_programCancelToken.IsCancellationRequested)
                    {
                        _programIsInProgress = false;
                        return;
                    }

                    if (_programPaused && !pauseRequestResponded)
                    {
                        _eventAggregator.GetEvent<ProgramPaused>().Publish();
                        pauseRequestResponded = true;
                    }

                    if (!_programPaused)
                    {
                        await Task.Delay((TimeSpan.FromSeconds(new Random().Next(1, 5) + new Random().NextDouble())));
                        if (!_programPaused)
                        {
                            pauseRequestResponded = false;
                            _eventAggregator.GetEvent<AdvanceStep>().Publish();
                        }
                    }
                }
            }, _programCancelToken);
        }
    }
}