using SharpLib2D.Graphics.Objects;
using SharpLib2D.Resources;
using SharpLib2D.UI.Internal;

namespace SharpLib2D.UI.Skin
{
    public class GwenTextureSkin : Skin
    {
        private readonly Texture Texture;

        private static readonly NinePatch 
            NinePatch_Panel = new NinePatch( 0, 256, 127, 127, 2, 2, 2, 2 ),
            NinePatch_Window = new NinePatch( 0, 24, 127, 104, 2, 1, 2, 2 ),
            NinePatch_WindowTitleBar = new NinePatch( 0, 0, 127, 24, 2, 2, 2, 1 );

        private static readonly NinePatch [ ]
            NinePatch_Buttons = NinePatch.CreateNinePatches( 480, 0, 31, 31, 2, 2, 2, 2, 4, false, true, 1 ),
            NinePatch_CloseButtons = NinePatch.CreateNinePatches( 3, 225, 21, 17, 2, 1, 2, 2, 4, true, true, 11 ),
            NinePatch_CheckBox_Enabled = NinePatch.CreateNinePatches( 448, 32, 15, 15, 2, 2, 2, 2, 2, true, true, 1 );

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
            DrawControl( B, B.IsDown ? NinePatch_Buttons[ 3 ] : NinePatch_Buttons[ B.IsMouseOn ? 1 : 0 ] );
        }

        public override void DrawWindowTitleBar( WindowTitleBar B )
        {
            DrawControl( B, NinePatch_WindowTitleBar );
        }

        public override void DrawWindowCloseButton( WindowCloseButton B )
        {
            DrawControl( B, B.IsDown ? NinePatch_CloseButtons[ 2 ] : NinePatch_CloseButtons[ B.IsMouseOn ? 1 : 0 ] );
        }

        public override void DrawWindow( Window W )
        {
            DrawControl( W, NinePatch_Window );
        }

        public override void DrawCheckbox( Checkbox C )
        {
            DrawControl( C, NinePatch_CheckBox_Enabled[ C.Checked ? 0 : 1 ] );
        }
    }
}
