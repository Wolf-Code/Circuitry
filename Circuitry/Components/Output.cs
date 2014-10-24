using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuitry.Components
{
    public class Output : IONode
    {
        public Output( NodeType Type, string Name, string Description )
            : base( Type, Name, Description )
        {
            this.Direction = NodeDirection.Out;
        }
    }
}
