using System.Data;
using Circuitry.Components;
using OpenTK.Graphics;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.Binary
{
    public class NAND : AND
    {
        public NAND( )
        {
            GetOutput( "Output" ).Description = "Returns 0 if both input 1 and 2 are 1, 1 otherwise.";
        }

        public override void OnInputChanged( Input I )
        {
            SetOutput( "Output", !( GetInput( "Input 1" ).BinaryValue && GetInput( "Input 2" ).BinaryValue ) );
        }

        protected override void DrawBody( )
        {
            base.DrawBody( );

            float R = Size.X / 10;
            Circle.DrawOutlined( Position.X + Size.X / 2 + R - Outline, Position.Y, R, Outline, Color4.Black, Color4.White, 16 );
        }
    }
}
