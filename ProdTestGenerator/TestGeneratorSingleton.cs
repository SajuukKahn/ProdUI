using Prism.Events;
using Prism.Ioc;
using ProductionCore.Events;
using ProductionCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ProductionCore.Concrete;
using System.Drawing;

namespace ProdTestGenerator
{
    public class TestGeneratorSingleton
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly IProgramCollection _programCollection;
        private readonly IProgramDataFactory _programDataFactory;
        private readonly IBarcodeFactory _barcodeFactory;
        private IProdDataViewModel _prodDataViewModel;

        private readonly int _scaleDataSet = 1;
        private CancellationTokenSource? _programCancellationTokenSource;

        private CancellationToken _programCancelToken;

        private bool _programIsInProgress;

        private bool _programPaused;
        private bool dateSwitch;

        private DateTime lastDate;

        //TODO I think I would refactor this entire class to be a series of classes / services
        public TestGeneratorSingleton(IEventAggregator eventAggregator, IProgramCollection programCollection, IProgramDataFactory programDataFactory, IBarcodeFactory barcodeFactory, IProdDataViewModel prodDataViewModel)
        {
            _prodDataViewModel = prodDataViewModel;
            _programCollection = programCollection;
            _eventAggregator = eventAggregator;
            _programDataFactory = programDataFactory;
            _barcodeFactory = barcodeFactory;
            _eventAggregator.GetEvent<StartRequest>().Subscribe(FulfillStartRequest);
            _eventAggregator.GetEvent<PauseRequest>().Subscribe(FulfillPauseRequest);
            _eventAggregator.GetEvent<ProductImageChangeRequest>().Subscribe(FulfillProcessDisplayChangeRequest);
            _eventAggregator.GetEvent<ProgramNamesRequest>().Subscribe(RequestProgramNamesReceived);
            _eventAggregator.GetEvent<ProgramDataRequest>().Subscribe(RequestProgramDataReceived);
            _eventAggregator.GetEvent<ProgramDataSaveRequest>().Subscribe(RequestProgramDataSaveReceived);
            _eventAggregator.GetEvent<ModalEvent>().Subscribe((m) => ModalHandle(true));
            _eventAggregator.GetEvent<ModalResponse>().Subscribe((m) => ModalHandle(false));
        }

        private void ModalHandle(bool tf)
        {
            _modalRaised = tf;
         }

        public ObservableCollection<Card?> GenerateRandom(IProgramData? programData)
        {
            ObservableCollection<Card?> cardDeck = new ObservableCollection<Card?>();
            string[] TitleArray =
            {
                "PolyLine 3D",
                "Area",
                "Move",
                "Line",
                "PolyLine",
                "Arc",
                "Spiral",
                "Rectangular Sprial",
                "Dot",
                "Part Presense Check"
            };

            BitmapImage? image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\FID.bmp", UriKind.RelativeOrAbsolute));

            if(programData?.ProgramName == "Manual Placement Simulation")
            {
                List<CardSubStep> steplist = new List<CardSubStep>
            {
                new CardSubStep("Place Product", new string[] {""})
            };
                Card zerothCardModel = new Card()
                {
                    CardSubSteps = steplist,
                    StepTitle = "Place Product",
                    StepImage = programData.ProductImage,
                    StepModalData = new ModalData()
                    {
                        CanAbort = true,
                        CanContinue = true,
                        CanRetry = false,
                        Card = null,
                        Instructions = "Place Product and press 'Continue' to begin" + Environment.NewLine + "Press 'Abort' to exit path playback",
                        InstructionImage = programData.ProductImage,
                        IsError = false
                    }
                };
                cardDeck.Add(zerothCardModel);
            }

            List<CardSubStep> fidList = new List<CardSubStep>();

            fidList.Add(new CardSubStep("Fiducial A", RandomCoordinates()));
            fidList.Add(new CardSubStep("Fiducial B", RandomCoordinates()));

            Card firstCardModel = new Card()
            {
                CardSubSteps = fidList,
                StepTitle = "Fiducial Check",
                StepImage = image,
                StepModalData = new ModalData() { CanAbort = true, CanContinue = true, CanRetry = true, Card = null, Instructions = "Fiducials failed, Select an option below", InstructionImage = image }
            };

            cardDeck.Add(firstCardModel);
            int randSize = new Random().Next(2 * _scaleDataSet, 8 * _scaleDataSet);
            List<CardSubStep> surfaceList = new List<CardSubStep>();
            for (int i = 0; i < randSize; i++)
            {
                surfaceList.Add(new CardSubStep("Surface " + (i + 1), RandomCoordinates()));
            }

            Card secondCardModel = new Card()
            {
                CardSubSteps = surfaceList,
                StepTitle = "Surface Height Check",
                StepModalData = new ModalData() { CanAbort = true, CanRetry = true, Card = null, Instructions = "Surface Height Checks failed, Select an option below"}
            };
            cardDeck.Add(secondCardModel);

            randSize = new Random().Next(4 * _scaleDataSet, 12 * _scaleDataSet);
            for (int i = 0; i < randSize; i++)
            {
                string stepTitle = TitleArray[new Random().Next(TitleArray.Length)];
                int randSteps = new Random().Next(1 * _scaleDataSet, 8 * _scaleDataSet);
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

                if (new Random().Next(0, 4) == 1)
                {
                    image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                    cardModel.StepImage = image;
                }

                if(cardModel.StepTitle == "Part Presence Check")
                {
                    cardModel.StepModalData = new ModalData() { CanAbort = true, CanContinue = true, CanRetry = true, Card = null, Instructions = cardModel.StepTitle +  " failed, Select an option below"};
                }

                cardDeck.Add(cardModel);
            }

            return cardDeck;
        }

