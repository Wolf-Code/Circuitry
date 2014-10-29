
using OpenTK.Graphics;

namespace Circuitry.Gates.Binary
{
    public class NOR : OR
    {
        public override void OnInputChanged( Components.Input I )
        {
            this.SetOutput( "Output",
                !( this.GetInput( "Input 1" ).BinaryValue || this.GetInput( "Input 2" ).BinaryValue ) );
        }

        protected override void DrawBody( )
        {
            base.DrawBody( );

            float R = this.Size.X / 10;
            SharpLib2D.Graphics.Circle.DrawOutlined( this.Position.X + this.Size.X / 2 + R - this.Outline, this.Position.Y, R, this.Outline, Color4.Black, Color4.White, 16 );
        }
    }
}
