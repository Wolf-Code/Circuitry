using SharpLib2D.Graphics.Objects;
using SharpLib2D.Info;
using SharpLib2D.Resources;
using SharpLib2D.UI.Internal;
using SharpLib2D.UI.Internal.Scrollbar;

namespace SharpLib2D.UI.Skin
{
    public class GwenTextureSkin : Skin
    {
        private readonly Texture Texture;

        private static readonly NinePatch
            NinePatch_Panel = new NinePatch( 0, 256, 127, 127, 3, 3, 3, 3 ),
            NinePatch_Window = new NinePatch( 0, 24, 127, 104, 4, 1, 4, 4 ),
            NinePatch_WindowTitleBar = new NinePatch( 0, 0, 127, 24, 4, 4, 4, 1 ),
            NinePatch_CategoryHeader_Closed = new NinePatch( 320, 352, 63, 31, 3, 3, 3, 3 ),
            NinePatch_CategoryHeader_Open = new NinePatch( 320, 384, 63, 20, 3, 3, 3, 0 ),
            NinePatch_Category_Container = new NinePatch( 320, 404, 63, 43, 3, 0, 3, 3 );

        private static readonly NinePatch [ ]
            NinePatch_Buttons = NinePatch.CreateNinePatches( 480, 0, 31, 31, 2, 2, 2, 2, 4, false, true, 1 ),
            NinePatch_CloseButtons = NinePatch.CreateNinePatches( 3, 225, 21, 17, 4, 1, 4, 4, 4, true, true, 11 ),
            NinePatch_CheckBox_Enabled = NinePatch.CreateNinePatches( 448, 32, 15, 15, 2, 2, 2, 2, 2, true, true, 1 ),
            NinePatch_ScrollbarButtons_Normal = NinePatch.CreateNinePatches( 464, 208, 15, 15, 2, 2, 2, 2, 4, false,true, 1 ),
            NinePatch_ScrollbarButtons_Hover = NinePatch.CreateNinePatches( 480, 208, 15, 15, 2, 2, 2, 2, 4, false, true,1 ),
            NinePatch_ScrollbarButtons_Down = NinePatch.CreateNinePatches( 464, 272, 15, 15, 2, 2, 2, 2, 4, false, true,1 ),
            NinePatch_Scrollbars = NinePatch.CreateNinePatches( 384, 208, 15, 127, 2, 2, 2, 2, 5, true, true, 1 );

        public GwenTextureSkin( Texture GwenTexture )
        {
            Texture = GwenTexture;
        }

        private void DrawControl( Control C, NinePatch Patch )
        {
            NinePatch.Draw( Texture, C.Position.X, C.Position.Y, C.Size.X, C.Size.Y, Patch );
        }

        private void DrawButtonDefault( Button B, NinePatch [ ] Patches, int Down = 2, int Hover = 1, int Normal = 0 )
        {
            DrawControl( B, B.IsDown ? Patches[ Down ] : Patches[ B.IsMouseOn ? Hover : Normal ] );
        }

        public override void DrawPanel( Control P )
        {
            DrawControl( P, NinePatch_Panel );
        }

        public override void DrawButton( Button B )
        {
            DrawButtonDefault( B, NinePatch_Buttons, 3 );
        }

        public override void DrawWindowTitleBar( WindowTitleBar B )
        {
            DrawControl( B, NinePatch_WindowTitleBar );
        }

        public override void DrawWindowCloseButton( WindowCloseButton B )
        {
            DrawButtonDefault( B, NinePatch_CloseButtons );
        }

        public override void DrawWindow( Window W )
        {
            DrawControl( W, NinePatch_Window );
        }

        public override void DrawCheckbox( Checkbox C )
        {
            DrawControl( C, NinePatch_CheckBox_Enabled[ C.Checked ? 0 : 1 ] );
        }

        public override void DrawScrollbarButton( ScrollbarButton B )
        {
            int ID = 0;
            switch ( B.Direction )
            {
                case Directions.Direction.Up:
                    ID = 1;
                    break;

                case Directions.Direction.Right:
                    ID = 2;
                    break;

                case Directions.Direction.Down:
                    ID = 3;
                    break;
            }

            DrawControl( B,
                B.IsDown
                    ? NinePatch_ScrollbarButtons_Down[ ID ]
                    : ( B.IsMouseOn ? NinePatch_ScrollbarButtons_Hover[ ID ] : NinePatch_ScrollbarButtons_Normal[ ID ] ) );
        }

        public override void DrawScrollbarBarContainer( ScrollbarBar B )
        {
            DrawControl( B, NinePatch_Scrollbars[ 0 ] );
        }

        public override void DrawScrollbarBar( ScrollbarBarDragger D )
        {
            DrawButtonDefault( D, NinePatch_Scrollbars, 3, 2, 1 );
        }

        public override void DrawCategoryHeader( CategoryHeader H )
        {
            DrawControl( H.TitleBar, H.Opened ? NinePatch_CategoryHeader_Open : NinePatch_CategoryHeader_Closed );
            if ( H.Opened )
                DrawControl( H.Container, NinePatch_Category_Container );
        }
    }
}
