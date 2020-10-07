namespace ProdTestGenerator.Services
{
    using ProductionCore.Interfaces;
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="FileService" />.
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Defines the _cardFactory.
        /// </summary>
        private readonly ICardFactory _cardFactory;

        /// <summary>
        /// Defines the _cardSubStepFactory.
        /// </summary>
        private readonly ICardSubStepFactory _cardSubStepFactory;

        /// <summary>
        /// Defines the _playbackService.
        /// </summary>
        private readonly IPlaybackService _playbackService;

        /// <summary>
        /// Defines the _modalService.
        /// </summary>
        private readonly IModalService _modalService;

        /// <summary>
        /// Defines the _programDataService.
        /// </summary>
        private readonly IProgramDataService _programDataService;

        /// <summary>
        /// Defines the _dateSwitch.
        /// </summary>
        private bool _dateSwitch;

        /// <summary>
        /// Defines the _lastDate.
        /// </summary>
        private DateTime _lastDate;

        /// <summary>
        /// Defines the _scaleDataSet.
        /// </summary>
        private readonly int _scaleDataSet = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="cardFactory">The cardFactory<see cref="ICardFactory"/>.</param>
        /// <param name="cardSubStepFactory">The cardSubStepFactory<see cref="ICardSubStepFactory"/>.</param>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        /// <param name="modalService">The modalService<see cref="IModalService"/>.</param>
        /// <param name="programDataService">The programDataService<see cref="IProgramDataService"/>.</param>
        public FileService(ICardFactory cardFactory, ICardSubStepFactory cardSubStepFactory, IPlaybackService playbackService, IModalService modalService, IProgramDataService programDataService)
        {
            _cardFactory = cardFactory;
            _cardSubStepFactory = cardSubStepFactory;
            _playbackService = playbackService;
            _modalService = modalService;
            _programDataService = programDataService;
        }

        /// <summary>
        /// The SaveToJSON.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        public void SaveToJSON(IProgramData program)
        {
        }

        /// <summary>
        /// The RetrieveProgram.
        /// </summary>
        /// <param name="programData">The programData<see cref="IProgramData"/>.</param>
        public void RetrieveProgram(IProgramData programData)
        {
            string[] titleArray = { "PolyLine 3D", "Area", "Move", "Line", "PolyLine", "Arc", "Spiral", "Rectangular Sprial", "Dot", "Part Presense Check" };

            BitmapImage? image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\FID.bmp", UriKind.RelativeOrAbsolute));

            _playbackService!.ProgramSteps?.Clear();

            if (programData?.ProgramName == "Manual Placement Simulation")
            {
                _playbackService!.ProgramSteps?.Add(_cardFactory.Create());
                _playbackService!.ProgramSteps![^1]?.CardSubSteps?.Add(_cardSubStepFactory.Create("Place Product", new string[] { string.Empty }));
                _playbackService!.ProgramSteps![^1]!.StepTitle = "Place Product";
                _playbackService!.ProgramSteps![^1]!.StepImage = programData.ProductImage;
                _playbackService!.ProgramSteps![^1]!.StepModalData = _modalService.CreateModalData();
                _playbackService!.ProgramSteps![^1]!.StepModalData!.CanAbort = true;
                _playbackService!.ProgramSteps![^1]!.StepModalData!.CanContinue = true;
                _playbackService!.ProgramSteps![^1]!.StepModalData!.CanRetry = false;
                _playbackService!.ProgramSteps![^1]!.StepModalData!.Card = null;
                _playbackService!.ProgramSteps![^1]!.StepModalData!.Instructions = "Place Product and press 'Continue' to begin" + Environment.NewLine + "Press 'Abort' to exit path playback";
                _playbackService!.ProgramSteps![^1]!.StepModalData!.InstructionImage = programData.ProductImage;
                _playbackService!.ProgramSteps![^1]!.StepModalData!.IsError = false;
            }

            _playbackService!.ProgramSteps?.Add(_cardFactory.Create());
            _playbackService!.ProgramSteps![^1]?.CardSubSteps?.Add(_cardSubStepFactory.Create("Fiducial A", RandomCoordinates()));
            _playbackService!.ProgramSteps![^1]?.CardSubSteps?.Add(_cardSubStepFactory.Create("Fiducial B", RandomCoordinates()));
            _playbackService!.ProgramSteps![^1]!.StepTitle = "Fiducial Check";
            _playbackService!.ProgramSteps![^1]!.StepImage = image;
            _playbackService!.ProgramSteps![^1]!.StepModalData = _modalService.CreateModalData();
            _playbackService!.ProgramSteps![^1]!.StepModalData!.CanAbort = true;
            _playbackService!.ProgramSteps![^1]!.StepModalData!.CanContinue = true;
            _playbackService!.ProgramSteps![^1]!.StepModalData!.CanRetry = true;
            _playbackService!.ProgramSteps![^1]!.StepModalData!.Card = null;
            _playbackService!.ProgramSteps![^1]!.StepModalData!.Instructions = "Fiducials Failed, Select an option below";
            _playbackService!.ProgramSteps![^1]!.StepModalData!.InstructionImage = image;
            _playbackService!.ProgramSteps![^1]!.StepModalData!.IsError = false;
            int randSize = new Random().Next(2 * _scaleDataSet, 8 * _scaleDataSet);

            _playbackService!.ProgramSteps?.Add(_cardFactory.Create());
            _playbackService!.ProgramSteps![^1]!.StepTitle = "Surface Height Check";
            _playbackService!.ProgramSteps![^1]!.StepModalData = _modalService.CreateModalData();
            _playbackService!.ProgramSteps![^1]!.StepModalData!.CanAbort = true;
            _playbackService!.ProgramSteps![^1]!.StepModalData!.CanRetry = true;
            _playbackService!.ProgramSteps![^1]!.StepModalData!.Card = null;
            _playbackService!.ProgramSteps![^1]!.StepModalData!.Instructions = "Surface Height Checks failed, Select an option below";
            _playbackService!.ProgramSteps![^1]!.StepModalData!.IsError = false;
            for (int i = 0; i < randSize; i++)
            {
                _playbackService!.ProgramSteps![^1]?.CardSubSteps?.Add(_cardSubStepFactory.Create("Surface " + (i + 1), RandomCoordinates()));
            }

            randSize = new Random().Next(4 * _scaleDataSet, 12 * _scaleDataSet);
            for (int i = 0; i < randSize; i++)
            {
                int randSteps = new Random().Next(1, 8 * _scaleDataSet);
                string stepTitle = titleArray[new Random().Next(titleArray.Length)];
                _playbackService!.ProgramSteps?.Add(_cardFactory.Create());
                _playbackService!.ProgramSteps![^1]?.CardSubSteps?.Add(_cardSubStepFactory.Create("Fiducial B", RandomCoordinates()));
                _playbackService!.ProgramSteps![^1]!.StepTitle = stepTitle;
                for (int j = 0; j < randSteps; j++)
                {
                    _playbackService!.ProgramSteps![^1]?.CardSubSteps?.Add(_cardSubStepFactory.Create(stepTitle + " " + (j + 1), RandomCoordinates()));
                }

                if (new Random().Next(0, 4) == 1)
                {
                    image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                    _playbackService!.ProgramSteps![^1]!.StepImage = image;
                }

                if (stepTitle == "Part Presence Check")
                {
                    _playbackService!.ProgramSteps![^1]!.StepModalData = _modalService.CreateModalData();
                    _playbackService!.ProgramSteps![^1]!.StepModalData!.CanAbort = true;
                    _playbackService!.ProgramSteps![^1]!.StepModalData!.CanContinue = true;
                    _playbackService!.ProgramSteps![^1]!.StepModalData!.CanRetry = true;
                    _playbackService!.ProgramSteps![^1]!.StepModalData!.Card = null;
                    _playbackService!.ProgramSteps![^1]!.StepModalData!.Instructions = "Part Presence Check failed, Select an option below";
                    _playbackService!.ProgramSteps![^1]!.StepModalData!.IsError = false;
                }
            }
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
                _programDataService.ProgramList.Add(_programDataService.CreateProgram());
                _programDataService.ProgramList[^1].AverageCycleTime = GenerateRandomCycleTimeAverage();
                _programDataService!.ProgramList[^1].CreatedDate = GenerateRandomDateTime();
                _programDataService!.ProgramList[^1].Dimensions = new Size(new Random().Next(750), new Random().Next(750));
                _programDataService!.ProgramList[^1].Cycles = new Random().Next(0, 9999);
                _programDataService!.ProgramList[^1].LastEditDate = GenerateRandomDateTime();
                _programDataService!.ProgramList[^1].ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                _programDataService!.ProgramList[^1].ProgramName = GenerateRandomString(26) + " Rev_" + (char)new Random().Next(65, 90);
                _programDataService!.ProgramList[^1].ProductName = relations[new Random().Next(relations.Length)];
                _programDataService!.ProgramList[^1].ProgramCreator = makernames[new Random().Next(makernames.Length)];
                _programDataService!.ProgramList[^1].ToolsUsed = GenerateRandomTools();
                _programDataService!.ProgramList[^1].UserCanStartPlayback = new Random().Next(4).Equals(1);
                _programDataService!.ProgramList[^1].AutoStartPlayback = false;
                _programDataService!.ProgramList[^1].IsFavorite = new Random().Next(7).Equals(1);
                if (new Random().Next(0, 5) == 1)
                {
                    _programDataService!.ProgramList[^1].Barcode = _programDataService.CreateBarcode();
                    _programDataService!.ProgramList[^1].Barcode!.BarcodeData = GenerateRandomString(128);
                    _programDataService!.ProgramList[^1].Barcode!.BarcodeImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\BAR" + new Random().Next(1, 4).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                }
            }

            _programDataService.ProgramList.Add(_programDataService.CreateProgram());
            _programDataService!.ProgramList[^1].AverageCycleTime = GenerateRandomCycleTimeAverage();
            _programDataService!.ProgramList[^1].CreatedDate = GenerateRandomDateTime();
            _programDataService!.ProgramList[^1].Dimensions = new Size(new Random().Next(750), new Random().Next(750));
            _programDataService!.ProgramList[^1].Cycles = new Random().Next(0, 9999);
            _programDataService!.ProgramList[^1].LastEditDate = GenerateRandomDateTime();
            _programDataService!.ProgramList[^1].ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
            _programDataService!.ProgramList[^1].ProgramName = "Manual Placement Simulation";
            _programDataService!.ProgramList[^1].ProductName = relations[new Random().Next(relations.Length)];
            _programDataService!.ProgramList[^1].ProgramCreator = makernames[new Random().Next(makernames.Length)];
            _programDataService!.ProgramList[^1].ToolsUsed = GenerateRandomTools();
            _programDataService!.ProgramList[^1].UserCanStartPlayback = false;
            _programDataService!.ProgramList[^1].AutoStartPlayback = true;
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
        /// The GenerateProcessDisplayChangeResponse.
        /// </summary>
        private void GenerateProcessDisplayChangeResponse()
        {
            BitmapImage image = ChooseRandomImage();
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
        /// The RandomCoordinates.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string[] RandomCoordinates()
        {
            return new string[] { new Random().Next(-20000, 200000).ToString(), new Random().Next(-20000, 200000).ToString(), new Random().Next(-9000, 100000).ToString() };
        }
    }
}
