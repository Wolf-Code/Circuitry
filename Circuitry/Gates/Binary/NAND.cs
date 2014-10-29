using System.Data;
using OpenTK.Graphics;

namespace Circuitry.Gates.Binary
{
    public class NAND : AND
    {
        public NAND( )
        {
            this.GetOutput( "Output" ).Description = "Returns 0 if both input 1 and 2 are 1, 1 otherwise.";
        }

        public override void OnInputChanged( Components.Input I )
        {
            this.SetOutput( "Output", !( this.GetInput( "Input 1" ).BinaryValue && this.GetInput( "Input 2" ).BinaryValue ) );
        }

        protected override void DrawBody( )
        {
            base.DrawBody( );

            float R = this.Size.X / 10;
            SharpLib2D.Graphics.Circle.DrawOutlined( this.Position.X + this.Size.X / 2 + R - this.Outline, this.Position.Y, R, this.Outline, Color4.Black, Color4.White, 16 );
        }
    }
}
