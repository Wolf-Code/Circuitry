using Circuitry.Components;
using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.Binary
{
    public class NOT : Gate
    {
        protected override Vector2 TextPosition
        {
            get { return base.TextPosition - new Vector2( ( Size.X / SizeUnit ) * 7f, 0 ); }
        }

        public NOT( )
        {
            Category = "Logic";
            AddInput( IONode.NodeType.Binary, "Input", "The input." );

            AddOutput( IONode.NodeType.Binary, "Output", "Returns 1 if the input is 0, and 1 otherwise." );
        }

        public override void OnInputChanged( Input I )
        {
            SetOutput( "Output", !GetInput( "Input" ).BinaryValue );
            
            base.OnInputChanged( I );
        }

        protected override void DrawBody( )
        {
            Color.Set( Color4.White );
            Triangle.DrawOutlined( TopLeft, new Vector2( BottomRight.X, Position.Y ),
                new Vector2( TopLeft.X, BottomRight.Y ), Color4.Black, Color4.White, Outline );
        }

        public override void Draw( FrameEventArgs e )
        {
            DefaultDraw( );

            base.Draw( e );
        }
    }
}
