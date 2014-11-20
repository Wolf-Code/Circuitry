using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using OpenTK;
using SharpLib2D.Resources;

namespace SharpLib2D.Graphics.Objects
{
    class TextObject
    {
        public readonly string Text;
        public readonly string Font;
        public readonly float Size;
        public readonly int Width, Height;
        public readonly Texture Texture;

        public TextObject( string Text, string Font, float Size )
        {
            this.Text = Text;
            this.Font = Font;
            this.Size = Size;

            Vector2 S = Graphics.Text.MeasureString( Text, Font, Size );
            Width = ( int )S.X;
            Height = ( int )S.Y;
            using ( Bitmap B = new Bitmap( Width, Height, PixelFormat.Format32bppArgb ) )
            {
                using ( System.Drawing.Graphics G = System.Drawing.Graphics.FromImage( B ) )
                {
                    G.TextRenderingHint = TextRenderingHint.AntiAlias;
                    G.Clear( System.Drawing.Color.FromArgb( 0 ) );
                    G.DrawString( Text,
                        Graphics.Text.GetFont( Font, Size ),
                        new SolidBrush( System.Drawing.Color.White ), 
                        PointF.Empty );
                }

                Texture = Resources.Loaders.Texture.LoadTexture( B );
            }
        }
    }
}
