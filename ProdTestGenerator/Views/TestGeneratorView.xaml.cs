namespace ProdTestGenerator.Views
{
    using ProductionCore.Interfaces;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TestGeneratorView.xaml.
    /// </summary>
    public partial class TestGeneratorView : UserControl
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
