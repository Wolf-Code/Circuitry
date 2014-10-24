
namespace Circuitry.Gates.Binary
{
    public class Divider : Components.Gate
    {
        public Divider( )
            : this( 2 )
        {

        }

        public Divider( int Amount = 2 )
        {
            this.AddInput( Components.IONode.NodeType.Binary, "Input", "The input to divide" );

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
            this.DrawIOConnectors( );

            SharpLib2D.Graphics.Color.Set( 0.3f, 0.3f, 0.3f, 1f );
            SharpLib2D.Graphics.Rectangle.Draw( TopLeft.X, TopLeft.Y, Size.X, Size.Y );
            base.Draw( e );
        }
    }
}
