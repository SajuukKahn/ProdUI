namespace ProdData.Views
{
    using System.Windows.Controls;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
    public partial class ProdModalDialogView : UserControl, IProdModalDialogView
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
