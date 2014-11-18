
namespace Circuitry.Components.Nodes
{
    public class Input : IONode
    {
        /// <summary>
        /// Gets the <see cref="Output"/> this input is connected to.
        /// </summary>
        public Output ConnectedOutput
        {
            get { return FirstNode as Output; }
        }

        /// <summary>
        /// Checks if this input has an <see cref="Output"/> connected to it.
        /// </summary>
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
