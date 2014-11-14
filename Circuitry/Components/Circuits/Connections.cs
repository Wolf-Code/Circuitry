using System.Linq;
using Circuitry.Renderers;
using SharpLib2D.Info;
using Input = Circuitry.Components.Nodes.Input;

namespace Circuitry.Components.Circuits
{
    public partial class Circuit
    {
        private void DrawConnections( )
        {
            foreach ( Gate E in Children.OfType<Gate>( ) )
            {
                foreach ( Output O in E.Outputs )
                    NodesRenderer.DrawConnection( this, O );

                foreach ( Input I in E.Inputs )
                    NodesRenderer.DrawConnection( this, I.FirstNode );

                if ( Connector.ConnectingNodes )
                    IONode.DrawNode( Mouse.WorldPosition );
            }
        }
    }
}
