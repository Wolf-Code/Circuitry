
namespace Circuitry.Components
{
    public class Output : IONode
    {
        public override bool IsOutput
        {
            get { return true; }
        }

        public Input ConnectedInput
        {
            get { return this.LastNode as Input; }
        }

        public bool HasConnectedInput
        {
            get { return ConnectedInput != null; }
        }

        public Output( NodeType Type, string Name, string Description )
            : base( Type, NodeDirection.Out, Name, Description )
        {
            
        }
    }
}
