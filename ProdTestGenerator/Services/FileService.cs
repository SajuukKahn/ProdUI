namespace ProdTestGenerator.Services
{
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Windows.Media.Imaging;
    using Prism.Commands;
    using ProductionCore.Interfaces;
    using ProductionCore.Interfaces.Services;
    using ProductionCore.Services;

    /// <summary>
    /// Defines the <see cref="FileService" />.
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Defines the _playbackService.
        /// </summary>
        private readonly IPlaybackService _playbackService;

        /// <summary>
        /// Defines the _modalService.
        /// </summary>
        private readonly IModalService _modalService;

        /// <summary>
        /// Defines the _mediationService.
        /// </summary>
        private readonly IMediationService _mediationService;

        /// <summary>
        /// Defines the _programDataService.
        /// </summary>
        private readonly IProgramDataService _programDataService;

        /// <summary>
        /// Defines the _cardFactory.
        /// </summary>
        private readonly ICardFactory _cardFactory;

        /// <summary>
        /// Defines the _cardSubStepFactory.
        /// </summary>
        private readonly ICardSubStepFactory _cardSubStepFactory;

        /// <summary>
        /// Defines the _programDataFactory.
        /// </summary>
        private readonly IProgramDataFactory _programDataFactory;

        /// <summary>
        /// Defines the _dateSwitch.
        /// </summary>
        private bool _dateSwitch;

        /// <summary>
        /// Defines the _lastDate.
        /// </summary>
        private DateTime _lastDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        /// <param name="modalService">The modalService<see cref="IModalService"/>.</param>
        /// <param name="mediationService">The mediationService<see cref="IMediationService"/>.</param>
        /// <param name="programDataService">The programDataService<see cref="IProgramDataService"/>.</param>
        /// <param name="cardFactory">The cardFactory<see cref="ICardFactory"/>.</param>
        /// <param name="cardSubStepFactory">The cardSubStepFactory<see cref="ICardSubStepFactory"/>.</param>
        /// <param name="programDataFactory">The programDataFactory<see cref="IProgramDataFactory"/>.</param>
        public FileService(
            IPlaybackService playbackService,
            IModalService modalService,
            IMediationService mediationService,
            IProgramDataService programDataService,
            ICardFactory cardFactory,
            ICardSubStepFactory cardSubStepFactory,
            IProgramDataFactory programDataFactory)
        {
            _playbackService = playbackService;
            _modalService = modalService;
            _mediationService = mediationService;
            _programDataService = programDataService;
            _cardFactory = cardFactory;
            _cardSubStepFactory = cardSubStepFactory;
            _programDataFactory = programDataFactory;
            GlobalCommands.RequestProgram.RegisterCommand(new DelegateCommand(LoadProgramSteps));
            LoadProgramCollection();
        }

        /// <summary>
        /// The LoadProgramSteps.
        /// </summary>
        public void LoadProgramSteps()
        {
            GenerateRandomProgram();
        }

        /// <summary>
        /// The LoadProgramCollection.
        /// </summary>
        public void LoadProgramCollection()
        {
            GenerateRandomProgramCollection();
        }

        /// <summary>
        /// The GenerateRandomProgram.
        /// </summary>
        private void GenerateRandomProgram()
        {
            string[] titleArray = { "PolyLine 3D", "Area", "Move", "Line", "PolyLine", "Arc", "Spiral", "Rectangular Sprial", "Dot", "Part Presense Check" };

            BitmapImage? image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\FID.bmp", UriKind.RelativeOrAbsolute));

            ObservableCollection<ICard?> cards = new ObservableCollection<ICard?>();

            if (_mediationService.CurrentProgram!.ProgramName == "Manual Placement Simulation")
            {
                ICard manualCard = _cardFactory.Create();
                manualCard.CardSubSteps?.Add(_cardSubStepFactory.Create("Place Product", new string[] { string.Empty }));
                manualCard.StepTitle = "Place Product";
                manualCard.StepImage = _mediationService.CurrentProgram.ProductImage;
                manualCard.StepModalData = _modalService.CreateModalData();
                manualCard.StepModalData!.CanAbort = true;
                manualCard.StepModalData!.CanContinue = true;
                manualCard.StepModalData!.CanRetry = false;
                manualCard.StepModalData!.Card = null;
                manualCard.StepModalData!.Instructions = "Place Product and press 'Continue' to begin" + Environment.NewLine + "Press 'Abort' to exit path playback";
                manualCard.StepModalData!.InstructionImage = _mediationService.CurrentProgram.ProductImage;
                manualCard.StepModalData!.IsError = false;
                cards.Add(manualCard);
            }

            ICard fiducialCard = _cardFactory.Create();
            fiducialCard.CardSubSteps?.Add(_cardSubStepFactory.Create("Fiducial A", RandomCoordinates()));
            fiducialCard.CardSubSteps?.Add(_cardSubStepFactory.Create("Fiducial B", RandomCoordinates()));
            fiducialCard.StepTitle = "Fiducial Check";
            fiducialCard.StepImage = image;
            fiducialCard.StepModalData = _modalService.CreateModalData();
            fiducialCard.StepModalData!.CanAbort = true;
            fiducialCard.StepModalData!.CanContinue = true;
            fiducialCard.StepModalData!.CanRetry = true;
            fiducialCard.StepModalData!.Card = null;
            fiducialCard.StepModalData!.Instructions = "Fiducials Failed, Select an option below";
            fiducialCard.StepModalData!.InstructionImage = image;
            fiducialCard.StepModalData!.IsError = true;
            cards.Add(fiducialCard);
            int randSize = new Random().Next(2, 8);

            ICard surfaceCard = _cardFactory.Create();
            for (int i = 0; i < randSize; i++)
            {
                surfaceCard.CardSubSteps?.Add(_cardSubStepFactory.Create("Surface " + (i + 1), RandomCoordinates()));
            }

            surfaceCard.StepTitle = "Surface Height Check";
            surfaceCard.StepModalData = _modalService.CreateModalData();
            surfaceCard.StepModalData!.CanAbort = true;
            surfaceCard.StepModalData!.CanRetry = true;
            surfaceCard.StepModalData!.Card = null;
            surfaceCard.StepModalData!.Instructions = "Surface Height Checks failed, Select an option below";
            surfaceCard.StepModalData!.IsError = true;
            cards.Add(surfaceCard);

            randSize = new Random().Next(4, 12);
            for (int i = 0; i < randSize; i++)
            {
                int randSteps = new Random().Next(1, 8);
                string stepTitle = titleArray[new Random().Next(titleArray.Length)];
                ICard card = _cardFactory.Create();
                card.CardSubSteps?.Add(_cardSubStepFactory.Create(stepTitle, RandomCoordinates()));
                card.StepTitle = stepTitle;
                for (int j = 0; j < randSteps; j++)
                {
                    card.CardSubSteps?.Add(_cardSubStepFactory.Create(stepTitle + " " + (j + 1), RandomCoordinates()));
                }

                if (new Random().Next(0, 4) == 1)
                {
                    card.StepImage = ChooseRandomImage();
                }

                if (stepTitle == "Part Presence Check")
                {
                    card.StepModalData = _modalService.CreateModalData();
                    card.StepModalData!.CanAbort = true;
                    card.StepModalData!.CanContinue = true;
                    card.StepModalData!.CanRetry = true;
                    card.StepModalData!.Card = null;
                    card.StepModalData!.Instructions = "Part Presence Check failed, Select an option below";
                    card.StepModalData!.IsError = true;
                }

                cards.Add(card);
            }

            _playbackService.ProgramSteps = cards;
        }

        /// <summary>
        /// The GenerateRandomProgramCollection.
        /// </summary>
        private void GenerateRandomProgramCollection()
        {
            int randsize = new Random().Next(7, 45);

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
                IProgramData prog = _programDataFactory.Create();
                prog.AverageCycleTime = GenerateRandomCycleTimeAverage();
                prog.CreatedDate = GenerateRandomDateTime();
                prog.Dimensions = new Size(new Random().Next(750), new Random().Next(750));
                prog.Cycles = new Random().Next(0, 9999);
                prog.LastEditDate = GenerateRandomDateTime();
                prog.ProductImage = ChooseRandomImage();
                prog.ProgramName = GenerateRandomString(26) + " Rev_" + (char)new Random().Next(65, 90);
                prog.ProductName = relations[new Random().Next(relations.Length)];
                prog.ProgramCreator = makernames[new Random().Next(makernames.Length)];
                prog.ToolsUsed = GenerateRandomTools();
                prog.UserCanStartPlayback = new Random().Next(4).Equals(1);
                prog.AutoStartPlayback = false;
                prog.IsFavorite = new Random().Next(7).Equals(1);
                if (new Random().Next(0, 5) == 1)
                {
                    prog.Barcode!.BarcodeData = GenerateRandomString(128);
                    prog.Barcode!.BarcodeImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\BAR" + new Random().Next(1, 4).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                }

                _programDataService.ProgramList.Add(prog);
            }

            IProgramData progA = _programDataFactory.Create();
            progA.AverageCycleTime = GenerateRandomCycleTimeAverage();
            progA.CreatedDate = GenerateRandomDateTime();
            progA.Dimensions = new Size(new Random().Next(750), new Random().Next(750));
            progA.Cycles = new Random().Next(0, 9999);
            progA.LastEditDate = GenerateRandomDateTime();
            progA.ProductImage = ChooseRandomImage();
            progA.ProgramName = "Manual Placement Simulation";
            progA.ProductName = relations[new Random().Next(relations.Length)];
            progA.ProgramCreator = makernames[new Random().Next(makernames.Length)];
            progA.ToolsUsed = GenerateRandomTools();
            progA.UserCanStartPlayback = false;
            progA.AutoStartPlayback = true;
            _programDataService.ProgramList.Add(progA);
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
        /// The ChooseRandomImage.
        /// </summary>
        /// <returns>The <see cref="BitmapImage"/>.</returns>
        private BitmapImage ChooseRandomImage()
        {
            return new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
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
