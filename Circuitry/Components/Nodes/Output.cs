
namespace Circuitry.Components.Nodes
{
    public class Output : IONode
    {
        /// <summary>
        /// Gets the <see cref="Input"/> connected to this output.
        /// </summary>
        public Input ConnectedInput
        {
            get { return LastNode as Input; }
        }

        /// <summary>
        /// Checks to see if this output is connected to an <see cref="Input"/>.
        /// </summary>
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
