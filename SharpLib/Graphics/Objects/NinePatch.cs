using System.Drawing;
using OpenTK.Graphics;
using SharpLib2D.Resources;
using Rect = System.Drawing.Rectangle;

namespace SharpLib2D.Graphics.Objects
{
    internal class NinePatch
    {
        private readonly int TopPatch, BottomPatch, LeftPatch, RightPatch;

        // ReSharper disable MemberCanBePrivate.Global
        public Rect TopRight { private set; get; }
        public Rect TopLeft { private set; get; }
        public Rect Top { private set; get; }
        public Rect Center { private set; get; }
        public Rect BottomLeft { private set; get; }
        public Rect BottomRight { private set; get; }
        public Rect Bottom { private set; get; }
        public Rect Left { private set; get; }
        public Rect Right { private set; get; }
        // ReSharper restore MemberCanBePrivate.Global

        public NinePatch( int Left, int Top, int Width, int Height, int LeftPatch, int TopPatch, int RightPatch,
            int BottomPatch )
        {
            Rect Region = new System.Drawing.Rectangle( Left, Top, Width, Height );
            this.TopPatch = TopPatch;
            this.RightPatch = RightPatch;
            this.BottomPatch = BottomPatch;
            this.LeftPatch = LeftPatch;

            this.Center = new Rect( Left + LeftPatch, Top + TopPatch,
                Region.Width - LeftPatch - RightPatch, Region.Height - TopPatch - BottomPatch );

            this.TopLeft = new Rect( Region.Left, Region.Top, LeftPatch, TopPatch );
            this.Top = new Rect( Center.Left, Region.Top, Center.Width, TopPatch );
            this.TopRight = new Rect( Center.Right, Region.Top, RightPatch, TopPatch );

            this.BottomLeft = new Rect( Region.Left, Region.Bottom - BottomPatch, LeftPatch, BottomPatch );
            this.Bottom = new Rect( BottomLeft.Right, BottomLeft.Top, Center.Width, BottomPatch );
            this.BottomRight = new Rect( Center.Right, BottomLeft.Top, RightPatch, BottomPatch );

            this.Left = new Rect( Left, TopLeft.Bottom, LeftPatch, Center.Height );
            this.Right = new Rect( Center.Right, TopRight.Bottom, RightPatch, Center.Height );
        }

        public static void Draw( Texture T, float X, float Y, float W, float H, NinePatch Patch )
        {
            Color.Set( Color4.White );
            Texture.EnableTextures( );
            T.Bind( );

            RectangleF CUV = T.PixelRegionToUVRegion( Patch.Center );
            Rectangle.DrawWithUV( X + Patch.LeftPatch, Y + Patch.TopPatch, W - Patch.LeftPatch - Patch.RightPatch,
                H - Patch.TopPatch - Patch.BottomPatch, CUV.Left, CUV.Top, CUV.Width, CUV.Height );

            RectangleF TLUV = T.PixelRegionToUVRegion( Patch.TopLeft );
            Rectangle.DrawWithUV( X, Y, Patch.TopLeft.Width, Patch.TopLeft.Height, TLUV.Left, TLUV.Top, TLUV.Width,
                TLUV.Height );

            RectangleF TUV = T.PixelRegionToUVRegion( Patch.Top );
            Rectangle.DrawWithUV( X + Patch.TopLeft.Width, Y, W - Patch.LeftPatch - Patch.RightPatch, Patch.Top.Height,
                TUV.Left, TUV.Top,
                TUV.Width, TUV.Height );

            RectangleF TRUV = T.PixelRegionToUVRegion( Patch.TopRight );
            Rectangle.DrawWithUV( X + W - Patch.TopRight.Width, Y, Patch.TopRight.Width, Patch.TopRight.Height,
                TRUV.Left, TRUV.Top, TRUV.Width, TRUV.Height );


            RectangleF BLUV = T.PixelRegionToUVRegion( Patch.BottomLeft );
            Rectangle.DrawWithUV( X, Y + H - Patch.BottomPatch, Patch.BottomLeft.Width, Patch.BottomLeft.Height,
                BLUV.Left, BLUV.Top, BLUV.Width,
                BLUV.Height );

            RectangleF BUV = T.PixelRegionToUVRegion( Patch.Bottom );
            Rectangle.DrawWithUV( X + Patch.BottomLeft.Width, Y + H - Patch.BottomPatch,
                W - Patch.LeftPatch - Patch.RightPatch, Patch.Bottom.Height,
                BUV.Left, BUV.Top,
                BUV.Width, BUV.Height );

            RectangleF BRUV = T.PixelRegionToUVRegion( Patch.BottomRight );
            Rectangle.DrawWithUV( X + W - Patch.BottomRight.Width, Y + H - Patch.BottomPatch, Patch.BottomRight.Width,
                Patch.BottomRight.Height,
                BRUV.Left, BRUV.Top, BRUV.Width, BRUV.Height );

            RectangleF LUV = T.PixelRegionToUVRegion( Patch.Left );
            Rectangle.DrawWithUV( X, Y + Patch.TopLeft.Height,
                Patch.LeftPatch, H - Patch.BottomPatch - Patch.TopPatch,
                LUV.Left, LUV.Top,
                LUV.Width, LUV.Height );

            RectangleF RUV = T.PixelRegionToUVRegion( Patch.Right );
            Rectangle.DrawWithUV( X + W - Patch.RightPatch, Y + Patch.TopRight.Height,
                Patch.RightPatch, H - Patch.BottomPatch - Patch.TopPatch,
                RUV.Left, RUV.Top,
                RUV.Width, RUV.Height );
        }
    }
}
