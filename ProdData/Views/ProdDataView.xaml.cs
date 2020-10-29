namespace ProdData.Views
{
    using System.Windows.Controls;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Interaction logic for ProdData.xaml.
    /// </summary>
    public partial class ProdDataView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProdDataView"/> class.
        /// </summary>
        /// <param name="prodDataViewModel">The prodDataViewModel<see cref="IProdDataViewModel"/>.</param>
        public ProdDataView(IProdDataViewModel prodDataViewModel)
        {
            InitializeComponent();
            DataContext = prodDataViewModel;
        }
    }
}
