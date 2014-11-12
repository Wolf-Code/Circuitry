using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;
using SharpLib2D.Info;

namespace SharpLib2D.UI
{
    public class Label : Control
    {
        public string Text { set; get; }
        public string Font { set; get; }
        public float FontSize { set; get; }
        public Directions.HorizontalAlignment HorizontalAlignment { set; get; }
        public Directions.VerticalAlignment VerticalAlignment { set; get; }

        public Label( )
        {
            Font = "Arial";
            FontSize = 12;
            Text = string.Empty;
            IgnoreMouseInput = true;
            PreventLeavingParent = false;
            HorizontalAlignment = Directions.HorizontalAlignment.Left;
            VerticalAlignment = Directions.VerticalAlignment.Top;
            SizeToContents( );
        }

        public void SetText( string Text )
        {
            this.Text = Text;
        }

        protected override void DrawSelf( )
        {
            if ( Text.Length <= 0 )
                return;

            Vector2 Pos = Position;
            switch ( HorizontalAlignment )
            {
                case Directions.HorizontalAlignment.Center:
                    Pos.X += Width / 2f;
                    break;

                case Directions.HorizontalAlignment.Right:
                    Pos.X += Width;
                    break;
            }

            switch ( VerticalAlignment )
            {
                case Directions.VerticalAlignment.Center:
                    Pos.Y += Height / 2f;
                    break;

                case Directions.VerticalAlignment.Bottom:
                    Pos.Y += Height;
                    break;
            }

            Graphics.Text.SetAlignments( HorizontalAlignment, VerticalAlignment );
            Graphics.Text.DrawString( Text, Font, FontSize, Pos, new Color4( 25, 25, 25, 255 ) );
        }


        public void SizeToContents( )
        {
            Vector2 S = Graphics.Text.MeasureString( Text, Font, FontSize );
            SetSize( S.X, S.Y );
        }
    }
}
