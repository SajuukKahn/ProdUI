namespace ProdTestGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;
    using Prism.Events;
    using ProductionCore.Concrete;
    using ProductionCore.Events;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="TestGeneratorSingleton" />.
    /// </summary>
    public class TestGeneratorSingleton
    {
        /// <summary>
        /// Defines the _eventAggregator.
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Defines the _scaleDataSet.
        /// </summary>
        private readonly int _scaleDataSet = 1;

        /// <summary>
        /// Defines the _prodDataViewModel.
        /// </summary>
        private readonly IProdDataViewModel _prodDataViewModel;

        /// <summary>
        /// Defines the _programDataService.
        /// </summary>
        private readonly IProgramDataService _programDataService;

        /// <summary>
        /// Defines the _programCancellationTokenSource.
        /// </summary>
        private CancellationTokenSource? _programCancellationTokenSource;

        /// <summary>
        /// Defines the _programCancelToken.
        /// </summary>
        private CancellationToken _programCancelToken;

        /// <summary>
        /// Defines the _programIsInProgress.
        /// </summary>
        private bool _programIsInProgress;

        /// <summary>
        /// Defines the _programPaused.
        /// </summary>
        private bool _programPaused;

        /// <summary>
        /// Defines the _dateSwitch.
        /// </summary>
        private bool _dateSwitch;

        /// <summary>
        /// Defines the _lastDate.
        /// </summary>
        private DateTime _lastDate;

        /// <summary>
        /// Defines the _modalRaised.
        /// </summary>
        private bool _modalRaised;

        /// <summary>
        /// Defines the _pauseRequestResponded.
        /// </summary>
        private bool _pauseRequestResponded;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestGeneratorSingleton"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="prodDataViewModel">The prodDataViewModel<see cref="IProdDataViewModel"/>.</param>
        /// <param name="programDataService">The programDataService<see cref="IProgramDataService"/>.</param>
        public TestGeneratorSingleton(IEventAggregator eventAggregator, IProdDataViewModel prodDataViewModel, IProgramDataService programDataService)
        {
            _prodDataViewModel = prodDataViewModel;
            _programDataService = programDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<StartRequest>().Subscribe(FulfillStartRequest);
            _eventAggregator.GetEvent<PauseRequest>().Subscribe(FulfillPauseRequest);
            _eventAggregator.GetEvent<ProductImageChangeRequest>().Subscribe(FulfillProcessDisplayChangeRequest);
            _eventAggregator.GetEvent<ProgramDataRequest>().Subscribe(RequestProgramDataReceived);
            _eventAggregator.GetEvent<ProgramDataSaveRequest>().Subscribe(RequestProgramDataSaveReceived);
            _eventAggregator.GetEvent<ModalEvent>().Subscribe((m) => ModalHandle(true));
            _eventAggregator.GetEvent<ModalResponse>().Subscribe((m) => ModalHandle(false));
            GenerateRandomProgramCollection();
        }

        /// <summary>
        /// The GenerateRandom.
        /// </summary>
        /// <param name="programData">The programData<see cref="IProgramData"/>.</param>
        /// <returns>The <see cref="ObservableCollection{Card}"/>.</returns>
        public ObservableCollection<Card?> GenerateRandom(IProgramData? programData)
        {
            ObservableCollection<Card?> cardDeck = new ObservableCollection<Card?>();
            string[] titleArray = { "PolyLine 3D", "Area", "Move", "Line", "PolyLine", "Arc", "Spiral", "Rectangular Sprial", "Dot", "Part Presense Check" };

            BitmapImage? image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\FID.bmp", UriKind.RelativeOrAbsolute));

            if (programData?.ProgramName == "Manual Placement Simulation")
            {
                List<CardSubStep> steplist = new List<CardSubStep>
            {
                new CardSubStep("Place Product", new string[] { string.Empty }),
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
                        IsError = false,
                    },
                };
                cardDeck.Add(zerothCardModel);
            }

            List<CardSubStep> fidList = new List<CardSubStep>
            {
                new CardSubStep("Fiducial A", RandomCoordinates()),
                new CardSubStep("Fiducial B", RandomCoordinates()),
            };

            Card firstCardModel = new Card()
            {
                CardSubSteps = fidList,
                StepTitle = "Fiducial Check",
                StepImage = image,
                StepModalData = new ModalData() { CanAbort = true, CanContinue = true, CanRetry = true, Card = null, Instructions = "Fiducials failed, Select an option below", InstructionImage = image },
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
                StepModalData = new ModalData() { CanAbort = true, CanRetry = true, Card = null, Instructions = "Surface Height Checks failed, Select an option below" },
            };
            cardDeck.Add(secondCardModel);

            randSize = new Random().Next(4 * _scaleDataSet, 12 * _scaleDataSet);
            for (int i = 0; i < randSize; i++)
            {
                string stepTitle = titleArray[new Random().Next(titleArray.Length)];
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

                if (cardModel.StepTitle == "Part Presence Check")
                {
                    cardModel.StepModalData = new ModalData() { CanAbort = true, CanContinue = true, CanRetry = true, Card = null, Instructions = cardModel.StepTitle + " failed, Select an option below" };
                }

                cardDeck.Add(cardModel);
            }

            return cardDeck;
        }

        /// <summary>
        /// The GenerateRandomProgramCollection.
        /// </summary>
        public void GenerateRandomProgramCollection()
        {
            int randsize = new Random().Next(7 * _scaleDataSet, 45 * _scaleDataSet);

            string[] makernames =
            {
            "Frank Briggs", "Alfred Travis", "Clarence Wallace", "Oscar Stanley", "Allan Gregory", "Randy Wyatt", "Terrence Hall", "Martin Charles", "Daniel Frazier", "Kevin Phillips",
            "Vernon Merrill", "Gene Nolan", "Kent Kirk", "Don Webb", "Eric Wilcox", "Edward Garrett", "Douglas Jacobs", "Reginald Booth", "Dustin Mathis", "Mason Campbell",
            "Bonnie Willis", "Thelma Frye", "Geneva Fowler", "Emma English", "Martha Harrison", "Jan Livingston", "Krista Hensley", "Yolanda Maxwell", "Elizabeth Lucas", "Whitney Powers",
            "Marion Evans", "Julia Cain", "Ellen O'Neill", "Martha Conrad", "Vicky Harvey", "Kathy Finley", "Tracie Gentry", "Kelsey Workman", "Natalie Petersen", "Whitney Lane",
            };

            string[] relations =
            {
            "Redshift Bundler", "Anode Booster", "Photon Slicer", "Cathode Estimator", "Cell Arranger", "Ptolemy Modifier", "Oersted Perceiver", "Hertz Constructor", "Ito Controller", "Brandt Clamper", "Axion Mixer",
            "Electric Surger", "Exothermic Wrencher", "Rotation Compressor", "Harmonic Migrator", "Exothermic Repeller", "de Fermat Surger", "Broglie Arranger", "Malpighi Estimator", "Mesmer Energizer", "Gamma Bundler",
            "Liquid Sterilizer", "Collision Morpher", "Pressure Sterilizer", "Electric Fuser", "Brongniart Mixer", "Nobel Merger", "Foucault Forger", "Heisenberg Pauser", "Ising Detector", "Plasma Transformer", "Anode Converter",
            "Flexibility Merger", "Gradient Reflector", "Collision Multiplier", "Kapitsa Handler", "Pavlov Retriever", "Heisenberg Compressor", "Marconi Transformer", "Raman Twister",
            };

            for (int i = 0; i < randsize; i++)
            {
                var prog = _programDataService.CreateProgram();
                prog.AverageCycleTime = GenerateRandomCycleTimeAverage();

                if (new Random().Next(0, 5) == 1)
                {
                    prog.Barcode = _programDataService.CreateBarcode();
                    prog.Barcode.BarcodeData = GenerateRandomString(128);
                    prog.Barcode.BarcodeImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\BAR" + new Random().Next(1, 4).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                }

                prog.CreatedDate = GenerateRandomDateTime();
                prog.Dimensions = new Size(new Random().Next(750), new Random().Next(750));
                prog.Cycles = new Random().Next(0, 9999);
                prog.LastEditDate = GenerateRandomDateTime();
                prog.ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                prog.ProgramName = GenerateRandomString(26) + " Rev_" + (char)new Random().Next(65, 90);
                prog.ProductName = relations[new Random().Next(relations.Length)];
                prog.ProgramCreator = makernames[new Random().Next(makernames.Length)];
                prog.ToolsUsed = GenerateRandomTools();
                prog.UserCanStartPlayback = new Random().Next(4).Equals(1);
                prog.AutoStartPlayback = false;
                prog.IsFavorite = new Random().Next(7).Equals(1);

                _programDataService.ProgramList.Add(prog);
            }

            var specialProg = _programDataService.CreateProgram();

            specialProg.AverageCycleTime = GenerateRandomCycleTimeAverage();
            specialProg.CreatedDate = GenerateRandomDateTime();
            specialProg.Dimensions = new Size(new Random().Next(750), new Random().Next(750));
            specialProg.Cycles = new Random().Next(0, 9999);
            specialProg.LastEditDate = GenerateRandomDateTime();
            specialProg.ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
            specialProg.ProgramName = "Manual Placement Simulation";
            specialProg.ProductName = relations[new Random().Next(relations.Length)];
            specialProg.ProgramCreator = makernames[new Random().Next(makernames.Length)];
            specialProg.ToolsUsed = GenerateRandomTools();
            specialProg.UserCanStartPlayback = false;
            specialProg.AutoStartPlayback = true;

            _programDataService.ProgramList.Add(specialProg);
        }

        /// <summary>
        /// The ModalHandle.
        /// </summary>
        /// <param name="tf">The tf<see cref="bool"/>.</param>
        private void ModalHandle(bool tf)
        {
            _modalRaised = tf;
        }

        /// <summary>
        /// The ChooseRandomImage.
        /// </summary>
        /// <returns>The <see cref="BitmapImage"/>.</returns>
        private BitmapImage ChooseRandomImage()
        {
            BitmapImage? image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
            return image;
        }

        /// <summary>
        /// The FulfillPauseRequest.
        /// </summary>
        private void FulfillPauseRequest()
        {
            _programPaused = true;
        }

        /// <summary>
        /// The FulfillProcessDisplayChangeRequest.
        /// </summary>
        private void FulfillProcessDisplayChangeRequest()
        {
            GenerateProcessDisplayChangeResponse();
        }

        /// <summary>
        /// The FulfillStartRequest.
        /// </summary>
        private void FulfillStartRequest()
        {
            _programPaused = false;
            if (_programIsInProgress == false)
            {
                RunProg();
            }
        }

        /// <summary>
        /// The GenerateProcessDisplayChangeResponse.
        /// </summary>
        private void GenerateProcessDisplayChangeResponse()
        {
            BitmapImage image = ChooseRandomImage();
            _eventAggregator.GetEvent<ProductImageChangeResponse>().Publish(image);
        }

        /// <summary>
        /// The GenerateRandomCycleTimeAverage.
        /// </summary>
        /// <returns>The <see cref="TimeSpan"/>.</returns>
        private TimeSpan GenerateRandomCycleTimeAverage()
        {
            return new TimeSpan(0, 0, new Random().Next(12), new Random().Next(59), new Random().Next(999));
        }

        /// <summary>
        /// The GenerateRandomDateTime.
        /// </summary>
        /// <returns>The <see cref="DateTime"/>.</returns>
        private DateTime GenerateRandomDateTime()
        {
            DateTime start = new DateTime(1995, 1, 1);
            if (_dateSwitch == true)
            {
                start = _lastDate;
            }

            int range = (DateTime.Today - start).Days;
            _lastDate = start.AddDays(new Random().Next(range));
            _dateSwitch ^= true;
            return _lastDate;
        }

        /// <summary>
        /// The GenerateRandomString.
        /// </summary>
        /// <param name="maxLength">The maxLength<see cref="int"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string GenerateRandomString(int maxLength)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[new Random().Next(1, maxLength)];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new string(stringChars);
            return finalString;
        }

        /// <summary>
        /// The GenerateRandomTools.
        /// </summary>
        /// <returns>The <see cref="ObservableCollection{String}"/>.</returns>
        private ObservableCollection<string> GenerateRandomTools()
        {
            ObservableCollection<string> toolReturn = new ObservableCollection<string>();
            string[] tools =
            {
            "FCM100", "FC100", "FC300", "FC100-MC", "FC100-CF", "FC100-C4", "FCS300-ES", "FCS300-ES-ND", "FCS300-R", "FCS300-F", "PC200", "PC200-TCM",
            "SB300", "SB300-C", "JDX", "SJ100", "VPX-2K", "VPX-450", "SVX", "MR1", "MR2", "IR Sensor", "Ionizer", "UV Wand", "Laser Height Sensor", "Touch Probe",
            };

            for (int i = 0; i < new Random().Next(1, 4); i++)
            {
                string toolcheck = tools[new Random().Next(tools.Length)];
                while (toolReturn.Contains(toolcheck))
                {
                    toolcheck = tools[new Random().Next(tools.Length)];
                }

                toolReturn.Add(toolcheck);
            }

            return toolReturn;
        }

        /// <summary>
        /// The RandomCoordinates.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string[] RandomCoordinates()
        {
            return new string[] { new Random().Next(-20000, 200000).ToString(), new Random().Next(-20000, 200000).ToString(), new Random().Next(-9000, 100000).ToString() };
        }

        /// <summary>
        /// The RequestProgramDataReceived.
        /// </summary>
        /// <param name="programData">The programData<see cref="IProgramData"/>.</param>
        private void RequestProgramDataReceived(IProgramData? programData)
        {
            _eventAggregator.GetEvent<ProgramDataResponse>().Publish(GenerateRandom(programData));
        }

        /// <summary>
        /// The RequestProgramDataSaveReceived.
        /// </summary>
        private void RequestProgramDataSaveReceived()
        {
            var prodData = _prodDataViewModel;
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = prodData.SelectedProgramData?.ProgramName +
                                "Run#" + prodData.SelectedProgramData?.Cycles + "_Completed" +
                                DateTime.Now.ToString("yyyy-MM-d--HH-mm-ss") + ".csv";
            string outputPath = Path.Combine(filePath, fileName);

            TextWriter sw = new StreamWriter(outputPath);

            sw.WriteLine(
                nameof(prodData.SelectedProgramData.ProgramName) + "," +
                nameof(prodData.SelectedProgramData.ProductName) + "," +
                nameof(prodData.SelectedProgramData.ProgramCreator) + "," +
                nameof(prodData.SelectedProgramData.AverageCycleTime) + "," +
                nameof(prodData.SelectedProgramData.Cycles) + ",");

            sw.WriteLine(
                prodData.SelectedProgramData?.ProgramName + "," +
                prodData.SelectedProgramData?.ProductName + "," +
                prodData.SelectedProgramData?.ProgramCreator + "," +
                prodData.SelectedProgramData?.AverageCycleTime + "," +
                prodData.SelectedProgramData?.Cycles + ",");

            sw.WriteLine("Title,Time,Status,Sub Steps");

            foreach (Card? card in prodData.CardCollection)
            {
                sw.WriteLine(card?.ToString());
            }

            _eventAggregator.GetEvent<ProgramDataSaveResponse>().Publish();
            sw.Close();
        }

        /// <summary>
        /// The RunProg.
        /// </summary>
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

            var task = Task.Run(
                () =>
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
