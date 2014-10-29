
using SharpLib2D.Graphics;

namespace Circuitry.Gates
{
    public class Splitter : Components.Gate
    {
        public Splitter( )
            : this( 2 )
        {
            this.Texture = @"Resources\Textures\Components\Splitter.png";
        }

        public Splitter( int Amount = 2 )
        {
            this.AddInput( Components.IONode.NodeType.Binary, "Input", "The input to split." );

            for ( int X = 0; X < Amount; X++ )
            {
                this.AddOutput( Components.IONode.NodeType.Binary, "Output " + ( X + 1 ), "Output #" + ( X + 1 ) );
            }
        }

        public override void OnInputChanged( Components.Input I )
        {
            foreach ( Components.Output O in this.Outputs )
                this.SetOutput( O.Name, I.Value );

            base.OnInputChanged( I );
        }

        public override void Draw( OpenTK.FrameEventArgs e )
        {
            this.DefaultTexturedDraw( );

            base.Draw( e );
        }
    }
}
