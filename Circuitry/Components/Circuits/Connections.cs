using System.Linq;
using Circuitry.Components.Nodes;
using Circuitry.Renderers;
using OpenTK.Graphics;
using Mouse = SharpLib2D.Info.Mouse;

namespace Circuitry.Components.Circuits
{
    public partial class Circuit
    {
        private void DrawConnection( IONode Start )
        {
            IONode Cur = Start;
            IONode Next = Start.NextNode;
            Color4 C = Cur.BinaryValue ? Color4.LimeGreen : Color4.Blue;

            while ( Next != null )
            {
                NodesRenderer.DrawLine( C, Cur.Position, Next.Position );

                Cur = Next;
                Next = Next.NextNode;
            }

            if ( !Connector.ConnectingNodes ) return;

            if ( Cur == Connector.ConnectionNode )
                NodesRenderer.DrawLine( C, Cur.Position, Mouse.WorldPosition );
            else if ( Start == Connector.ConnectionNode )
                NodesRenderer.DrawLine( C, Start.Position, Mouse.WorldPosition );

            IONode.DrawNode( Mouse.WorldPosition );
        }

        private void DrawConnections( )
        {
            foreach ( Gate E in Children.OfType<Gate>( ) )
            {
                foreach ( Output O in E.Outputs )
                {
                    DrawConnection( O );
                }

                foreach ( Input I in E.Inputs )
                {
                    DrawConnection( I.FirstNode );
                }
            }
        }
    }
}
