using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SharpLib2D.Math;
using SharpLib2D.Resources;

namespace SharpLib2D.Graphics
{
    public static class Line
    {
        #region Draw - Width

        public static void Draw( Vector2 Start, Vector2 End, float Width = 1f )
        {
            if ( Width <= 1f )
            {
                PrimitiveBatch.Begin( );
                {
                    PrimitiveBatch.AddVertex( Start, Color.ActiveColor, Vector2.Zero, PrimitiveType.LineStrip );
                    PrimitiveBatch.AddVertex( End, Color.ActiveColor, Vector2.One, PrimitiveType.LineStrip );
                }
                PrimitiveBatch.End( );
            }
            else
            {
                Vector2 Normal = Vector.Normal( Start, End );

                PrimitiveBatch.Begin( );
                {
                    PrimitiveBatch.AddVertex( Start + Normal * Width * 0.5f, Color.ActiveColor, Vector2.Zero );
                    PrimitiveBatch.AddVertex( End + Normal * Width * 0.5f, Color.ActiveColor, Vector2.Zero );
                    PrimitiveBatch.AddVertex( Start - Normal * Width * 0.5f, Color.ActiveColor, Vector2.Zero );
                    PrimitiveBatch.AddVertex( End - Normal * Width * 0.5f, Color.ActiveColor, Vector2.Zero );
                }
                PrimitiveBatch.End( );
            }
        }

        #endregion

        #region Draw - Points

        public static void DrawConnected( Vector2 [ ] Points, float Width = 1f )
        {
            if ( Points.Length <= 1 )
                return;

            GL.Color4( Color.ActiveColor );
            if ( Width <= 1f )
            {
                PrimitiveBatch.Begin( );
                {
                    for ( int X = 0; X < Points.Length - 1; X++ )
                    {
                        PrimitiveBatch.AddVertex( Points[ X ], Color.ActiveColor, Vector2.Zero, PrimitiveType.LineStrip );
                        //PrimitiveBatch.AddVertex( Points[ X + 1 ], Color.ActiveColor, Vector2.Zero );
                    }
                }
                PrimitiveBatch.End( );
            }
            else
            {
                float Div = Width * 0.5f;

                Vector2 P1 = Points[ 0 ], P2 = Points[ 1 ];
                Vector2 Normal = Vector.Normal( P1, P2 );
                Vector2 PreviousTop = P1 + Normal * Div, PreviousBottom = P1 - Normal * Div;
                PrimitiveBatch.Begin( );
                {
                    for ( int X = 0; X < Points.Length - 1; X++ )
                    {
                        P1 = Points[ X ];
                        P2 = Points[ X + 1 ];
                        Normal = Vector.Normal( P1, P2 );
                        Vector2 Top = P2 + Normal * Div;
                        Vector2 Bottom = P2 - Normal * Div;

                        PrimitiveBatch.AddVertex( PreviousTop, Color.ActiveColor, Vector2.Zero );
                        PrimitiveBatch.AddVertex( Top, Color.ActiveColor, Vector2.Zero );
                        PrimitiveBatch.AddVertex( PreviousBottom, Color.ActiveColor, Vector2.Zero );
                        PrimitiveBatch.AddVertex( Bottom, Color.ActiveColor, Vector2.Zero );

                        PreviousTop = Top;
                        PreviousBottom = Bottom;
                    }
                }
                PrimitiveBatch.End( );
            }
        }

        #endregion

        #region Draw - Bezier Curve

        public static void DrawCubicBezierCurve( Vector2 Start, Vector2 End, Vector2 Anchor1, Vector2 Anchor2, int Points, float Width = 1f )
        {
            Texture.EnableTextures( false );
            Points += 1;
            Vector2[ ] Curve = new Vector2[ Points + 1 ];
            for ( int X = 0; X < Points + 1; X++ )
            {
                float Progress = X / ( float )Points;

                Curve[ X ] = Interpolation.CubicBezier( Start, End, Anchor1, Anchor2, Progress );
            }

            DrawConnected( Curve, Width );
        }

        public static void DrawCubicBezierCurve( BezierCurveCubic Curve, int Points, float Width = 1f )
        {
            Texture.EnableTextures( false );
            Points += 1;
            Vector2[ ] CurvePoints = new Vector2[ Points + 1 ];
            for ( int X = 0; X < Points + 1; X++ )
            {
                float Progress = X / ( float )Points;

                CurvePoints[ X ] = Curve.CalculatePoint( Progress );
            }

            DrawConnected( CurvePoints, Width );
        }

        public static void DrawCubicBezierPath( List<Vector2> Positions, int Points, float Scale = 0.3f, float Width = 1f )
        {
            Texture.EnableTextures( false );
            List<BezierCurveCubic> Cubics = Interpolation.CubicBezierPath( Positions, Scale );
            foreach ( BezierCurveCubic Cubic in Cubics )
            {
                DrawCubicBezierCurve( Cubic, Points, Width );
            }
        }

        #endregion
    }
}
