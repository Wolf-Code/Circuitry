using OpenTK;

namespace SharpLib2D.Math
{
    class Interpolation
    {
        public static Vector2 Linear( Vector2 Start, Vector2 End, float Fraction )
        {
            return Start + ( End - Start ) * Fraction;
        }

        public static Vector2 Bezier( Vector2 Start, Vector2 End, Vector2 Anchor1, Vector2 Anchor2, float Fraction )
        {
            return ( float )System.Math.Pow( ( 1f - Fraction ), 3 ) * Start +
                    3 * ( float )System.Math.Pow( ( 1f - Fraction ), 2 ) * Fraction * Anchor1 +
                    3 * ( 1f - Fraction ) * ( Fraction * Fraction ) * Anchor2 +
                    ( Fraction * Fraction * Fraction ) * End;
        }
    }
}
