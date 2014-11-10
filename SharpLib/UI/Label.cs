using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;

namespace SharpLib2D.UI
{
    public class Label : Control
    {
        public string Text { set; get; }
        public string Font { set; get; }
        public float FontSize { set; get; }
        public Text.HorizontalAlignment HorizontalAlignment { set; get; }
        public Text.VerticalAlignment VerticalAlignment { set; get; }

        public Label( )
        {
            this.Font = "Arial";
            this.FontSize = 12;
            this.Text = string.Empty;
            this.IgnoreMouseInput = true;
            this.PreventLeavingParent = false;
            this.HorizontalAlignment= Graphics.Text.HorizontalAlignment.Left;
            this.VerticalAlignment = Graphics.Text.VerticalAlignment.Top;
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
                case Graphics.Text.HorizontalAlignment.Center:
                    Pos.X += this.Width / 2f;
                    break;

                case Graphics.Text.HorizontalAlignment.Right:
                    Pos.X += this.Width;
                    break;
            }

            switch ( VerticalAlignment )
            {
                case Graphics.Text.VerticalAlignment.Center:
                    Pos.Y += this.Height / 2f;
                    break;

                case Graphics.Text.VerticalAlignment.Bottom:
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
