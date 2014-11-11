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
            this.Font = "Arial";
            this.FontSize = 12;
            this.Text = string.Empty;
            this.IgnoreMouseInput = true;
            this.PreventLeavingParent = false;
            this.HorizontalAlignment = Directions.HorizontalAlignment.Left;
            this.VerticalAlignment = Directions.VerticalAlignment.Top;
            this.SizeToContents( );
        }

        public void SetText( string Text )
        {
            this.Text = Text;
        }

        protected override void DrawSelf( )
        {
            if ( this.Text.Length <= 0 )
                return;

            Vector2 Pos = this.Position;
            switch ( HorizontalAlignment )
            {
                case Directions.HorizontalAlignment.Center:
                    Pos.X += this.Width / 2f;
                    break;

                case Directions.HorizontalAlignment.Right:
                    Pos.X += this.Width;
                    break;
            }

            switch ( VerticalAlignment )
            {
                case Directions.VerticalAlignment.Center:
                    Pos.Y += this.Height / 2f;
                    break;

                case Directions.VerticalAlignment.Bottom:
                    Pos.Y += this.Height;
                    break;
            }

            Graphics.Text.SetAlignments( this.HorizontalAlignment, this.VerticalAlignment );
            Graphics.Text.DrawString( this.Text, this.Font, this.FontSize, Pos, new Color4( 25, 25, 25, 255 ) );
        }


        public void SizeToContents( )
        {
            Vector2 S = Graphics.Text.MeasureString( this.Text, this.Font, this.FontSize );
            this.SetSize( S.X, S.Y );
        }
    }
}
