namespace ProdProgramSelect.Views
{
    using System.Windows.Controls;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
    public partial class ProgramSelectView : UserControl, IProgramSelectView
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
