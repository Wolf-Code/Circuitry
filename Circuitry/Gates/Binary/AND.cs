using Circuitry.Components;
using Gwen.Control.Property;
using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;
using Color = SharpLib2D.Graphics.Color;

namespace Circuitry.Gates.Binary
{
    public class AND : Gate
    {
        private const float EdgeSizePercentage = 0.5f;

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

        protected override void DrawBody( )
        {
            float W = Size.X * EdgeSizePercentage;

            Color.Set( Color4.Black );
            Rectangle.DrawRoundedOutlined( TopLeft.X, TopLeft.Y, Size.X, Size.Y, Color4.Black, Color4.White, Outline, ( int )W, 8, false, true, true, false );
        }

        public override void Draw( FrameEventArgs e )
        {
            DefaultDraw( );
            base.Draw( e );
        }
    }
}
