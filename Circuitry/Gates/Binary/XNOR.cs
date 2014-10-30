
using Circuitry.Components;
using OpenTK.Graphics;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.Binary
{
    public class XNOR : XOR
    {
        public XNOR( )
        {
            GetOutput( "Output" ).Description = "Returns 0 if either input is one, but not both.";
        }

        public override void OnInputChanged( Input I )
        {
            bool B1 = GetInput( "Input 1" ).BinaryValue, B2 = GetInput( "Input 2" ).BinaryValue;
            SetOutput( "Output", B1 == B2 );
        }

        protected override void DrawBody( )
        {
            base.DrawBody( );

            float R = Size.X / 10;
            Circle.DrawOutlined( Position.X + Size.X / 2 + R - Outline, Position.Y, R, Outline, Color4.Black, Color4.White, 16 );
        }
    }
}
