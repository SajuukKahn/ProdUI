namespace ProdData.Views
{
    using System.Windows.Controls;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Interaction logic for ProdModalDialog.xaml.
    /// </summary>
    public partial class ProdModalDialogView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProdModalDialogView"/> class.
        /// </summary>
        /// <param name="prodModalDialogViewModel">The prodModalDialogViewModel<see cref="IProdModalDialogViewModel"/>.</param>
        public ProdModalDialogView(IProdModalDialogViewModel prodModalDialogViewModel)
        {
            InitializeComponent();
            DataContext = prodModalDialogViewModel;
        }
    }
}
