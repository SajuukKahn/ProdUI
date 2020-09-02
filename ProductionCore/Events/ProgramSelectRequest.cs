using Prism.Events;
using ProductionCore.Concrete;
using ProductionCore.Interfaces;

namespace ProductionCore.Events
{
    public class ProgramSelectRequest : PubSubEvent<IProgramData?>
    {
    }
}
