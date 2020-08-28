using Prism.Events;
using ProductionCore.Concrete;

namespace ProductionCore.Events
{
    public class ProgramSelectResponse : PubSubEvent<ProgramData?>
    {
    }
}
