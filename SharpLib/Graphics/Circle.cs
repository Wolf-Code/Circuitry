
using OpenTK.Graphics;
using SharpLib2D.Resources;

namespace SharpLib2D.Graphics
{
    public static class Circle
    {
        public static void Draw( float X, float Y, float Radius, int Quality = 8 )
        {
            Texture.EnableTextures( false );
            Rectangle.DrawRounded( X - Radius, Y - Radius, Radius * 2, Radius * 2, Radius, Quality / 4 );
        }

        public static void DrawOutlined( float X, float Y, float Radius, float Outline, Color4 OutlineColor, Color4 InsideColor, int Quality = 8 )
        {
            Texture.EnableTextures( false );
            Rectangle.DrawRoundedOutlined( X - Radius, Y - Radius, Radius * 2, Radius * 2, OutlineColor, InsideColor, Outline, Radius, Quality / 4 );
        }
    }
}
