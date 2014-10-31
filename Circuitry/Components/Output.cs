
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
            get
            {
                IONode Next = this.NextNode;

                while ( Next != null && Next.HasNextNode )
                {
                    Next = Next.NextNode;
                }

                if ( Next != null ) return Next as Input;

                return null;
            }
        }

        public bool HasConnectedInput
        {
            get { return ConnectedInput != null; }
        }

        public Output( NodeType Type, string Name, string Description )
            : base( Type, Name, Description )
        {
            Direction = NodeDirection.Out;
        }
    }
}
