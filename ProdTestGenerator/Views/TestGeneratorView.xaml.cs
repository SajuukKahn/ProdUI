namespace ProdTestGenerator.Views
{
    using System.Windows.Controls;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
    public partial class TestGeneratorView : UserControl, ITestGeneratorView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestGeneratorView"/> class.
        /// </summary>
        /// <param name="testGeneratorViewModel">The testGeneratorViewModel<see cref="ITestGeneratorViewModel"/>.</param>
        public TestGeneratorView(ITestGeneratorViewModel testGeneratorViewModel)
        {
            InitializeComponent();
            DataContext = testGeneratorViewModel;
        }
    }
}
