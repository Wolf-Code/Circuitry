using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SharpLib2D.Graphics
{
    public static class Rectangle
    {
        #region New OpenGL
        /*
         * TODO: Use non-deprecated OpenGL
        private static int iVBO;

        static Rectangle( )
        {
            iVBO = GL.GenBuffer( );

            Vertex [ ] Vertices = new Vertex[ 4 ];

            Vertices[ 0 ] = new Vertex
            {
                Normal = new Vector3( 0, 0, 1 ),
                Position = new Vector3( 0, 0, 0 ),
                UV = new Vector2( 0, 0 )
            };

            Vertices[ 1 ] = new Vertex
            {
                Normal = new Vector3( 0, 0, 1 ),
                Position = new Vector3( 1, 0, 0 ),
                UV = new Vector2( 1, 0 )
            };

            Vertices[ 2 ] = new Vertex
            {
                Normal = new Vector3( 0, 0, 1 ),
                Position = new Vector3( 1, 1, 0 ),
                UV = new Vector2( 1, 1 )
            };

            Vertices[ 3 ] = new Vertex
            {
                Normal = new Vector3( 0, 0, 1 ),
                Position = new Vector3( 0, 1, 0 ),
                UV = new Vector2( 0, 1 )
            };

            GL.BindBuffer( BufferTarget.ArrayBuffer, iVBO );
            {
                GL.BufferData( BufferTarget.ArrayBuffer,
                    ( IntPtr ) ( Vertices.Length * 8 * sizeof ( float ) ),
                    Vertices, BufferUsageHint.StaticDraw );
            }
            GL.BindBuffer( BufferTarget.ArrayBuffer, 0 );
        }
        */
        #endregion

        #region DrawRect

        private static void DrawRect( float X, float Y, float W, float H )
        {
            GL.Color4( Color.ActiveColor );
            GL.Begin( PrimitiveType.Quads );
            {
                GL.TexCoord2( 0.0f, 0.0f );
                GL.Vertex2( X, Y );

                GL.TexCoord2( 1.0f, 0.0f );
                GL.Vertex2( X + W, Y );

                GL.TexCoord2( 1.0f, 1.0f );
                GL.Vertex2( X + W, Y + H );

                GL.TexCoord2( 0.0f, 1.0f );
                GL.Vertex2( X, Y + H );
            }
            GL.End( );
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
