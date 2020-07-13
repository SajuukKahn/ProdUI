using ProdData.Models;
using ProdData.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProdTestGenerator
{
    class Program
    {
        List<CardModel> _productionCardCollection;
        ProdDataViewModel _productionCardDeckViewModel;
        List<ProgramModel> _productionProgramCollection;

        private string[] RandomCoordinates()
        {
            return new string[] { new Random().Next(-20000, 200000).ToString(), new Random().Next(-20000, 200000).ToString(), new Random().Next(-9000, 100000).ToString() };
        }

        public List<CardModel> GenerateRandom(List<CardModel> productionCardDeckModel)
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
            _productionCardCollection = productionCardDeckModel;

            BitmapImage image;//= new BitmapImage(new Uri("/Tests/TestImages/PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.Relative));
            image = new BitmapImage(new Uri("/Tests/TestImages/PCB5.bmp", UriKind.Relative));

            List<CardSubStepModel> fidList = new List<CardSubStepModel>
            {
                new CardSubStepModel("Fiducial A", RandomCoordinates()),
                new CardSubStepModel("Fiducial B", RandomCoordinates())
            };
            CardModel firstCardModel = new CardModel()
            {
                CardSubSteps = fidList,
                StepTitle = "Fiducial Check",
                StepImage = image
            };

            _productionCardCollection.Add(firstCardModel);
            int randSize = new Random().Next(4, 30);
            List<CardSubStepModel> surfaceList = new List<CardSubStepModel>();
            for (int i = 0; i < randSize; i++)
            {
                surfaceList.Add(new CardSubStepModel("Surface " + (i + 1), RandomCoordinates()));
            }
            CardModel secondCardModel = new CardModel()
            {
                CardSubSteps = surfaceList,
                StepTitle = "Surface Height Check",
                StepImage = image
            };
            _productionCardCollection.Add(secondCardModel);

            randSize = new Random().Next(15, 200);
            for (int i = 0; i < randSize; i++)
            {
                string stepTitle = TitleArray[new Random().Next(TitleArray.Length)];
                int randSteps = new Random().Next(1, 12);
                List<CardSubStepModel> randomSteps = new List<CardSubStepModel>();

                for (int j = 0; j < randSteps; j++)
                {
                    randomSteps.Add(new CardSubStepModel(stepTitle + (j + 1), RandomCoordinates()));
                }

                CardModel cardModel = new CardModel()
                {
                    CardSubSteps = randomSteps,
                    StepTitle = stepTitle,
                };

                if(new Random().Next(0, 3) == 1)
                {
                    cardModel.StepImage = image;
                }

                _productionCardCollection.Add(cardModel);
            }

            return _productionCardCollection;
        }

        public ProdDataViewModel GenerateRandom(ProdDataViewModel productionCardDeckViewModel)
        {
            _productionCardDeckViewModel = productionCardDeckViewModel;
            _productionCardDeckViewModel.ProgramName = ProgramName;
            return _productionCardDeckViewModel;
        }

        public List<ProgramModel> GenerateRandom(List<ProgramModel> productionPrograms)
        {
            _productionProgramCollection = productionPrograms;

            int randsize = new Random().Next(5, 11);

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

                _productionProgramCollection.Add(new ProgramModel(progname, relations[new Random().Next(relations.Length)] ,makernames[new Random().Next(makernames.Length)]));
            }

            return _productionProgramCollection;
        }

        private string ProgramName
        {
            get
            {
                return "Program " + new Random().Next(1, 30).ToString();
            }
        }

        #region MechanicalTurk

        private CancellationTokenSource _randomProgramCancellationTokenSource;
        private CancellationToken _randomProgramCancelToken;

        private bool _programPaused;

        private void LoadRandomProgram(bool reUseProgram = false)
        {
            if (!reUseProgram)
            {
                //ProcessDisplay = new BitmapImage(new Uri("/Tests/TestImages/PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.Relative));
                _productionCardCollection.Clear();
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

        #endregion

    }
}
