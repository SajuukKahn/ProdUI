namespace ProdProgramSelect.Services
{
    using Prism.Mvvm;
    using ProductionCore.Interfaces;
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="ProgramDataService" />.
    /// </summary>
    public class ProgramDataService : BindableBase, IProgramDataService
    {
        /// <summary>
        /// Defines the _programDataFactory.
        /// </summary>
        private readonly IProgramDataFactory _programDataFactory;

        /// <summary>
        /// Defines the _barcodeFactory.
        /// </summary>
        private readonly IBarcodeFactory _barcodeFactory;

        /// <summary>
        /// Defines the _dateSwitch.
        /// </summary>
        private bool _dateSwitch;

        /// <summary>
        /// Defines the _lastDate.
        /// </summary>
        private DateTime _lastDate;

        /// <summary>
        /// Defines the _canCancel.
        /// </summary>
        private bool _canCancel;

        /// <summary>
        /// Defines the _canConfirm.
        /// </summary>
        private bool _canConfirm;

        /// <summary>
        /// Defines the _programRequestOpen.
        /// </summary>
        private bool _programRequestOpen;

        /// <summary>
        /// Defines the _selectedProgramData.
        /// </summary>
        private IProgramData? _selectedProgramData;

        /// <summary>
        /// Defines the _currentProgram.
        /// </summary>
        private IProgramData? _currentProgram;

        /// <summary>
        /// Defines the _programList.
        /// </summary>
        private ObservableCollection<IProgramData> _programList = new ObservableCollection<IProgramData>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramDataService"/> class.
        /// </summary>
        /// <param name="programDataFactory">The programDataFactory<see cref="IProgramDataFactory"/>.</param>
        /// <param name="barcodeFactory">The barcodeFactory<see cref="IBarcodeFactory"/>.</param>
        public ProgramDataService(IProgramDataFactory programDataFactory, IBarcodeFactory barcodeFactory)
        {
            _programDataFactory = programDataFactory;
            _barcodeFactory = barcodeFactory;
            RetrieveProgramCollection();
        }

        /// <summary>
        /// Gets or sets the ProgramList.
        /// </summary>
        public ObservableCollection<IProgramData> ProgramList
        {
            get
            {
                return _programList;
            }

            set
            {
                SetProperty(ref _programList, value);
            }
        }

        /// <summary>
        /// Gets or sets the SelectedProgramData.
        /// </summary>
        public IProgramData? SelectedProgramData
        {
            get
            {
                return _selectedProgramData;
            }

            set
            {
                SetProperty(ref _selectedProgramData, value, SetConfirmStatus);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ProgramRequestShow.
        /// </summary>
        public bool ProgramRequestShow
        {
            get
            {
                return _programRequestOpen;
            }

            set
            {
                SetProperty(ref _programRequestOpen, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanCancel.
        /// </summary>
        public bool CanCancel
        {
            get
            {
                return _canCancel;
            }

            set
            {
                SetProperty(ref _canCancel, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanConfirm.
        /// </summary>
        public bool CanConfirm
        {
            get
            {
                return _canConfirm;
            }

            private set
            {
                SetProperty(ref _canConfirm, value);
            }
        }

        /// <summary>
        /// Gets or sets the CurrentProgram.
        /// </summary>
        public IProgramData? CurrentProgram
        {
            get
            {
                return _currentProgram;
            }

            set
            {
                SetProperty(ref _currentProgram, value, SetCancelStatus);
            }
        }

        /// <summary>
        /// The RetrieveProgramCollection.
        /// </summary>
        public void RetrieveProgramCollection()
        {
            GenerateRandomProgramCollection();
        }

        /// <summary>
        /// The UpdateProgramCycleTime.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        public void IterateProgramCycles(IProgramData? program)
        {
            if (program == null)
            {
                program = CurrentProgram ?? null;
                if (program == null)
                {
                    return;
                }
            }

            program.Cycles++;
        }

        /// <summary>
        /// The UpdateProgramAverageCycleTime.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        /// <param name="cycleTime">The cycleTime<see cref="TimeSpan"/>.</param>
        public void UpdateProgramAverageCycleTime(IProgramData? program, TimeSpan cycleTime)
        {
            if (program == null)
            {
                program = CurrentProgram ?? null;
                if (program == null)
                {
                    return;
                }
            }

            program.AverageCycleTime = default(TimeSpan).Add(program.AverageCycleTime.Add(cycleTime)).Divide(2);
        }

        /// <summary>
        /// The ProgramSelectClose.
        /// </summary>
        public void ProgramSelectClose()
        {
            _programRequestOpen = false;
        }

        /// <summary>
        /// The Program.
        /// </summary>
        /// <returns>The <see cref="IProgramData"/>.</returns>
        public IProgramData CreateProgram()
        {
            return _programDataFactory.Create();
        }

        /// <summary>
        /// The CreateBarcode.
        /// </summary>
        /// <returns>The <see cref="IBarcode"/>.</returns>
        public IBarcode CreateBarcode()
        {
            return _barcodeFactory.Create();
        }

        /// <summary>
        /// The SetSelectedProgramAsCurrent.
        /// </summary>
        public void SetSelectedProgramAsCurrent()
        {
            CurrentProgram = SelectedProgramData;
        }

        /// <summary>
        /// The SaveProgram.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        public void SaveProgram(IProgramData? program = null)
        {
            if (program == null && CurrentProgram == null)
            {
                return;
            }
            else if (program == null)
            {
                program = CurrentProgram ?? null;
            }
        }

        /// <summary>
        /// The SetCancelStatus.
        /// </summary>
        private void SetCancelStatus()
        {
            if (_currentProgram == null)
            {
                CanCancel = false;
            }
            else
            {
                CanCancel = true;
            }
        }

        /// <summary>
        /// The SetConfirmStatus.
        /// </summary>
        private void SetConfirmStatus()
        {
            if (_selectedProgramData == null)
            {
                CanConfirm = false;
            }
            else
            {
                CanConfirm = true;
            }
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
                IProgramData prog = CreateProgram();
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
                    prog.Barcode = CreateBarcode();
                    prog.Barcode!.BarcodeData = GenerateRandomString(128);
                    prog.Barcode!.BarcodeImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\BAR" + new Random().Next(1, 4).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                }

                ProgramList.Add(prog);
            }

            IProgramData progA = CreateProgram();
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
            ProgramList.Add(progA);
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
    }
}
