using OpenTK;

namespace Circuitry.Gates.Binary
{
    public class NOT : Components.Gate
    {
        public NOT( )
        {
            this.Category = "Logic";
            this.AddInput( Components.IONode.NodeType.Binary, "Input", "The input." );

            this.AddOutput( Components.IONode.NodeType.Binary, "Output", "Returns 1 if the input is 0, and 1 otherwise." );
        }

        public override void OnInputChanged( Components.Input I )
        {
            this.SetOutput( "Output", !this.GetInput( "Input" ).BinaryValue );
            
            base.OnInputChanged( I );
        }

        public override void Draw( FrameEventArgs e )
        {
            this.DefaultDraw( );

            base.Draw( e );
        }
    }
}
