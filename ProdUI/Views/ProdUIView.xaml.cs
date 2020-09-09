using Prism.Services.Dialogs;
using System.Windows;

namespace ProdUI.Views
{
    /// <summary>
    /// Interaction logic for ProdUIShell.xaml
    /// </summary>
    public partial class ProdUIView : Window
    {
        public ProdUIView()
        {
            InitializeComponent();
            //TODO I would like to work towards making the different views based on scaling / some part of a constructor
            //  This seems like a lot of work, and I don't know a good way to handle it
        }
    }
}