
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
            get { return FirstNode as Output; }
        }

        public bool HasConnectedOutput
        {
            get { return ConnectedOutput != null; }
        }

        public Input( NodeType Type, string Name, string Description )
            : base( Type, NodeDirection.In, Name, Description )
        {
            
        }
    }
}
