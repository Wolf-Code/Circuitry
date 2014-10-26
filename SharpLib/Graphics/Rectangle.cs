using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SharpLib2D.Graphics
{
    public static class Rectangle
    {
        #region DrawRect

        private static void DrawRect( float X, float Y, float W, float H, float U1 = 0f, float V1 = 0f, float U2 = 1f, float V2 = 1f )
        {
            PrimitiveBatch.Begin( );
            {
                PrimitiveBatch.AddVertex( new Vector2( X, Y + H ), Color.ActiveColor, new Vector2( U1, V2) );
                PrimitiveBatch.AddVertex( new Vector2( X, Y ), Color.ActiveColor, new Vector2( U1, V1 ) );
                PrimitiveBatch.AddVertex( new Vector2( X + W, Y + H ), Color.ActiveColor, new Vector2( U2, V2 ) );
                PrimitiveBatch.AddVertex( new Vector2( X + W, Y ), Color.ActiveColor, new Vector2( U2, V1 ) );
            }
            PrimitiveBatch.End( );
        }

        #endregion

        #region DrawRoundedRect

        public static void DrawRounded( float X, float Y, float W, float H, int Size, int Quality )
        {
            if ( Size < 0 )
                Size = 0;

            if ( Size == 0 )
            {
                DrawRect( X, Y, W, H );
                return;
            }

            Vector2 TopLeft = new Vector2( X + Size, Y + Size );
            Vector2 BottomRight = new Vector2( X + W - Size, Y + H - Size );

            // Center
            DrawRect( X, TopLeft.Y, W, H - Size * 2 );

            // Top
            DrawRect( X + Size, Y, W - Size * 2, Size );

            // Bottom
            DrawRect( X + Size, Y + H - Size, W - Size * 2, Size );

            float PiDiv = ( float )( System.Math.PI / 2f / Quality );

            // Top Left
            for ( int Q = 0; Q < 4; Q++ )
            {
                Vector2 Start = TopLeft;
                int MulX = -1, MulY = -1;

                switch ( Q )
                {
                        // Top Right
                    case 1:
                        Start = new Vector2( BottomRight.X, TopLeft.Y );
                        MulX = 1;
                        break;

                        // Bottom Right
                    case 2:
                        Start = BottomRight;
                        MulX = 1;
                        MulY = 1;
                        break;

                        // Bottom Left
                    case 3:
                        Start = new Vector2( TopLeft.X, BottomRight.Y );
                        MulY = 1;
                        break;
                }

                PrimitiveBatch.Begin( );
                {
                    PrimitiveBatch.AddVertex( Start, Color.ActiveColor, Vector2.Zero,
                        PrimitiveType.TriangleFan );
                    for ( int I = 0; I < Quality + 1; I++ )
                    {
                        float Cx = ( float ) System.Math.Cos( I * PiDiv );
                        float Cy = ( float ) System.Math.Sin( I * PiDiv );

                        PrimitiveBatch.AddVertex( Start + new Vector2( Cx * Size * MulX, Cy * Size * MulY ),
                            Color.ActiveColor,
                            Vector2.Zero,
                            PrimitiveType.TriangleFan );
                    }
                }
                PrimitiveBatch.End( );
            }
        }

        #endregion

        #region 2D Quad

        /// <summary>
        /// Draws a 2D quad.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        public static void Draw( float X, float Y, float W, float H )
        {
            Texture.EnableTextures( false );

            DrawRect( X, Y, W, H );
        }

        /// <summary>
        /// Draws a 2D textured quad.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        public static void DrawTextured( float X, float Y, float W, float H )
        {
            Texture.EnableTextures( );

            DrawRect( X, Y, W, H );
        }

        #endregion

        #region Rotated Quad

        /// <summary>
        /// Draws a 2D rotated quad.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        /// <param name="Radians"></param>
        /// <param name="RotationPoint">( 0, 0 ) = Top left corner, ( 1, 1 ) = Bottom right corner.</param>
        public static void DrawRotated( float X, float Y, float W, float H, float Radians, Vector2 RotationPoint )
        {
            Texture.EnableTextures( false );

            Vector2 Move = new Vector2( X + W * RotationPoint.X, Y + H * RotationPoint.X );
            Matrix4 Rot = Matrix4.CreateTranslation( -Move.X, -Move.Y, 0 ) * 
                          Matrix4.CreateRotationZ( Radians ) *
                          Matrix4.CreateTranslation( Move.X, Move.Y, 0 );

            GL.PushMatrix( );
            {
                GL.LoadMatrix( ref Rot );
                Draw( X, Y, W, H );
            }
            GL.PopMatrix( );
        }

        /// <summary>
        /// Draws a 2D rotated and textured quad.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        /// <param name="Radians"></param>
        /// <param name="RotationPoint"></param>
        public static void DrawRotatedTextured( float X, float Y, float W, float H, float Radians, Vector2 RotationPoint )
        {
            Texture.EnableTextures( );

            DrawRotated( X, Y, W, H, Radians, RotationPoint );
        }

        #endregion
    }
}
