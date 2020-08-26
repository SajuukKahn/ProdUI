using Prism.Events;
using ProdData.ViewModels;

namespace ProdData.Events
{
    public class ProgramDataSaveRequest : PubSubEvent<ProdDataViewModel>
    {
    }
}