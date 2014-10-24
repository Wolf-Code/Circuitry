using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
