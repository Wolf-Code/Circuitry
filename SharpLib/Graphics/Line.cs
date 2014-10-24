using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SharpLib2D.Graphics
{
    public static class Line
    {
        #region Draw - Width

        public static void Draw( Vector2 Start, Vector2 End, float Width = 1f )
        {
            GL.Color4( Color.ActiveColor );
            if ( Width <= 1f )
            {
                GL.Begin( PrimitiveType.Lines );
                {
                    GL.Vertex2( Start );
                    GL.Vertex2( End );
                }
                GL.End( );
            }
            else
            {
                Vector2 Normal = Math.Vector.Normal( Start, End );

                GL.Begin( PrimitiveType.Quads );
                {
                    GL.Vertex2( Start + Normal * Width * 0.5f );
                    GL.Vertex2( End + Normal * Width * 0.5f );
                    GL.Vertex2( End - Normal * Width * 0.5f );
                    GL.Vertex2( Start - Normal * Width * 0.5f );
                }
                GL.End( );
            }
        }

        #endregion

        #region Draw - Points

        public static void DrawConnected( Vector2[ ] Points, float Width = 1f )
        {
            if ( Points.Length <= 1 )
                return;

            GL.Color4( Color.ActiveColor );
            if ( Width <= 1f )
            {
                GL.Begin( PrimitiveType.Lines );
                {
                    for ( int X = 0; X < Points.Length - 1; X++ )
                    {
                        GL.Vertex2( Points[ X ] );
                        GL.Vertex2( Points[ X + 1 ] );
                    }
                }
                GL.End( );
            }
            else
            {
                float Div = Width * 0.5f;
                GL.Begin( PrimitiveType.Quads );
                {
                    Vector2 P1 = Points[ 0 ], P2 = Points[ 1 ];
                    Vector2 Normal = Math.Vector.Normal( P1, P2 );
                    Vector2 PreviousTop = P1 + Normal * Div, PreviousBottom = P1 - Normal * Div;

                    for ( int X = 0; X < Points.Length - 1; X++ )
                    {
                        P1 = Points[ X ];
                        P2 = Points[ X + 1 ];
                        Normal = Math.Vector.Normal( P1, P2 );
                        Vector2 Top = P2 + Normal * Div;
                        Vector2 Bottom = P2 - Normal * Div;

                        GL.Vertex2( PreviousTop );
                        GL.Vertex2( Top );
                        GL.Vertex2( Bottom );
                        GL.Vertex2( PreviousBottom );

                        PreviousTop = Top;
                        PreviousBottom = Bottom;
                    }
                }
                GL.End( );
            }
        }

        #endregion

        #region Draw - Bezier Curve

        public static void DrawCubicBezierCurve( Vector2 Start, Vector2 End, Vector2 Anchor1, Vector2 Anchor2, int Points, float Width = 1f )
        {
            Points += 1;
            Vector2[ ] Curve = new Vector2[ Points + 1 ];
            for ( int X = 0; X < Points + 1; X++ )
            {
                float Progress = X / ( float )Points;
                Vector2 Point = ( float )System.Math.Pow( ( 1f - Progress ), 3 ) * Start +
                                3 * ( float )System.Math.Pow( ( 1f - Progress ), 2 ) * Progress * Anchor1 +
                                3 * ( 1f - Progress ) * ( Progress * Progress ) * Anchor2 +
                                ( Progress * Progress * Progress ) * End;

                Curve[ X ] = Point;
            }

            DrawConnected( Curve, Width );
        }

        #endregion
    }
}
