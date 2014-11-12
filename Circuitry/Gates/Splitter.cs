
using Circuitry.Components;
using Circuitry.Components.Nodes;
using OpenTK;
using SharpLib2D.Graphics;

namespace Circuitry.Gates
{
    public class Splitter : Gate
    {
        public Splitter( )
            : this( 2 )
        {
            Texture = @"Resources\Textures\Components\Splitter.png";
        }

        public Splitter( int Amount = 2 )
        {
            AddInput( IONode.NodeType.Binary, "Input", "The input to split." );

            for ( int X = 0; X < Amount; X++ )
            {
                AddOutput( IONode.NodeType.Binary, "Output " + ( X + 1 ), "Output #" + ( X + 1 ) );
            }
        }

        public override void OnInputChanged( Input I )
        {
            foreach ( Output O in Outputs )
                SetOutput( O.Name, I.Value );

            base.OnInputChanged( I );
        }

        public override void Draw( FrameEventArgs e )
        {
            DefaultTexturedDraw( );

            base.Draw( e );
        }
    }
}
