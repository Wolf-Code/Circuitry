using OpenTK.Graphics;
using SharpLib2D.Resources;

namespace SharpLib2D.Graphics
{
    public static class Circle
    {
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="X">The X-coordinate.</param>
        /// <param name="Y">The Y-coordinate.</param>
        /// <param name="Radius">The radius.</param>
        /// <param name="Quality">The amount of vertices in the circle.</param>
        public static void Draw( float X, float Y, float Radius, int Quality = 8 )
        {
            Texture.EnableTextures( false );
            Rectangle.DrawRounded( X - Radius, Y - Radius, Radius * 2, Radius * 2, Radius, Quality / 4 );
        }

        /// <summary>
        /// Draws an outlined circle.
        /// </summary>
        /// <param name="X">The X-coordinate.</param>
        /// <param name="Y">The Y-coordinate.</param>
        /// <param name="Radius">The radius.</param>
        /// <param name="Outline">The width of the outline.</param>
        /// <param name="OutlineColor">The color of the outline.</param>
        /// <param name="InsideColor">The color of the circle.</param>
        /// <param name="Quality">The amount of vertices in the circle.</param>
        public static void DrawOutlined( float X, float Y, float Radius, float Outline, Color4 OutlineColor, Color4 InsideColor, int Quality = 8 )
        {
            Texture.EnableTextures( false );
            Rectangle.DrawRoundedOutlined( X - Radius, Y - Radius, Radius * 2, Radius * 2, OutlineColor, InsideColor, Outline, Radius, Quality / 4 );
        }
    }
}
