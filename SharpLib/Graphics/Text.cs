using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using OpenTK;

namespace SharpLib2D.Graphics
{
    #region Helper Classes
    static class FontCollection
    {
        private static readonly Dictionary<string, Dictionary<float, Font>> Fonts = new Dictionary<string, Dictionary<float, Font>>( );

        public static Font GetFont( string Font, float Size )
        {
            if ( !Fonts.ContainsKey( Font ) )
                Fonts.Add( Font, new Dictionary<float, Font>( ) );

            if ( !Fonts[ Font ].ContainsKey( Size ) )
                Fonts[ Font ].Add( Size, LoadFont( Font, Size ) );

            return Fonts[ Font ][ Size ];
        }

        private static Font LoadFont( string Font, float Size )
        {
            return new Font( Font, Size, FontStyle.Regular );
        }
    }

    static class TextObjectContainer
    {
        public static int Count { private set; get; }

        static readonly Dictionary<string, List<TextObject>> Objects = new Dictionary<string, List<TextObject>>( );

        public static void Flush( )
        {
            Count = 0;
            foreach ( TextObject O in Objects.SelectMany( Pair => Pair.Value ) )
                O.Texture.Remove( );

            Objects.Clear( );
        }

        public static bool HasTextObject( string Text, string Font, float Size )
        {
            return Objects.ContainsKey( Text ) &&
                   Objects[ Text ].FirstOrDefault( O => O.Font == Font && O.Size == Size ) != null;
        }

        public static TextObject GetTextObject( string Text, string Font, float Size )
        {
            TextObject Obj = !Objects.ContainsKey( Text )
                ? null
                : Objects[ Text ].FirstOrDefault( O => O.Font == Font && O.Size == Size );
            if ( Obj != null ) return Obj;

            Obj = new TextObject( Text, Font, Size );
            AddObject( Obj );

            return Obj;
        }

        private static void AddObject( TextObject O )
        {
            if ( !Objects.ContainsKey( O.Text ) ) 
                Objects.Add( O.Text, new List<TextObject>( ) );

            Objects[ O.Text ].Add( O );
            Count++;
        }
    }

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
            Width = ( int ) S.X;
            Height = ( int ) S.Y;
            using ( Bitmap B = new Bitmap( Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb ) )
            {
                using ( System.Drawing.Graphics G = System.Drawing.Graphics.FromImage( B ) )
                {
                    G.TextRenderingHint = TextRenderingHint.AntiAlias;
                    G.Clear( System.Drawing.Color.Transparent );
                    G.DrawString( Text,
                        Graphics.Text.GetFont( Font, Size ),
                        Brushes.White,
                        PointF.Empty );
                }

                this.Texture = Texture.Load( B );
            }
        }
    }

    #endregion

    public static class Text
    {
        private static readonly System.Drawing.Graphics Gfx;
        private static HorizontalAlignment HorAlignment = HorizontalAlignment.Left;
        private static VerticalAlignment VerAlignment = VerticalAlignment.Top;

        public enum HorizontalAlignment
        {
            Left,
            Right,
            Center
        }

        public enum VerticalAlignment
        {
            Top,
            Bottom,
            Center
        }

        /// <summary>
        /// The amount of different text-font-size combinations currently in memory.
        /// </summary>
        public static int LoadedTexts
        {
            get { return TextObjectContainer.Count; }
        }

        static Text( )
        {
            Gfx = System.Drawing.Graphics.FromImage( new Bitmap( 1, 1 ) );
        }

        internal static Font GetFont( string Font, float Size )
        {
            return FontCollection.GetFont( Font, Size );
        }

        /// <summary>
        /// Flushes all loaded texts, clearing the memory they used.
        /// </summary>
        public static void FlushTexts( )
        {
            TextObjectContainer.Flush( );
        }

        /// <summary>
        /// Measures the size of a string using a given font and size.
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Font"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static Vector2 MeasureString( string Text, string Font, float Size )
        {
            if ( TextObjectContainer.HasTextObject( Text, Font, Size ) )
            {
                TextObject O = TextObjectContainer.GetTextObject( Text, Font, Size );
                return new Vector2( O.Width, O.Height );
            }

            SizeF S = Gfx.MeasureString( Text, GetFont( Font, Size ), new PointF( 0, 0 ),
                new StringFormat( StringFormatFlags.MeasureTrailingSpaces ) );

            return new Vector2( S.Width, S.Height );
        }

        #region Set parameters

        public static void SetAlignments( HorizontalAlignment Horizontal, VerticalAlignment Vertical )
        {
            HorAlignment = Horizontal;
            VerAlignment = Vertical;
        }

        #endregion

        #region Draw String

        /// <summary>
        /// Draws a string using a given font and size.
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Font"></param>
        /// <param name="Size"></param>
        /// <param name="Position"></param>
        public static void DrawString( string Text, string Font, float Size, Vector2 Position )
        {
            TextObject O = TextObjectContainer.GetTextObject( Text, Font, Size );
            O.Texture.Bind( );

            Vector2 S = MeasureString( Text, Font, Size );

            switch ( HorAlignment )
            {
                case HorizontalAlignment.Center:
                    Position.X -= S.X / 2;
                    break;

                case HorizontalAlignment.Right:
                    Position.X -= S.X;
                    break;
            }

            switch ( VerAlignment )
            {
                case VerticalAlignment.Center:
                    Position.Y -= S.Y / 2;
                    break;

                    case VerticalAlignment.Bottom:
                    Position.Y -= S.Y;
                    break;
            }

            Rectangle.DrawTextured( Position.X, Position.Y, O.Width, O.Height );
        }

        #endregion
    }
}
