
using Circuitry.Components;
using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.Binary
{
    public class AND : Gate
    {
        public AND( )
        {
            Category = "Logic";

            AddInput( IONode.NodeType.Binary, "Input 1", "The first input." );
            AddInput( IONode.NodeType.Binary, "Input 2", "The second input." );

            AddOutput( IONode.NodeType.Binary, "Output", "Returns 1 if both input 1 and 2 are 1, 0 otherwise." );
        }

        public override void OnInputChanged( Input I )
        {
            SetOutput( "Output", GetInput( "Input 1" ).BinaryValue && GetInput( "Input 2" ).BinaryValue );
            
            base.OnInputChanged( I );
        }

        public override void Draw( FrameEventArgs e )
        {
            this.DefaultDraw( );
            base.Draw( e );
        }
    }
}
