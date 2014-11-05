using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SharpLib2D.Resources;
using SharpLib2D.States;

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

        #region DrawOutlined

        public static void DrawOutlined( float X, float Y, float W, float H, float Outline = 1f )
        {
            Line.Draw( new Vector2( X, Y ), new Vector2( X + W, Y ), Outline );
            Line.Draw( new Vector2( X + W, Y ), new Vector2( X + W, Y + H ), Outline );
            Line.Draw( new Vector2( X + W, Y + H ), new Vector2( X, Y + H ), Outline );
            Line.Draw( new Vector2( X, Y + H ), new Vector2( X, Y ), Outline );
        }

        #endregion

        #region DrawRounded

        /// <summary>
        /// Draws a rounded rectangle.
        /// </summary>
        /// <param name="X">The X-coordinate.</param>
        /// <param name="Y">The Y-coordinate.</param>
        /// <param name="W">The width.</param>
        /// <param name="H">The height.</param>
        /// <param name="Size">The size of the rounded corner.</param>
        /// <param name="Quality">The quality of the rounded corner.</param>
        /// <param name="TopLeft">Whether to draw the top left corner rounded or not.</param>
        /// <param name="TopRight">Whether to draw the top right corner rounded or not.</param>
        /// <param name="BottomRight">Whether to draw the bottom right corner rounded or not.</param>
        /// <param name="BottomLeft">Whether to draw the bottom left corner rounded or not.</param>
        public static void DrawRounded( float X, float Y, float W, float H, float Size, int Quality, bool TopLeft = true, bool TopRight = true, bool BottomRight = true, bool BottomLeft = true )
        {
            if ( Size <= 0 )
            {
                DrawRect( X, Y, W, H );
                return;
            }

            Vector2 TLeft = new Vector2( X + Size, Y + Size );
            Vector2 BRight = new Vector2( X + W - Size, Y + H - Size );

            // Center
            DrawRect( X, TLeft.Y, W, H - Size * 2 );

            // Top
            DrawRect( X + Size, Y, W - Size * 2, Size );

            // Bottom
            DrawRect( X + Size, Y + H - Size, W - Size * 2, Size );

            float PiDiv = ( float )( System.Math.PI / 2f / Quality );

            // Top Left
            for ( int Q = 0; Q < 4; Q++ )
            {
                Vector2 Start = TLeft;
                int MulX = -1, MulY = -1;

                switch ( Q )
                {
                        // Top Left
                    case 0:
                        if ( !TopLeft )
                        {
                            Draw( X, Y, Size, Size );
                            continue;
                        }
                        break;
                        // Top Right
                    case 1:
                        if ( !TopRight )
                        {
                            Draw( BRight.X, Y, Size, Size );
                            continue;
                        }

                        Start = new Vector2( BRight.X, TLeft.Y );
                        MulX = 1;
                        break;

                        // Bottom Right
                    case 2:
                        if ( !BottomRight )
                        {
                            Draw( BRight.X, BRight.Y, Size, Size );
                            continue;
                        }

                        Start = BRight;
                        MulX = 1;
                        MulY = 1;
                        break;

                        // Bottom Left
                    case 3:
                        if ( !BottomLeft )
                        {
                            Draw( X, BRight.Y, Size, Size );
                            continue;
                        }
                        Start = new Vector2( TLeft.X, BRight.Y );
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

        #region DrawRoundedOutlined

        public static void DrawRoundedOutlined( float X, float Y, float W, float H, Color4 OutlineColor,
            Color4 InsideColor, float Outline, float Size = 8, int Quality = 8, bool TopLeft = true, bool TopRight = true, bool BottomRight = true, bool BottomLeft = true )
        {
            Color.Set( OutlineColor );
            DrawRounded( X, Y, W, H, Size, Quality, TopLeft, TopRight, BottomRight, BottomLeft );

            Color.Set( InsideColor );
            DrawRounded( X + Outline, Y + Outline, W - Outline * 2, H - Outline * 2, ( Size - Outline ), Quality, TopLeft, TopRight, BottomRight, BottomLeft );
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
                          Matrix4.CreateTranslation( Move.X, Move.Y, 0 ) *
                          State.ActiveState.Camera.View;

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
