using System.Linq;
using Circuitry.Components.Nodes;
using Circuitry.Renderers;

namespace Circuitry.Components.Circuits
{
    public partial class Circuit
    {
        private void DrawConnections( )
        {
            foreach ( Gate E in Children.OfType<Gate>( ) )
            {
                foreach ( Output O in E.Outputs )
                {
                    NodesRenderer.DrawConnection( this, O );
                }

                foreach ( Input I in E.Inputs )
                {
                    NodesRenderer.DrawConnection( this, I.FirstNode );
                }
            }
        }
    }
}
