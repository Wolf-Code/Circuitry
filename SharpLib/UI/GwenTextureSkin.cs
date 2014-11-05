using SharpLib2D.Graphics.Objects;
using SharpLib2D.Resources;

namespace SharpLib2D.UI
{
    public class GwenTextureSkin : Skin
    {
        private readonly Texture Texture;
        private static readonly NinePatch NinePatch_Panel = new NinePatch( 0, 256, 127, 127, 2, 2, 2, 2 );

        public GwenTextureSkin( Texture GwenTexture )
        {
            this.Texture = GwenTexture;
        }

        private void DrawControl( Control C, NinePatch Patch )
        {
            NinePatch.Draw( Texture, C.Position.X, C.Position.Y, C.Size.X, C.Size.Y, Patch );
        }

        public override void DrawPanel( Control P )
        {
            DrawControl( P, NinePatch_Panel );
        }

        public override void DrawButton( Button B )
        {
            
        }
    }
}
