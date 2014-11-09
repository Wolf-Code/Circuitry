using System;
using OpenTK.Graphics.OpenGL;

namespace SharpLib2D.Graphics
{
    public static class Scissor
    {
        /// <summary>
        /// Enables scissor testing.
        /// </summary>
        public static void Enable( )
        {
            GL.Enable( EnableCap.ScissorTest );
        }

        /// <summary>
        /// Disable scissor testing.
        /// </summary>
        public static void Disable( )
        {
            GL.Disable( EnableCap.ScissorTest );
        }

        /// <summary>
        /// Sets the scissor testing rectangle.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public static void SetScissorRectangle( float X, float Y, float Width, float Height )
        {
            GL.Scissor( ( int ) X, ( int ) ( Info.Screen.Size.Y - Y ), ( int ) Width, ( int ) -Height );
        }
    }
}
