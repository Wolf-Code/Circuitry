using OpenTK;
using OpenTK.Graphics;

namespace SharpLib2D.UI
{
    public class Label : Control
    {
        public string Text { set; get; }
        public string Font { set; get; }
        public float FontSize { set; get; }

        public Label( )
        {
            this.Font = "Arial";
            this.FontSize = 12;
            this.Text = string.Empty;
            this.IgnoreMouseInput = true;
            this.PreventLeavingParent = false;
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

            Graphics.Text.DrawString( this.Text, this.Font, this.FontSize, this.Position, new Color4( 25, 25, 25, 255 ) );
        }

        public void SizeToContents( )
        {
            Vector2 S = Graphics.Text.MeasureString( this.Text, this.Font, this.FontSize );
            this.SetSize( S.X, S.Y );
        }
    }
}
