namespace ProdData.Views
{
    using System.Windows.Controls;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Interaction logic for ProgramSelectView.xaml.
    /// </summary>
    public partial class ProgramSelectView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramSelectView"/> class.
        /// </summary>
        /// <param name="programSelectViewModel">The programSelectViewModel<see cref="IProgramSelectViewModel"/>.</param>
        public ProgramSelectView(IProgramSelectViewModel programSelectViewModel)
        {
            InitializeComponent();
            DataContext = programSelectViewModel;
        }
    }
}
