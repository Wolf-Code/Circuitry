using System;
using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;
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
            NinePatch_ScrollbarButtons_Normal = NinePatch.CreateNinePatches( 464, 208, 15, 15, 2, 2, 2, 2, 4, false, true, 1 ),
            NinePatch_ScrollbarButtons_Hover = NinePatch.CreateNinePatches( 480, 208, 15, 15, 2, 2, 2, 2, 4, false, true, 1 ),
            NinePatch_ScrollbarButtons_Down = NinePatch.CreateNinePatches( 464, 272, 15, 15, 2, 2, 2, 2, 4, false, true, 1 ),
            NinePatch_Scrollbars = NinePatch.CreateNinePatches( 384, 208, 15, 127, 2, 2, 2, 2, 5, true, true, 1 ),
            NinePatch_SliderGrips_Horizontal = NinePatch.CreateNinePatches( 418, 33, 11, 14, 2, 2, 2, 5, 4, false, true, 2 ),
            NinePatch_SliderGrips_Vertical = NinePatch.CreateNinePatches( 433, 34, 14, 11, 2, 2, 5, 2, 4, false, true, 5 );

        public GwenTextureSkin( Texture GwenTexture )
        {
            Texture = GwenTexture;
        }

        private void DrawControl( Control C, NinePatch Patch )
        {
            NinePatch.Draw( Texture, C.Position.X, C.Position.Y, C.Size.X, C.Size.Y, Patch );
        }

        private void DrawControl( Control C, Color4 Color )
        {
            Graphics.Color.Set( Color );
            Rectangle.Draw( C.Position.X, C.Position.Y, C.Size.X, C.Size.Y );
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

        public override void DrawCategoryButton( CategoryHeader.CategoryButton B )
        {
            Color4 Col;
            if ( B.IsDown )
                Col = new Color4( 150, 180, 255, 255 );
            else
            {
                if ( B.IsMouseOn )
                    Col = new Color4( 200, 220, 255, 255 );
                else
                    Col = B.Even ? Color4.White : new Color4( 240, 240, 240, 255 );
            }

            DrawControl( B, Col );
        }

        public override void DrawSlider( Slider S )
        {
            double Ticks = S.MaxValue - S.MinValue;

            Vector2 Center = S.TopLeft + S.Size / 2f;
            float GripThickness = ( S.ThicknessVector * S.Grip.Size * 0.5f ).Length;
            float MaxDist = S.Length - ( S.LengthVector * GripThickness * 2f ).Length;
            Vector2 Start = Center - S.LengthVector * S.Size * 0.5f + S.LengthVector * GripThickness;
            Vector2 End = Start + S.LengthVector * MaxDist;
            Color.Set( 0, 0, 0, 255 );
            Line.Draw( Start, End, 2 );

            if ( Ticks >= S.Length / 2f )
                return;

            double Max = Ticks;
            for ( int X = 0; X < Max + 1; X++ )
            {
                float Progress = X / ( float ) Max;
                Vector2 Pos = Start + S.LengthVector * MaxDist * Progress;
                float Mul = 0.5f;
                if ( X == 0 || X == Max )
                {
                    Pos -= S.ThicknessVector * S.Thickness * 0.5f;
                    Mul = 1f;
                }
                else
                {
                    Pos -= S.ThicknessVector * S.Thickness * 0.25f;
                }

                End = Pos + S.ThicknessVector * S.Thickness * Mul;

                Line.Draw( Pos, End, 2 );
            }
        }

        public override void DrawSliderGrip( Slider.SliderGrip G )
        {
            DrawButtonDefault( G,
                G.Slider.Horizontal ? NinePatch_SliderGrips_Horizontal : NinePatch_SliderGrips_Vertical );
        }
    }
}
