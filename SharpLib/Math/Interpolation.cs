using System.Collections.Generic;
using System.Linq;
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
            return new BezierCurveCubic( Start, End, Anchor1, Anchor2 ).CalculatePoint( Fraction );
        }

        public static List<BezierCurveCubic> CubicBezierPath( List<Vector2> Positions, float Scale = 0.3f )
        {
            int Count = Positions.Count( );
            if ( Count <= 2 )
                return new List<BezierCurveCubic>( );

            var Points = new List<Vector2>( );
            for ( int Q = 0; Q < Count; Q++ )
            {
                if ( Q == 0 )
                {
                    Vector2 p1 = Positions[ Q ];
                    Vector2 p2 = Positions[ Q + 1 ];

                    Vector2 tangent = ( p2 - p1 ).Normalized( );
                    Vector2 q1 = p1 + Scale * tangent;

                    Points.Add( p1 );
                    Points.Add( q1 );
                }
                else if ( Q == Count - 1 )
                {
                    Vector2 p0 = Positions[ Q - 1 ];
                    Vector2 p1 = Positions[ Q ];
                    Vector2 tangent = ( p1 - p0 ).Normalized( );
                    Vector2 q0 = p1 - Scale * tangent;

                    Points.Add( q0 );
                    Points.Add( p1 );
                }
                else
                {
                    Vector2 p0 = Positions[ Q - 1 ];
                    Vector2 p1 = Positions[ Q ];
                    Vector2 p2 = Positions[ Q + 1 ];
                    Vector2 tangent = ( p2 - p0 ).Normalized( );
                    Vector2 q0 = p1 - Scale * tangent * ( p1 - p0 ).Length;
                    Vector2 q1 = p1 + Scale * tangent * ( p2 - p1 ).Length;

                    Points.Add( q0 );
                    Points.Add( p1 );
                    Points.Add( q1 );
                }
            }

            List<BezierCurveCubic> Cubics = new List<BezierCurveCubic>( );
            for ( int Q = 0; Q < Points.Count - 3; Q += 3 )
            {
                Cubics.Add( 
                    new BezierCurveCubic( 
                        Points[ Q ], 
                        Points[ Q + 3 ], 
                        Points[ Q + 1 ],
                        Points[ Q + 2 ] 
                    )
                );
            }

            return Cubics;
        }

        public static List<BezierCurveCubic> CubicBezierPath( Vector2 StartControl, Vector2 EndControl, List<Vector2> Positions, float Scale = 0.3f )
        {
            int Count = Positions.Count( );
            if ( Count <= 2 )
                return new List<BezierCurveCubic>( );

            var Points = new List<Vector2>( );
            for ( int Q = 0; Q < Count; Q++ )
            {
                if ( Q == 0 )
                {
                    Vector2 p1 = Positions[ Q ];

                    Points.Add( p1 );
                    Points.Add( StartControl );
                }
                else if ( Q == Count - 1 )
                {
                    Vector2 p1 = Positions[ Q ];

                    Points.Add( EndControl );
                    Points.Add( p1 );
                }
                else
                {
                    Vector2 p0 = Positions[ Q - 1 ];
                    Vector2 p1 = Positions[ Q ];
                    Vector2 p2 = Positions[ Q + 1 ];
                    Vector2 tangent = ( p2 - p0 ).Normalized( );
                    Vector2 q0 = p1 - Scale * tangent * ( p1 - p0 ).Length;
                    Vector2 q1 = p1 + Scale * tangent * ( p2 - p1 ).Length;

                    Points.Add( q0 );
                    Points.Add( p1 );
                    Points.Add( q1 );
                }
            }

            List<BezierCurveCubic> Cubics = new List<BezierCurveCubic>( );
            for ( int Q = 0; Q < Points.Count - 3; Q += 3 )
            {
                Cubics.Add(
                    new BezierCurveCubic(
                        Points[ Q ],
                        Points[ Q + 3 ],
                        Points[ Q + 1 ],
                        Points[ Q + 2 ]
                    )
                );
            }

            return Cubics;
        }

        public static Vector2 CubicBezier( float Fraction, params Vector2[ ] Points )
        {
            BezierCurve C = new BezierCurve( Points );
            return C.CalculatePoint( Fraction );
        }
    }
}
