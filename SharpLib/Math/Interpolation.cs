using System;
using System.Collections.Generic;
using OpenTK;

namespace SharpLib2D.Math
{
    public static class Interpolation
    {
        public static Vector2 Linear( Vector2 Start, Vector2 End, float Fraction )
        {
            return Start + ( End - Start ) * Fraction;
        }

        public static Vector2 CubicBezier( Vector2 Start, Vector2 End, Vector2 Anchor1, Vector2 Anchor2, float Fraction )
        {
            return new BezierCurveCubic( Anchor1, Anchor2, Start, End ).CalculatePoint( Fraction );
        }

        public static Vector2 CubicBezier( float Fraction, params Vector2[ ] Points )
        {
            BezierCurve C = new BezierCurve( Points );
            return C.CalculatePoint( Fraction );
        }
    }
}