        public void GenerateRandomProgramCollection()
        {
            int randsize = new Random().Next(7 * _scaleDataSet, 45 * _scaleDataSet);

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
                var prog = _programDataFactory.Create();
                prog.AverageCycleTime = GenerateRandomCycleTimeAverage();

                if (new Random().Next(0, 5) == 1)
                {
                    prog.Barcode = _barcodeFactory.Create(GenerateRandomString(128),
                        new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\BAR" + new Random().Next(1, 4).ToString() + ".bmp", UriKind.RelativeOrAbsolute)));
                }

                prog.CreatedDate = GenerateRandomDateTime();
                prog.Dimensions = new Size(new Random().Next(750), new Random().Next(750));
                prog.HistoricalCycles = new Random().Next(0, 9999);
                prog.LastEditDate = GenerateRandomDateTime();
                prog.ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                prog.ProgramName = GenerateRandomString(26) + " Rev_" + (char)new Random().Next(65, 90);
                prog.ProductName = relations[new Random().Next(relations.Length)];
                prog.ProgramCreator = makernames[new Random().Next(makernames.Length)];
                prog.ToolsUsed = GenerateRandomTools();
                prog.UserCanStartPlayback = new Random().Next(4).Equals(1);
                prog.AutoStartPlayback = false;
                prog.IsFavorite = new Random().Next(7).Equals(1);
                
                _programCollection!.ProgramList?.Add(prog);
            }

            var specialProg = _programDataFactory.Create();

            specialProg.AverageCycleTime = GenerateRandomCycleTimeAverage();
            specialProg.CreatedDate = GenerateRandomDateTime();
            specialProg.Dimensions = new Size(new Random().Next(750), new Random().Next(750));
            specialProg.HistoricalCycles = new Random().Next(0, 9999);
            specialProg.LastEditDate = GenerateRandomDateTime();
            specialProg.ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
            specialProg.ProgramName = "Manual Placement Simulation";
            specialProg.ProductName = relations[new Random().Next(relations.Length)];
            specialProg.ProgramCreator = makernames[new Random().Next(makernames.Length)];
            specialProg.ToolsUsed = GenerateRandomTools();
            specialProg.UserCanStartPlayback = false;
            specialProg.AutoStartPlayback = true;

            _programCollection!.ProgramList?.Add(specialProg);
        }

        private BitmapImage ChooseRandomImage()
        {
            BitmapImage? image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
            return image;
        }

        private void FulfillPauseRequest()
        {
            _programPaused = true;
        }

        private void FulfillProcessDisplayChangeRequest()
        {
            GenerateProcessDisplayChangeResponse();
        }

        private void FulfillStartRequest()
        {
            _programPaused = false;
            if (_programIsInProgress == false)
            {
                RunProg();
            }
        }

        private void GenerateProcessDisplayChangeResponse()
        {
            BitmapImage image = ChooseRandomImage();
            _eventAggregator.GetEvent<ProductImageChangeResponse>().Publish(image);
        }

        private TimeSpan GenerateRandomCycleTimeAverage()
        {
            return new TimeSpan(0, 0, new Random().Next(12), new Random().Next(59), new Random().Next(999));
        }

