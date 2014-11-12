
using Circuitry.Components;
using Circuitry.Components.Nodes;
using OpenTK.Graphics;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.Binary
{
    public class NOR : OR
    {
        public override void OnInputChanged( Input I )
        {
            SetOutput( "Output",
                !( GetInput( "Input 1" ).BinaryValue || GetInput( "Input 2" ).BinaryValue ) );
        }

        protected override void DrawBody( )
        {
            base.DrawBody( );

            float R = Size.X / 10;
            Circle.DrawOutlined( Position.X + Size.X / 2 + R - Outline, Position.Y, R, Outline, Color4.Black, Color4.White, 16 );
        }
    }
}
