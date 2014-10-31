
namespace Circuitry.Components
{
    public class Input : IONode
    {
        public override bool IsInput
        {
            get { return true; }
        }

        public Output ConnectedOutput
        {
            get
            {
                IONode Prev = this.PreviousNode;

                while ( Prev != null && Prev.HasPreviousNode )
                {
                    Prev = Prev.PreviousNode;
                }

                if ( Prev != null ) return Prev as Output;

                return null;
            }
        }

        public bool HasConnectedOutput
        {
            get
            {
                return ConnectedOutput != null;
            }
        }

        public Input( NodeType Type, string Name, string Description )
            : base( Type, Name, Description )
        {
            Direction = NodeDirection.In;
        }
    }
}
