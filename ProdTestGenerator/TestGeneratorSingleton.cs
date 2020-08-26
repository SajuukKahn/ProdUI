using Prism.Events;
using ProdData.Events;
using ProdData.Models;
using ProdData.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProdTestGenerator
{
    public class TestGeneratorSingleton
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly int _scaleDataSet = 1;
        private CancellationTokenSource _programCancellationTokenSource;

        private CancellationToken _programCancelToken;

        private bool _programIsInProgress;

        private bool _programPaused;
        private bool dateSwitch;

        private DateTime lastDate;

        public TestGeneratorSingleton(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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

        public ObservableCollection<Card> GenerateRandom(ProgramData programData)
        {
            ObservableCollection<Card> cardDeck = new ObservableCollection<Card>();
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

            if(programData.ProgramName == "Manual Placement Simulation")
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

            List<CardSubStep> fidList = new List<CardSubStep>
            {
                new CardSubStep("Fiducial A", RandomCoordinates()),
                new CardSubStep("Fiducial B", RandomCoordinates())
            };
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

        public ObservableCollection<ProgramData> GenerateRandom(ObservableCollection<ProgramData> productionPrograms)
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
                productionPrograms.Add(new ProgramData
                {
                    AverageCycleTime = GenerateRandomCycleTimeAverage(),
                    Barcode = GenerateBarcodeSometimes(),
                    CreatedDate = GenerateRandomDateTime(),
                    Dimensions = new Size(new Random().Next(750), new Random().Next(750)),
                    HistoricalCycles = new Random().Next(0, 9999),
                    LastEditDate = GenerateRandomDateTime(),
                    ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute)),
                    ProgramName = GenerateRandomString(26) + " Rev_" + (char)new Random().Next(65, 90),
                    ProductName = relations[new Random().Next(relations.Length)],
                    ProgramCreator = makernames[new Random().Next(makernames.Length)],
                    ToolsUsed = GenerateRandomTools(),
                    UserCanStartPlayback = new Random().Next(4).Equals(1),
                    AutoStartPlayback = false,
                    IsFavorite = new Random().Next(7).Equals(1)
                });
            }
            productionPrograms.Add(new ProgramData()
            {
                AverageCycleTime = GenerateRandomCycleTimeAverage(),
                Barcode = GenerateBarcodeSometimes(),
                CreatedDate = GenerateRandomDateTime(),
                Dimensions = new Size(new Random().Next(750), new Random().Next(750)),
                HistoricalCycles = new Random().Next(0, 9999),
                LastEditDate = GenerateRandomDateTime(),
                ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute)),
                ProgramName = "Manual Placement Simulation",
                ProductName = relations[new Random().Next(relations.Length)],
                ProgramCreator = makernames[new Random().Next(makernames.Length)],
                ToolsUsed = GenerateRandomTools(),
                UserCanStartPlayback = false,
                AutoStartPlayback = true
            });
            return productionPrograms;
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

        private Barcode GenerateBarcodeSometimes()
        {
            Barcode barcode = new Barcode();
            if (new Random().Next(0, 4) == 1)
            {
                barcode.BarcodeData = GenerateRandomString(128);
                barcode.BarcodeImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\BAR" + new Random().Next(1, 4).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
            }
            return barcode;
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

        private void RequestProgramDataReceived(ProgramData programData)
        {
            _eventAggregator.GetEvent<ProgramDataResponse>().Publish(GenerateRandom(programData));
        }

        private void RequestProgramDataSaveReceived(ProdDataViewModel prodData)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = prodData.SelectedProgramData.ProgramName +
                                "Run#" + prodData.SelectedProgramData.HistoricalCycles + "_Completed" +
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
                prodData.SelectedProgramData.ProgramName + "," +
                prodData.SelectedProgramData.ProductName + "," +
                prodData.SelectedProgramData.ProgramCreator + "," +
                prodData.SelectedProgramData.AverageCycleTime + "," +
                prodData.SelectedProgramData.HistoricalCycles + ",");

            sw.WriteLine("Title,Time,Status,Sub Steps");

            foreach (Card card in prodData.CardCollection)
            {
                sw.WriteLine(card.ToString());
            }
            _eventAggregator.GetEvent<ProgramDataSaveResponse>().Publish();
            sw.Close();
        }

        private void RequestProgramNamesReceived()
        {
            _eventAggregator.GetEvent<ProgramNamesResponse>().Publish(GenerateRandom(new ObservableCollection<ProgramData>()));
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
                        //Task.Delay(TimeSpan.FromSeconds(new Random().Next(2, 5) + new Random().NextDouble()));
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