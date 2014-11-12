
using Circuitry.Components.Nodes;

namespace Circuitry.Components
{
    public class Signal
    {
        public Output Out
        {
            private set;
            get;
        }

        public Input In
        {
            private set;
            get;
        }

        public Signal( Output Out, Input In )
        {
            this.Out = Out;
            this.In = In;
        }
    }
}
