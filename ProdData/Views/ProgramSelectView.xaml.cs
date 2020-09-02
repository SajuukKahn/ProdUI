using ProdData.ViewModels;
using ProductionCore.Interfaces;
using System.Windows.Controls;

namespace ProdData.Views
{
    /// <summary>
    /// Interaction logic for ProgramSelectView.xaml
    /// </summary>
    public partial class ProgramSelectView : UserControl
    {
        public ProgramSelectView(IProgramSelectViewModel programSelectViewModel)
        {
            InitializeComponent();
            DataContext = programSelectViewModel;
        }
    }
}