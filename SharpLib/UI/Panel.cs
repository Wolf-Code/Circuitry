using OpenTK;

namespace SharpLib2D.UI
{
    public class Panel : Control
    {
        public override void Draw( FrameEventArgs e )
        {
            Canvas.Skin.DrawControl( this );
            DrawChildren( e );
        }
    }
}
