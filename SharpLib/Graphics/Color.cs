using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace SharpLib2D.Graphics
{
    public static class Color
    {
        /// <summary>
        /// The color currently being used to draw.
        /// </summary>
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
        /// <param name="C">The new color.</param>
        public static void Set( Color4 C )
        {
            ActiveColor = C;
        }

        /// <summary>
        /// Sets the active rendering color.
        /// </summary>
        /// <param name="R">The red component.</param>
        /// <param name="G">The green component.</param>
        /// <param name="B">The blue component.</param>
        /// <param name="A">The alpha component.</param>
        public static void Set( float R, float G, float B, float A = 1f )
        {
            ActiveColor = new Color4( R, G, B, A );
        }

        /// <summary>
        /// Clears the screen into a solid color.
        /// </summary>
        /// <param name="C">The color to clear with.</param>
        public static void Clear( Color4 C )
        {
            GL.ClearColor( C );
        }
    }
}
