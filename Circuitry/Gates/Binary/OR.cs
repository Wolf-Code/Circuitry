
using Circuitry.Components;
using OpenTK;

namespace Circuitry.Gates.Binary
{
    public class OR : Gate
    {
        public OR( )
        {
            Category = "Logic";

            Texture = @"Resources\Textures\Components\OR.png";

            AddInput( IONode.NodeType.Binary, "Input 1", "The first input." );
            AddInput( IONode.NodeType.Binary, "Input 2", "The second input." );

            AddOutput( IONode.NodeType.Binary, "Output", "Returns 1 if either input is 1 or if they're both 1, 0 otherwise." );
        }

        public override void OnInputChanged( Input I )
        {
            SetOutput( "Output", GetInput( "Input 1" ).BinaryValue || GetInput( "Input 2" ).BinaryValue );
            
            base.OnInputChanged( I );
        }

        protected override void DrawBody( )
        {
            DefaultTexturedDraw( );
        }

        public override void Draw( FrameEventArgs e )
        {
            DefaultDraw( );

            base.Draw( e );
        }
    }
}
