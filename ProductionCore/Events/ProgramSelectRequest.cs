using Prism.Events;
using ProductionCore.Concrete;

namespace ProductionCore.Events
{
    public class ProgramSelectRequest : PubSubEvent<ProgramData?>
    {
    }
}
