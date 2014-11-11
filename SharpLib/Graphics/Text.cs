using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics.Objects;
using SharpLib2D.Info;
using SharpLib2D.Resources;
using Font = System.Drawing.Font;

namespace SharpLib2D.Graphics
{
    #region Helper Classes

    static class TextObjectContainer
    {
        public static int Count { private set; get; }

        static readonly Dictionary<string, List<TextObject>> Objects = new Dictionary<string, List<TextObject>>( );

        public static void Flush( )
        {
            Count = 0;
            foreach ( TextObject O in Objects.SelectMany( Pair => Pair.Value ) )
                O.Texture.Dispose( );

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

    #endregion

    public static class Text
    {
        private static readonly System.Drawing.Graphics Gfx;
        private static Directions.HorizontalAlignment HorAlignment = Directions.HorizontalAlignment.Left;
        private static Directions.VerticalAlignment VerAlignment = Directions.VerticalAlignment.Top;

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
            return Loader.Get<Resources.Font>( Font ).Get( Size );
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

        public static void SetAlignments( Directions.HorizontalAlignment Horizontal, Directions.VerticalAlignment Vertical )
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
        public static void DrawString( string Text, string Font, float Size, Vector2 Position, Color4 Color )
        {
            TextObject O = TextObjectContainer.GetTextObject( Text, Font, Size );
            O.Texture.Bind( );

            Vector2 S = MeasureString( Text, Font, Size );

            switch ( HorAlignment )
            {
                case Directions.HorizontalAlignment.Center:
                    Position.X -= S.X / 2;
                    break;

                case Directions.HorizontalAlignment.Right:
                    Position.X -= S.X;
                    break;
            }

            switch ( VerAlignment )
            {
                case Directions.VerticalAlignment.Center:
                    Position.Y -= S.Y / 2;
                    break;

                    case Directions.VerticalAlignment.Bottom:
                    Position.Y -= S.Y;
                    break;
            }

            Color4 Old = Graphics.Color.ActiveColor;
            Graphics.Color.Set( Color );
            {
                Rectangle.DrawTextured( Position.X, Position.Y, O.Width, O.Height );
            }
            Graphics.Color.Set( Old );
        }

        #endregion
    }
}
