using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace SharpLib2D.Graphics
{
    public static class Color
    {
        public static Color4 ActiveColor
        {
            private set;
            get;
        }

        static Color( )
        {
            ActiveColor = Color4.White;
        }

        /// <summary>
        /// Sets the active rendering color.
        /// </summary>
        /// <param name="C"></param>
        public static void Set( Color4 C )
        {
            ActiveColor = C;
        }

        /// <summary>
        /// Sets the active rendering color.
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <param name="A"></param>
        public static void Set( float R, float G, float B, float A = 1f )
        {
            ActiveColor = new Color4( R, G, B, A );
        }

        /// <summary>
        /// Clears the screen into a solid color.
        /// </summary>
        /// <param name="C"></param>
        public static void Clear( Color4 C )
        {
            GL.ClearColor( C );
        }
    }
}
