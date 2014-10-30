
using OpenTK;
using OpenTK.Graphics;

namespace Circuitry.Gates.Binary
{
    public class XOR : OR
    {
        private const float OffsetPercentage = 0.2f;

        public XOR( )
        {
            this.GetOutput( "Output" ).Description = "Returns 1 if either input is one, but not both.";
        }

        public override void OnInputChanged( Components.Input I )
        {
            bool B1 = this.GetInput( "Input 1" ).BinaryValue, B2 = this.GetInput( "Input 2" ).BinaryValue;
            this.SetOutput( "Output", B1 != B2 );
        }

        protected override void DrawBody( )
        {
            base.DrawBody( );

            float D = this.Size.X * OffsetPercentage;

            Vector2 DVector = new Vector2( D, 0 );
            Vector2 Start = this.TopLeft - DVector;
            Vector2 End = new Vector2( this.TopLeft.X - D, this.BottomRight.Y );

            SharpLib2D.Graphics.Color.Set( Color4.Black );
            SharpLib2D.Graphics.Line.DrawCubicBezierCurve( Start, End, Start + DVector, End + DVector, 5, 5f );

            SharpLib2D.Graphics.Color.Set( Color4.White );
            SharpLib2D.Graphics.Line.DrawCubicBezierCurve( Start, End, Start + DVector, End + DVector, 5, 2f );
        }
    }
}
