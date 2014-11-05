using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using SharpLib2D.Resources;

namespace SharpLib2D.Graphics.Objects
{
    internal class NinePatch
    {
        private System.Drawing.Rectangle Region;
        private int TopPatch, BottomPatch, LeftPatch, RightPatch;

        public System.Drawing.Rectangle TopRight { private set; get; }
        public System.Drawing.Rectangle TopLeft { private set; get; }
        public System.Drawing.Rectangle Top { private set; get; }
        public System.Drawing.Rectangle Center { private set; get; }

        public NinePatch( int Left, int Top, int Width, int Height, int LeftPatch, int TopPatch, int RightPatch,
            int BottomPatch )
        {
            this.Region = new System.Drawing.Rectangle( Left, Top, Width, Height );
            this.TopPatch = TopPatch;
            this.RightPatch = RightPatch;
            this.BottomPatch = BottomPatch;
            this.LeftPatch = LeftPatch;

            this.Center = new System.Drawing.Rectangle( Left + LeftPatch, Top + TopPatch,
                Region.Width - LeftPatch - RightPatch, Region.Height - TopPatch - BottomPatch );

            this.TopLeft = new System.Drawing.Rectangle( Region.Left, Region.Top, LeftPatch, TopPatch );
            this.Top = new System.Drawing.Rectangle( Center.Left, Region.Top, Center.Width, TopPatch );
            this.TopRight = new System.Drawing.Rectangle( Center.Right, Region.Top, RightPatch, TopPatch );
        }

        public static void Draw( Texture T, float X, float Y, float W, float H, NinePatch Patch )
        {
            Color.Set( Color4.White );
            Texture.EnableTextures( );
            T.Bind( );

            RectangleF CUV = T.PixelRegionToUVRegion( Patch.Center );
            Console.WriteLine(CUV + ", " + T.Width + ", " + T.Height);
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
        }
    }
}
