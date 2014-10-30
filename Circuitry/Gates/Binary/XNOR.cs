
using OpenTK.Graphics;

namespace Circuitry.Gates.Binary
{
    public class XNOR : XOR
    {
        public XNOR( )
        {
            this.GetOutput( "Output" ).Description = "Returns 0 if either input is one, but not both.";
        }

        public override void OnInputChanged( Components.Input I )
        {
            bool B1 = this.GetInput( "Input 1" ).BinaryValue, B2 = this.GetInput( "Input 2" ).BinaryValue;
            this.SetOutput( "Output", B1 == B2 );
        }

        protected override void DrawBody( )
        {
            base.DrawBody( );

            float R = this.Size.X / 10;
            SharpLib2D.Graphics.Circle.DrawOutlined( this.Position.X + this.Size.X / 2 + R - this.Outline, this.Position.Y, R, this.Outline, Color4.Black, Color4.White, 16 );
        }
    }
}
