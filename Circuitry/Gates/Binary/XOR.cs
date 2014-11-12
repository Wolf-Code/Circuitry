
using Circuitry.Components;
using Circuitry.Components.Nodes;
using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.Binary
{
    public class XOR : OR
    {
        private const float OffsetPercentage = 0.2f;

        public XOR( )
        {
            GetOutput( "Output" ).Description = "Returns 1 if either input is one, but not both.";
        }

        public override void OnInputChanged( Input I )
        {
            bool B1 = GetInput( "Input 1" ).BinaryValue, B2 = GetInput( "Input 2" ).BinaryValue;
            SetOutput( "Output", B1 != B2 );
        }

        protected override void DrawBody( )
        {
            base.DrawBody( );

            float D = Size.X * OffsetPercentage;

            Vector2 DVector = new Vector2( D, 0 );
            Vector2 Start = TopLeft - DVector;
            Vector2 End = new Vector2( TopLeft.X - D, BottomRight.Y );

            Color.Set( Color4.Black );
            Line.DrawCubicBezierCurve( Start, End, Start + DVector, End + DVector, 5, 5f );

            Color.Set( Color4.White );
            Line.DrawCubicBezierCurve( Start, End, Start + DVector, End + DVector, 5, 2f );
        }
    }
}