        private DateTime GenerateRandomDateTime()
        {
            DateTime start = new DateTime(1995, 1, 1);
            if (dateSwitch == true)
            {
                start = lastDate;
            }
            int range = (DateTime.Today - start).Days;
            lastDate = start.AddDays(new Random().Next(range));
            dateSwitch ^= true;
            return lastDate;
        }

        private string GenerateRandomString(int maxLength)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[new Random().Next(1, maxLength)];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        private string[] GenerateRandomTools()
        {
            string[] toolReturn = new string[new Random().Next(1, 4)];
            string[] tools =
            {
            "FCM100", "FC100", "FC300", "FC100-MC","FC100-CF","FC100-C4","FCS300-ES","FCS300-ES-ND","FCS300-R","FCS300-F","PC200","PC200-TCM",
            "SB300","SB300-C","JDX","SJ100","VPX-2K","VPX-450","SVX","MR1","MR2","IR Sensor","Ionizer","UV Wand","Laser Height Sensor","Touch Probe"
            };

            for (int i = 0; i < toolReturn.Length; i++)
            {
                string toolcheck = tools[new Random().Next(tools.Length)];
                while (toolReturn.Contains(toolcheck))
                {
                    toolcheck = tools[new Random().Next(tools.Length)];
                }
                toolReturn[i] = toolcheck;
            }

            return toolReturn;
        }

        private string[] RandomCoordinates()
        {
            return new string[] { new Random().Next(-20000, 200000).ToString(), new Random().Next(-20000, 200000).ToString(), new Random().Next(-9000, 100000).ToString() };
        }

        private void RequestProgramDataReceived(IProgramData? programData)
        {
            _eventAggregator.GetEvent<ProgramDataResponse>().Publish(GenerateRandom(programData));
        }

        private void RequestProgramDataSaveReceived()
        {
            var prodData = _prodDataViewModel;
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = prodData.SelectedProgramData?.ProgramName +
                                "Run#" + prodData.SelectedProgramData?.HistoricalCycles + "_Completed" +
                                DateTime.Now.ToString("yyyy-MM-d--HH-mm-ss") + ".csv";
            string outputPath = Path.Combine(filePath, fileName);

            TextWriter sw = new StreamWriter(outputPath);

            sw.WriteLine(
                nameof(prodData.SelectedProgramData.ProgramName) + "," +
                nameof(prodData.SelectedProgramData.ProductName) + "," +
                nameof(prodData.SelectedProgramData.ProgramCreator) + "," +
                nameof(prodData.SelectedProgramData.AverageCycleTime) + "," +
                nameof(prodData.SelectedProgramData.HistoricalCycles) + ",");

            sw.WriteLine(
                prodData.SelectedProgramData?.ProgramName + "," +
                prodData.SelectedProgramData?.ProductName + "," +
                prodData.SelectedProgramData?.ProgramCreator + "," +
                prodData.SelectedProgramData?.AverageCycleTime + "," +
                prodData.SelectedProgramData?.HistoricalCycles + ",");

            sw.WriteLine("Title,Time,Status,Sub Steps");

            foreach (Card? card in prodData.CardCollection)
            {
                sw.WriteLine(card?.ToString());
            }
            _eventAggregator.GetEvent<ProgramDataSaveResponse>().Publish();
            sw.Close();
        }

        private void RequestProgramNamesReceived()
        {
            GenerateRandomProgramCollection();
            _eventAggregator.GetEvent<ProgramNamesResponse>().Publish();
        }

        private bool _modalRaised;
        bool _pauseRequestResponded;
        private void RunProg()
        {
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

            //TODO This section controls playback - it's my fake process running
            var task = Task.Run(() =>
            {
                
                _programIsInProgress = true;
                while (true)
                {
                    if (_programCancelToken.IsCancellationRequested)
                    {
                        _programIsInProgress = false;
                        return;
                    }

                    if (_programPaused && !_pauseRequestResponded)
                    {
                        _eventAggregator.GetEvent<ProgramPaused>().Publish();
                        _pauseRequestResponded = true;
                    }

                    if (!_programPaused)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(2, 5) + new Random().NextDouble()));
                        if (!_programPaused && !_modalRaised)
                        {
                            _pauseRequestResponded = false;
                            _eventAggregator.GetEvent<AdvanceStep>().Publish();
                        }
                    }
                }
            }, _programCancelToken);
        }
    }
}